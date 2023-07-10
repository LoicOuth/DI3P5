---
layout: Part
author: "Loïc OUTHIER"
---

# Choix de l'architecture de monitoring

[[toc]]

## Description des besoins

### Les actifs du projet

Notre infrastructure complète se décline en deux environnements :

- L'environnement de `dev` et de `test` hébergé sur un serveur du DIIAGE
- L'environnement de `prod` hébergé sur Azure

#### Environnement DIIAGE

L'ensemble des serveurs des environnements de `dev` et de `test` est hébergé sur un serveur Proxmox, lui-même hébergé sur un serveur physique `HPE Proliant`. Ce dernier est hébergé directement au DIIAGE et est accessible depuis le réseau du DIIAGE en `10.4.0.0/16`.

Les deux environnements sont mutualisés sur l'infrastructure mise en place. L'infrastructure se compose des éléments suivants :

- Un pare-feu `pfSense`, mis au-devant de toute l'infrastructure permettant une protection des en provenance des réseaux externes
- Un serveur `Proxmox` qui virtualise l'ensemble des serveurs ainsi que le réseau privé de l'infrastructure qui est alors strictement interne
- Quatre machines virtuelles sous `Debian` créées à partir d'une machine templatisée

#### Environnement Azure

L'environnement Azure se compose des éléments suivants :

- Un `Azure Container Registry` qui nous sert de dépôt pour nos images docker
- Un cluster `Azure Kubernetes Service`

### Les besoins de monitoring par environnement

Approche séquentielle **bottom up** en se basant sur le modèle OSI.

- **Monitoring réseau**
  - Monitoring du routeur (passerelle de l'infrastructure)
  - Monitoring de la connexion Internet
- **Monitoring des serveurs** (RAM, CPU, Disque, Réseau)
  - Proxmox
  - pfSense
  - serveurs Debian (K8s)
- **Monitoring des services**
  - K8s
    - Etat de santé des pods
      - RAM et CPU consommé pour vérifier si le pod prend pas tout
      - Erreurs récentes (log)
    - Services
      - Nombre et état des Ingress (lié aux clients)
      - Disponibilité des services (s'ils sont actifs ou pas)
      - Erreurs récentes (si service est souvent en erreur)
    - API Kubernetes
      - Latence (surveiller la performance et la disponibilité de l'API du cluster)
      - Taux d'erreurs (détecter les problèmes de configuration et les erreurs d'authentification)
    - Etcd
      - Utilisation du disque
      - Latence des opérations
      - Taux d'erreurs
    - CoreDNS
      - Taux de requêtes
      - Taux d'erreurs
      - Latence
    - CSI
  - ArgoCD
    - Etat des déploiements
    - Versions déployées
  - pfSense
    - HaProxy
      - Etat des backends
      - Etat des frontends
      - Taux d'erreurs
    - OpenVPN
  - Proxmox
    - Etat du service VM
    - Vérification des backups

### Les contraintes

Plusieures contraintes sont à prendre en compte lors du choix de l'architecture de moniroting :

- **Une infrastructure hybride** - Il faudra prendre en compte le fait que les environnements sont présents hébergés sur-site pour une partie et sur la plateforme cloud Azure pour le reste.
- **Un budget limité** - Dans le cas ou nous hébergerions nous-même la solution de monitoring sur Azure, le budget alloué à la solution ne doit pas excéder $15 par mois.

## Solutions de monitoring envisageables

Voici la liste des solutions envisagées dans notre projet.

Les solutions sont ordonnées par ordre de préférence :

- **Open Source**
  - **Zabbix** - Solution open-source maintenue par une communauté très active.
    - Avec Grafana
  - **ELK** - Solution de monitoring orientée analyse de logs
  - **Prometheus** - Solution très utilisé pour du monitoring Kubernetes
    - Avec Grafana
  - **Zipkin** - Solution de monitoring dédiée aux traces
  - **Loki** - Solution de monitoring orientée analyse de logs
  - **Nagios** - Solution de monitoring complète
  - **PRTG** - Solution de monitoring complète basée sur le protocole SNMP
  - **Pandora FMS** - Solution de monitoring

- **Propriétaire**
  - **Datadog** - Solution MaaS adaptée au monitoring d'infrastructures hybrides
    - Tier free limité à 5 hosts, autrement $15/mois par host
  - **Azure Monitor** - Solution de monitoring de la plateforme cloud Azure
    - Peut s'intégrer à on-premises avec `Azure Arc`

## Conclusion de l'étude

### Architecture retenue

Cet ensemble de solutions a été retenu pour le monitoring de l'infrastructure complète :

- **Zabbix** - Hébergé on-premises pour le monitoring exclusif de l'environnement sur site, comprenant l'ensemble des machines virtuelles (RAM, CPU, Disque...)
- **Prometheus** - Déployé en tant que pod dans Kubernetes, pour le monitoring avancé de Kubernetes
- **Azure Monitor** - Pour le monitoring des services Azure
- **Grafana** - Hébergé sur une plateforme cloud (Azure ou OVH), pour la centralisation du moniroting (via installation de modules)

### Axes d'amélioration

- **Azure Prometheus** - Pour le monitoring spécifique Kubernetes sur l'AKS
- **Prometheus** (à installer sur les nodes AKS) - Déployé en tant que pod dans Kubernetes, pour le monitoring complet de Kubernetes

### Justification de la démarche

- **Grafana**
  - **Centralisation du monitoring sur Grafana**  
        Grafana est un service sans état qui récupère les données de services externes afin de les mettre en forme.  
        Ce service permet de centraliser toutes les sources de données (Zabbix, Prometheus, Azure Monitor) en un point.
  - **Externalisation du service Grafana**  
    Le service a été externalisé pour les raisons suivantes :
    1. En cas de **dysfonctionnement majeur** de l'environnement on-premises (panne du serveur Proxmox), nous pouvons toujours monitorer activement l'environnement de production et être alerté de la non réception de données de la part de l'environnement sur site.
    2. En cas de **dysfonctionnement réseau** sur l'environnement on-premises, le service Zabbix hébergé localement peut continuer de monitorer les serveurs. Une fois la connexion réseau rétablie, les données sont réaffichées sans perte de données.
- **Zabbix**
  - **Choix de Zabbix**  
    La solution Zabbix a été retenue pour la grande diversité du monitoring qu'il peut accomplir. Il est de plus intégrable au service Grafana.
  - **Installation sur site**  
    La décision d'installer Zabbix on-premises a été prise pour les raisons suivantes :
    1. Zabbix est dédié uniquement au monitoring on-premises. En cas de coupure internet, le monitoring de toute l'infrastructure on-premises n'est pas interrompu.
- **Prometheus**
  - **Choix de Prometheus**  
    Prometheus est la meilleure option pour monitorer un cluster Kubernetes. En effet, ces derniers offrent nativement la possibilité d'intégrer le monitoring par Prometheus.
  - **Installation en tant que pod**  
    La décision d'installer le service en tant que pod a été prise afin de faciliter le déploiement du service. L'avantage est que le service Prometheus est déployé plus rapidement et s'appuiera sur une méthodologie GitOPS.
- **Azure Monitor**
  - **Choix d'Azure Monitor**  
    La solution Azure Monitor a été retenue car il s'agit de la solution native de monitoring pour la plateforme cloud Azure. De plus, elle est directement intégrable à Grafana via l'installation d'un plugin.

## Responsable d'implémentation

Le responsable de l'implémentation de la stack monitoring est **Quentin ADRENOY**.

Contacter le responsable d'implémentation :

- [Teams](https://teams.microsoft.com/l/chat/0/0?users=quentin.ardenoy@diiage.org)
- [Mail](mailto:admin@w3schools.io)

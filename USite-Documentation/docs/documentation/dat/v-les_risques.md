---
layout: Part
author: "L'équipe uSite"
---

# V. Les risques

## V.a. Risques sur l'environnement on-premises

1. **Pare-feu pfSense** :
    - **R1** - Attaques par déni de service (DoS) et attaques par déni de service distribué (DDoS)
    - **R2** - Tentatives de contournement du pare-feu et d'exploitation de vulnérabilités
    - **R3** - Configuration incorrecte ou insuffisante des règles de pare-feu
    - **R4** - Compromission des identifiants d'accès administratifs

   ![Matrice des risques Pare-feu pfSense](/assets/images/rgpd/matrice1.svg)

2. **Serveur Proxmox** :
    - **R1** - Exploitation des vulnérabilités logicielles
    - **R2** - Accès non autorisé aux machines virtuelles et aux données
    - **R3** - Contournement de l'isolement des machines virtuelles
    - **R4** - Compromission des identifiants d'accès administratifs

   ![Matrice des risques Serveur Proxmox](/assets/images/rgpd/matrice2.svg)

3. **Machines virtuelles Debian** :
   - **R1** - Accès non autorisé par SSH
   - **R2** - Exploitation des vulnérabilités logicielles
   - **R3** - Attaques de type "man-in-the-middle" et interception des communications

   ![Matrice des risques Machines virtuelles Debian](/assets/images/rgpd/matrice3.svg)

4. **Prometheus**
   - **R1** - Accès non autorisé aux données de métriques
   - **R2** - Exploitation des vulnérabilités de configuration
   - **R3** - Compromission des identifiants d'accès

   ![Matrice des risques Prometheus](/assets/images/rgpd/matrice4.svg)

## V.b. Risques sur l'environnement Azure

1. **Azure Container Registry** :
   - **R1** - Accès non autorisé aux images Docker

   ![Matrice des risques Azure Container Registry](/assets/images/rgpd/matrice5.svg)

2. **Cluster Azure Kubernetes Service** :
   - **R1** - Accès non autorisé aux ressources du cluster
   - **R2** - Exploitation des vulnérabilités dans les applications déployées
   - **R3** - Attaques internes par des applications malveillantes ou compromises

   ![Matrice des risques Cluster Azure Kubernetes Servic](/assets/images/rgpd/matrice6.svg)

3. **Azure SQL Database** :
   - **R1** - Accès non autorisé à la base de données
   - **R2** - Attaques par injection SQL
   - **R3** - Exploitation des vulnérabilités de sécurité dans la base de données

   ![Matrice des risques Azure SQL Database](/assets/images/rgpd/matrice7.svg)

### Risques sur les autres services

1. **Zabbix** :
   - **R1** - Accès non autorisé aux données de surveillance et de journalisation
   - **R2** - Exploitation des vulnérabilités dans l'intégration avec d'autres services
   - **R3** - Compromission des identifiants d'accès

   ![Matrice des risques Zabbix](/assets/images/rgpd/matrice8.svg)

2. **Azure DevOPS** :
   - **R1** - Accès non autorisé aux données et aux communications
   - **R2** - Compromission des identifiants d'accès
   - **R3** - Exploitation des vulnérabilités logicielles et de configuration

   ![Matrice des risques Azure DevOPS](/assets/images/rgpd/matrice9.svg)

3. **Grafana Cloud**
   - **R1** - Accès non autorisé aux tableaux de bord
   - **R2** - Exploitation d'une faille de sécurité lors du transfert des données entre les services
   - **R3** - Compromission des identifiants d'accès
   - **R4** - Exploitation des vulnérabilités dans l'intégration avec des plugins

   ![Matrice des risques Grafana Cloud](/assets/images/rgpd/matrice10.svg)

## V.c. Couverture actuelle des risques

Dans cette section, nous allons évaluer la couverture actuelle concernant les risques recensés au niveau de notre infrastructure. Afin d'organiser le travail, nous allons procéder par actif.

### Environnement On-Premises

- **Pare-feu pfSense**
  - ⚠️ **Attaques par déni de service (DoS) et attaques par déni de service distribué (DDoS)**

      Pour l'heure, aucun élément ne permet de protéger l'actif de ce risque.

  - **Tentatives de contournement du pare-feu et d'exploitation de vulnérabilités**
  
      Le pare-feu pfSense a été configuré selon le [guide des meilleures pratiques](https://docs.netgate.com/pfsense/en/latest/firewall/best-practices.html) fourni par l'éditeur du système d'exploitation. De plus, les services installés ont été réduits au strict nécessaire, réduisant ainsi la surface d'attaque.

      Une politique de mise à jour régulière de l'équipement a été instaurée. Nous sommes alertés lorsqu'une nouvelle mise à jour est disponible.

      Une veille technologique est maintenue afin d'être informé en cas de découverte d'une vulnérabilité.

  - **Configuration incorrecte ou insuffisante des règles de pare-feu**

      Le pare-feu constitue la seule porte d'entrée vers notre infrastructure. Un service VPN a été configuré et seules les personnes disposant d'accès explicites peuvent s'y connecter et accéder à l'infrastructure. Le trafic entrant est limité grâce à des règles de pare-feu et est activement contrôlé grâce au contrôle des logs.

      Le pare-feu fait l'interface entre plusieurs réseaux :

        - Le réseau du DIIAGE en 10.4.0.0/16

        - Le réseau publique
        
        - Le réseau privé en 172.16.0.0/16

      L'ensemble du trafic en provenance de ces réseaux est réduit au strict minimum.

  - ⚠️ **Compromission des identifiants d'accès administratifs**

      Pour l'heure, aucun élément ne permet de protéger l'actif de ce risque.

- **Serveur Proxmox**

  - **Exploitation des vulnérabilités logicielles**

      Une politique de mise à jour régulière de l'équipement a été instaurée. Nous sommes alertés lorsqu'une nouvelle mise à jour est disponible.

      Une veille technologique est maintenue afin d'être informé en cas de découverte d'une vulnérabilité.

  - **Accès non autorisé aux machines virtuelles et aux données**

      Renforcement de la sécurité d'accès aux machines virtuelles en utilisant des mécanismes d'authentification solides, tels que l'authentification par clé.

      Une gestion rigoureuse des privilèges a été mise en place pour limiter l'accès aux machines virtuelles et aux données. De plus, la connexion SSH en tant que root a été désactivée afin d'accorder uniquement les privilèges nécessaires à chaque utilisateur en fonction de leurs responsabilités.

  - ⚠️ **Contournement de l'isolement des machines virtuelles**

      Pour l'heure, aucun élément ne permet de protéger l'actif de ce risque.

  - **Compromission des identifiants d'accès administratifs**

      L'authentification à deux facteurs est un élément obligatoire lorsqu'un utilisateur tente de se connecter au serveur.

- **Machines virtuelles Debian**

  - **Accès non autorisé par SSH**

      Des règles de pare-feu ont été appliquées sur l'ensemble des machines virtuelles afin de limiter les accès SSH.

      Ainsi, seuls les clients connectés en VPN au pare-feu pfSense, soit le réseau `192.168.0.0/24`, peuvent se connecter en SSH en les machines.

  - **Exploitation des vulnérabilités logicielles**

      Une politique de mise à jour régulière de l'équipement a été instaurée. Nous sommes alertés lorsqu'une nouvelle mise à jour est disponible.

      Une veille technologique est maintenue afin d'être informé en cas de découverte d'une vulnérabilité.

  - **Attaques de type "man-in-the-middle" et interception des communications**

      Utilisation de connexions sécurisées (HTTPS) pour les transferts de données sensibles.

      Mise en place de certificats SSL/TLS valides pour garantir l'authenticité des serveurs et prévenir des attaques de type "man-in-the-middle".

- **Prometheus**

  - **Accès non autorisé aux données de métriques**

      Mise en place d'authentification sur l'API et l'interface utilisateur de Prometheus.

  - **Exploitation des vulnérabilités de configuration**

      Une stratégie de veille technologique est mise en place afin de s'informer des dernières vulnérabilités sorties sur la plateforme.

  - **Compromission des identifiants d'accès**

      Les comptes peuvent être désactivés lorsque les identifiants d'un utilisateurs sont compromis.

### Environnement cloud Azure

- **Azure Container Registry**

  - **Accès non autorisé aux images Docker**

      Seules les actifs suivants ont accès aux images hébergées dans l'Azure Container Registry :

        - Le cluster Kubernetes hébergé sur site

        - Le cluster AKS hébergé sur Azure

      Les managed identities ont été utilisés pour assurer un accès sécurisé aux images.

- **Cluster Azure Kubernetes Service**

  - **Accès non autorisé aux ressources du cluster**

      La connexion au service AKS est exclusivement possible grâce à un fichier de configuration unique et nominatif.

      Seules les personnes explicitement autorisées et qui disposent du fichier de configuration mentionné peuvent accéder au cluster.

  - ⚠️ **Exploitation des vulnérabilités dans les applications déployées**

      Pour l'heure, aucun élément ne permet de protéger l'actif de ce risque.

  - **Attaques internes par des applications malveillantes ou compromises**

      Séparation des charges de travail sensibles en utilisant des espaces de noms (namespaces) distincts.

- **Azure SQL Database** :

  - **Accès non autorisé à la base de données**

      Mise en place de connexions sécurisées en utilisant SSL/TLS pour chiffrer les communications avec la base de données.

      L’authentification de base de données SQL a été activée et configurée afin de renforcer la sécurité de la base de données en s'assurant que seuls les utilisateurs authentifiés et autorisés peuvent accéder aux données.

  - **Attaques par injection SQL**

      L’audit de base de données a été activée et une surveillance accrue des logs est réalisé afin de d'avoir une vue sur les activités de base de données en cours, d’analyser et d’examiner l’historique des activités pour identifier les menaces potentielles ou les violations de sécurité et abus présumés.

  - **Exploitation des vulnérabilités de sécurité dans la base de données**

      Le principe du moindre privilège proposant d'accorder uniquement les autorisations nécessaires aux utilisateurs et de limiter l'accès aux données sensibles a pu être respecté grâce à la configuration des rôles intégrés à Azure (RBAC Azure).

### Les services externes

- **Zabbix**

  - **Accès non autorisé aux données de surveillance et de journalisation**

      Le service Zabbix a été configuré de sorte à ce qu'une authentification forte soit activé sur tous les comptes.

      De plus, seuls les comptes exclusivement autorisés à accéder à la plateforme et ayant les droits spécifiques peuvent accéder à la plateforme.

  - **Exploitation des vulnérabilités dans l'intégration avec d'autres services**

      Des comptes dédiés avec des accès restreints ont été créés afin de permettre aux différences services de communiquer entre eux.

      Une veille technologique est maintenue afin d'être informé en cas de découverte d'une vulnérabilité.

  - **Compromission des identifiants d'accès**

      Les comptes peuvent être désactivés lorsque les identifiants d'un utilisateurs sont compromis.

      De plus, une politique de changement de mot de passe a été enforcée afin de garantir une rotation de mots de passes tous les **90 jours calendaires**.

- **Grafana Cloud**

  - **Accès non autorisé aux tableaux de bord**

      Une gestion stricte des accès à été implémentée en utilisant des rôles et des autorisations appropriées pour les utilisateurs.

  - **Exploitation d'une faille de sécurité lors du transfert des données entre les services**

      Les communications entre les différentes solutions de supervision et Grafana sont chiffrées à l'aide du protocole HTTPS pour assurer la confidentialité et l'intégrité des données.

  - **Compromission des identifiants d'accès**

      Une authentification forte à pu être mise en place et les comptes peuvent être désactivés lorsque les identifiants d'un utilisateurs sont compromis.

  - **Exploitation des vulnérabilités dans l'intégration avec des plugins**

      Une veille technologique est maintenue afin d'être informé en cas de découverte d'une vulnérabilité.

      De plus, nous nous sommes limité à l'utilisation de plugins provenant de sources fiables et vérifiées.

- **Azure DevOPS**

  - **Accès non autorisé aux données**

      Seuls les comptes explicitement autorisés et ajoutés dans l'organisation DevOPS peuvent accéder à la plateforme.

  - **Compromission des identifiants d'accès**

      Une politique de changement de mot de passe a été enforcée afin de garantir une rotation de mots de passes tous les **90 jours calendaires**.

  - **Exploitation des vulnérabilités logicielles et de configuration**

      Une stratégie de veille technologique est mise en place afin de s'informer des dernières vulnérabilités sorties sur la plateforme.

## V.d. Mesures principales implémentées

Pour atténuer les risques identifiés, les mesures de sécurité suivantes ont été implémentées sur uSite :

1. Authentification forte : Une authentification forte est requise pour accéder aux comptes d'administrateurs et aux systèmes critiques, de plus une rotation des mots de passe a été configurée.

2. Chiffrement des données en transit : Les communications entre le navigateur de l'utilisateur et le serveur uSite sont chiffrées à l'aide du protocole HTTPS pour assurer la confidentialité et l'intégrité des données.

3. Contrôle d'accès basé sur les rôles (RBAC) : Un système de contrôle d'accès basé sur les rôles Azure a été mis en place pour garantir que seules les personnes autorisées ont accès aux services Azure mise en production.

4. Mise en place de logiciels à jour : Les systèmes et les logiciels utilisés par uSite disposent des dernières mises à jour avec les derniers correctifs de sécurité pour atténuer les vulnérabilités connues.

## Références

- [Netgate - Guide des best practices de configuration de pfSense](https://docs.netgate.com/pfsense/en/latest/firewall/best-practices.html)
- [Microsoft - Gouvernance, risque et conformité](https://learn.microsoft.com/fr-fr/azure/well-architected/security/design-governance)
- [Prometheus - Les recommandations de sécurité générale](https://prometheus.io/docs/operating/security/)
- [Microsoft - Vue d’ensemble des capacités de sécurité d’Azure SQL Database](https://learn.microsoft.com/fr-fr/azure/azure-sql/database/security-overview?view=azuresql)
- [Microsoft - Authentification et autorisation dans Azure Devops](https://learn.microsoft.com/fr-fr/azure/devops/organizations/security/about-security-identity?view=azure-devops)
- [Grafana Labs - Configurer la sécurité](https://grafana.com/docs/grafana/latest/setup-grafana/configure-security/)
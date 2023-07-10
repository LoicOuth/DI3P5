---
layout: Part
author: "L'équipe uSite"
---

# IV. Décisions architecturales et fonctionnelles

[[toc]]

## Décision d'héberger l'environnement de Dev/Test sur un serveur Proxmox isolé

| Date       | Sprint   | Participants                             |
|------------|----------|------------------------------------------|
| 07/09/2022 | Sprint 0 | Equipe OPS et Equipe de développement    |

**Contexte:** Le projet exigeant un haut niveau de sécurisation, nous devions trouver un moyen d'isoler complètement notre infrastructure du réseau de notre provider (cf: DIIAGE).

**Options envisagées:**

- Intégrer notre infrastructure au réseau existant partagé (DIIAGE)
- Isoler notre infrastructure sur une machine déconnectée du réseau du réseau partagé
- Mettre en place un hyperviseur et y implémenter notre propre réseau dédié

**Décision:** Nous avons opté pour la dernière option, à savoir utioliser notre hyperviseur dédié tout en restant connecté au réseau partagé.

**Raisons:** Nous devions nous conformer aux standards de sécurité que nous avons adopté dès le début du projet.

**Impact:** Cette décision nous a permis de contrôler le trafic entrant et sortant de notre infrastructure et de garantir que seules les personnes ayant des accès explicites pouvaient y accéder. Cela nous a également permis de réaliser des tests rapidement dans un environnement contrôlé par nos soins.

## Décision d'implémenter un pare-feu pfSense faisant le pont entre notre réseau isolé et les réseaux partagé et public

| Date       | Sprint   | Participants                             |
|------------|----------|------------------------------------------|
| 07/09/2022 | Sprint 0 | Equipe OPS et Equipe de développement    |

**Contexte:** En réponse à la décision précédente d'isoler notre infrastructure des réseaux potentiellement dangereux, nous devions trouver un moyen de garantir l'accès à notre réseau de manière sécurisée. De plus, spécifiquement à notre environnement de travail, nous ne pouvions plus travailler sur l'infrastructure entre 22h et 6h le lendemain, ce qui pouvait nous retarder dans nos jalons.

**Options envisagées:**

- Intégrer un pare-feu physique ou un routeur physique à notre hyperviseur
- Implémenter une machine virtuelle faisant office de pare-feu entre les 3 différents réseaux

**Décision:** Nous avons décider de virtualiser la solution pfSense afin de filtrer le trafic réseau et de nous fournir une passerelle VPN vers notre infrastructure.

**Raisons:** L'équipe OPS a une certaine expérience avec la solution pfSense. De plus, la solution est polyvalente, simple à maintenir et peu consommatrice de ressources.

**Impact:** Cette décision nous a permis de contrôler le trafic entrant et sortant de notre infrastructure et de garantir que seules les personnes ayant des accès explicites pouvaient y accéder. Cela nous a également permis de réaliser des tests rapidement dans un environnement contrôlé par nos soins, et ce sans être bloqué par des impératifs de temps.

## Décision de mettre en place une architecture Kubernetes

| Date       | Sprint   | Participants                             |
|------------|----------|------------------------------------------|
| 07/09/2022 | Sprint 0 | Equipe OPS et Equipe de développement    |

**Contexte:** Dans l'optique de produire une infrastructure efficiente, nous cherchions une technologie capable de n'utiliser que les ressources strictement nécessaires pour desservir les sites de nos clients.

**Options envisagées:**

- Implémenter une stack applicative sur un serveur unique (application monolithique)
- Découper les différentes fonctions principales de l'application (backend, site de présentation, CMS) et les héberger sur des serveurs différents
- Utiliser un orchestrateur pour déployer la stack applicative

**Décision:** Nous avons décidé de mettre en place un cluster Kubernetes pour gérer le déploiement de nos applications.

**Raisons:** Nous avions besoin d'une solution résiliente qui nous permettrait de monter en charge facilement et rapidement pour supporter d'éventuels pics de charge.

**Impact:** Cette décision a conduit à une réduction significative des coûts, tout en maintenant un niveau élevé de performances. Il aura cependant été nécessaire de conteneuriser l'ensemble des composants applicatifs, Kubernetes ne supportant que les conteneurs.

## Décision au sujet des tests au sein de notre application

| Date       | Sprint   | Participants                 |
|------------|----------|------------------------------|
| 07/09/2022 | Sprint 0 | Equipe de développement      |

**Contexte:** Au fur et à mesure que notre codebase s'agrandit, nous reconnaissons le besoin de garantir la qualité du code et de minimiser les régressions. Nous avons donc décidé d'intégrer des tests unitaires et des tests de bout en bout (E2E) dans notre pipeline de build et nos pull requests.

**Décision:** Nous avons décidé d'intégrer les tests unitaires et E2E à la fois dans la pipeline de build et dans les pull requests.

**Raisons:** En intégrant les tests dans la pipeline de build et dans les pull requests, nous pouvons nous assurer que chaque modification de code est vérifiée pour la qualité et l'absence de régressions avant d'être fusionnée. Cela nous permet de détecter et de corriger les problèmes plus tôt dans le cycle de développement, ce qui est plus efficace et coûte moins cher que de les corriger après la fusion.

**Impact:** Cette décision nécessitera un travail initial pour intégrer les tests dans notre pipeline de build et configurer notre système de gestion de versions pour exécuter les tests sur chaque pull request. Cependant, nous prévoyons que cela améliorera la qualité de notre code et réduira le nombre de bugs dans notre application en production.

**Quoi tester:**

Après avoir décidé d'intégrer les tests unitaires et les tests E2E dans notre pipeline de build et nos pull requests, nous avons dû déterminer exactement où ces tests devraient être appliqués.

Nous avons décidé de faire des tests unitaires uniquement sur la couche application dans le backend, et de faire des tests E2E pour tester les interfaces dans le frontend.

Nous avons choisi de limiter les tests unitaires à la couche application du backend car nous pensons que c'est là que la majorité de notre logique métier réside et que c'est donc là que les tests auront le plus d'impact. En ce qui concerne le frontend, nous avons décidé de réaliser des tests E2E sur les interfaces, car c'est là que l'utilisateur interagit avec notre application, et il est donc crucial de s'assurer que ces interactions fonctionnent comme prévu.

## Décision sur l'Utilisation d'Identity Server pour Gérer l'Authentification

| Date       | Sprint   | Participants                             |
|------------|----------|------------------------------------------|
| 28/10/2022 | Sprint 1 | Equipe OPS et Equipe de développement    |

**Contexte:** Dans le cadre de notre besoin de gérer l'authentification des utilisateurs dans notre application, nous avons examiné plusieurs options pour un système d'authentification.

**Options envisagées:**

- Utiliser Keycloak, une solution open-source d'authentification et de gestion des identités.
- Coder notre propre serveur d'authentification.
- Utiliser Identity Server, une solution open-source pour ASP.NET Core.

**Décision:** Nous avons décidé d'utiliser Identity Server pour gérer l'authentification dans notre application.

**Raisons:** Plusieurs facteurs ont contribué à cette décision. Premièrement, Identity Server est une solution bien établie avec une large base d'utilisateurs, ce qui nous donne confiance dans sa fiabilité et sa sécurité. Deuxièmement, comme nous utilisons déjà ASP.NET Core pour notre backend, Identity Server s'intègre bien dans notre stack technologique existante. Enfin, coder notre propre serveur d'authentification serait un défi significatif et prendrait du temps et des ressources loin du développement d'autres fonctionnalités importantes de notre application.

**Impact:** Cette décision signifie que nous devrons passer du temps à intégrer Identity Server dans notre application et à le configurer pour répondre à nos besoins. Cependant, nous pensons que les avantages de l'utilisation d'une solution d'authentification éprouvée et bien soutenue l'emportent sur les coûts.

## Décision de Passer d'une Base de Données Non Relationnelle à une Base de Données Relationnelle

| Date       | Sprint   | Participants                             |
|------------|----------|------------------------------------------|
| 28/10/2022 | Sprint 1 | Equipe de développement    |

**Contexte:** Nous développons un système de gestion de contenu (CMS) et avons initialement choisi d'utiliser une base de données non relationnelle. Cependant, au fur et à mesure de l'avancement du projet, nous avons commencé à reconsidérer cette décision.

**Options envisagées:**

- Continuer à utiliser une base de données non relationnelle.
- Passer à une base de données relationnelle.

**Décision:** Nous avons décidé de passer à une base de données relationnelle.

**Raisons:** Plusieurs facteurs ont contribué à cette décision. Premièrement, nous avons constaté que pour notre CMS, une base de données relationnelle est plus adaptée à notre structure de données, qui est fortement axée sur les relations entre les différentes entités. Deuxièmement, nous avons trouvé que la gestion des transactions et l'intégrité des données sont plus simples avec une base de données relationnelle. Enfin, nous avons estimé que l'utilisation d'une base de données relationnelle serait plus intuitive pour notre équipe, compte tenu de notre expérience et de nos compétences existantes.

**Impact:** Comme nous n'avons pas encore commencé à coder, nous n'aurons pas à faire face aux coûts de migration des données ou de réécriture du code pour s'adapter à ce changement. Nous pensons que les avantages à long terme de cette décision, y compris une meilleure intégrité des données et une facilité d'utilisation, l'emportent sur les coûts initiaux.

## Décision d'Utiliser OVH pour la Réservation des Noms de Domaine et Sous-domaine

| Date       | Sprint   | Participants                             |
|------------|----------|------------------------------------------|
| 02/12/2022 | Sprint 1 | Equipe OPS et Equipe de développement    |

**Contexte:** Nous avions besoin d'un service pour la réservation de noms de domaine et de sous-domaines pour nos sites clients.

**Options envisagées:**

- Utiliser d'autres fournisseurs de services de noms de domaine.
- Utiliser OVH pour la réservation de noms de domaine et de sous-domaines.

**Décision:** Nous avons décidé d'utiliser OVH pour la réservation de noms de domaine et de sous-domaines.

**Raisons:** Nous avons choisi OVH car il dispose d'une API simple à utiliser. Cette simplicité nous a permis d'intégrer facilement la réservation de noms de domaine et de sous-domaines dans nos processus de déploiement et de gestion des sites clients.

**Impact:** Cette décision a simplifié nos processus de réservation de noms de domaine et de sous-domaines. Grâce à l'API OVH, nous avons été en mesure d'automatiser ces processus, ce qui a réduit le temps et l'effort nécessaires pour gérer les noms de domaine et les sous-domaines de nos sites clients.

## Décision de Déployer les Sites Clients sur un PVC Kubernetes et Mise à Jour de la Configuration Nginx

| Date       | Sprint   | Participants                             |
|------------|----------|------------------------------------------|
| 13/02/2023 | Sprint 2 | Equipe OPS et Equipe de développement    |

**Contexte:** Dans le cadre de notre stratégie de déploiement pour les sites clients, nous cherchions un moyen de limiter les coûts tout en maintenant de bonnes performances.

**Options envisagées:**

- Utiliser une approche traditionnelle de déploiement avec une instance de serveur distincte pour chaque site client.
- Déployer les sites clients sur un Persistent Volume Claim (PVC) Kubernetes et mettre à jour la configuration Nginx.

**Décision:** Nous avons décidé de déployer les sites clients sur un PVC Kubernetes et de mettre à jour la configuration Nginx.

**Raisons:** Cette décision nous a permis de réduire les coûts en évitant le besoin de provisionner et de gérer une instance de serveur distincte pour chaque site client. De plus, grâce à l'utilisation de PVC Kubernetes, nous avons pu bénéficier de la flexibilité, de la scalabilité et de la résilience offertes par Kubernetes. La mise à jour de la configuration Nginx a permis d'optimiser les performances.

**Impact:** Cette décision a conduit à une réduction significative des coûts, tout en maintenant un niveau élevé de performances. Cela a également nécessité un certain travail initial pour mettre en place et configurer l'environnement PVC Kubernetes et la configuration Nginx, mais ces efforts ont été jugés justifiés par les avantages à long terme.

## Décision de Déployer l'environnement de Prod sur Azure

| Date       | Sprint   | Participants                             |
|------------|----------|------------------------------------------|
| 13/02/2023 | Sprint 2 | Equipe OPS et Equipe de développement    |

**Contexte:** Notre projet était prêt et déployé sur les environnements de Dev/Test. Nous pouvions commencer l'implémentation Cloud de l'environement de prod.

**Options envisagées:**

- Utiliser le cloud provider OVH
- Utiliser le cloud provider Azure

**Décision:** Nous avons décidé de déployer l'environement de Prod chez le cloud provider Azure

**Raisons:** A cause des contraintes de temps, nous avons dû prendre la décision de rester sur notre domaine d'expertise en matière de services Cloud. De plus, l'équipe OPS est familière avec tous les types de déploiement de ce cloud provider, et notammenet les déploiements de type IaC.

**Impact:** Cette décision nous a permis de nous concentrer sur l'essentiel lors du déploiement de l'infrastructure dans le cloud. Nous avons ainsi pu rapidement produire tous les éléments nécessaires au déploiement de cet environnement.

## Décision d'Utiliser Tailwind CSS pour la Génération du CSS

| Date       | Sprint   | Participants                             |
|------------|----------|------------------------------------------|
| 13/02/2023 | Sprint 2 | Equipe de développement    |

**Contexte:** Nous avions besoin d'une solution pour gérer le CSS de nos sites clients, qui pourrait simplifier le processus de développement et améliorer les performances lors de l'affichage.

**Options envisagées:**

- Écrire et gérer le CSS nous-mêmes.
- Utiliser un autre framework CSS.
- Utiliser Tailwind CSS pour la génération du CSS.

**Décision:** Nous avons décidé d'utiliser Tailwind CSS pour la génération du CSS.

**Raisons:** Avec Tailwind CSS, nous n'avons pas besoin de gérer le CSS nous-mêmes. Lors de la construction de la pipeline d'un client, un projet npm est instancié et Tailwind est installé. Grâce à Tailwind, nous générons uniquement le CSS nécessaire pour chaque site, ce qui améliore les performances lors de l'affichage.

**Impact:** Cette décision a permis de simplifier notre processus de développement et a amélioré les performances de nos sites clients. En utilisant Tailwind CSS, nous avons été en mesure de réduire la taille de notre CSS, ce qui a accéléré le chargement des pages et amélioré l'expérience utilisateur.

## Décision d'Utiliser Azure DevOps pour la Gestion des Sites Clients

| Date       | Sprint   | Participants                             |
|------------|----------|------------------------------------------|
| 09/03/2023 | Sprint 3 | Equipe de développement    |

**Contexte:** Nous avions besoin d'une solution pour gérer les sites de nos clients, depuis la génération du site en HTML jusqu'à sa mise en ligne.

**Options envisagées:**

- Gérer les déploiements dans notre code via ssh
- Utiliser Azure DevOps pour gérer les sites clients.

**Décision:** Nous avons décidé d'utiliser Azure DevOps pour gérer les sites de nos clients.

**Raisons:** Azure DevOps offre une intégration robuste avec les API, ce qui nous permet de créer et d'automatiser des pipelines pour chaque site client. Une fois le site généré en HTML, le code est mis sur un repo sur Azure DevOps grâce aux API, une pipeline est créée et ensuite exécutée. C'est dans cette pipeline que le site et la configuration Nginx sont mis à jour sur le PVC. De plus grâce à cette solution nous avons un système de retour en arrière directement intégrer et nous n'avons pas besoin de le gérer autrement.

**Impact:** Cette décision a permis une automatisation efficace du processus de déploiement des sites clients. L'utilisation d'Azure DevOps a également facilité la gestion et le suivi des déploiements, grâce à ses outils intégrés de gestion des versions et de suivi des problèmes.

## Décision sur le choix de l'architecture de monitoring

| Date       | Sprint   | Participants                             |
|------------|----------|------------------------------------------|
| 14/03/2023 | Sprint 3 | Equipe OPS    |

**Contexte:** Pour garantir une gestion optimale de nos infrastructures de dev, de test et de prod, nous avions besoin d'un système de monitoring performant et adapté à nos besoins spécifiques.

**Options envisagées:**

- Utiliser une solution open source comme Zabbix, ELK, Prometheus, Zipkin, Loki, Nagios, PRTG, ou Pandora FMS.
- Utiliser une solution propriétaire comme Datadog ou Azure Monitor.
- Créer une solution de monitoring interne.

**Décision:** Nous avons choisi de combiner plusieurs solutions pour répondre à nos besoins diversifiés :

- Zabbix, hébergé on-premises, pour le monitoring spécifique de l'environnement sur site, comprenant l'ensemble des machines virtuelles.
- Prometheus, déployé en tant que pod dans Kubernetes, pour le monitoring avancé de Kubernetes.
- Azure Monitor, pour le monitoring des services Azure.
- Grafana, hébergé sur une plateforme cloud, pour la centralisation du monitoring.

**Raisons:**

- Zabbix offre une grande diversité de monitoring et est intégrable à Grafana.
- Prometheus est la solution de référence pour le monitoring de Kubernetes.
- Azure Monitor est parfaitement adapté pour les services Azure.
- Grafana permet de centraliser et de visualiser les données de monitoring de toutes les autres solutions.

**Impact:** Cette décision a permis d'obtenir une visibilité complète sur l'ensemble de notre infrastructure, de faciliter le dépannage et d'améliorer la réactivité face aux problèmes. En combinant plusieurs solutions, nous avons été en mesure de tirer parti des forces de chacune d'elles, ce qui a amélioré l'efficacité de notre monitoring.

## Décision de Passer d'une Base de Données PostgreSQL à Microsoft SQL Server en Production

| Date       | Sprint   | Participants                             |
|------------|----------|------------------------------------------|
| 29/04/2023 | Sprint 3 | Equipe OPS et Equipe de développement  |

**Contexte:** Pour nos environnements, nous utilisions initialement une base de données PostgreSQL. Cependant, nous avons rencontré des problèmes avec les migrations Entity Framework sur cette base de données.

**Options envisagées:**

- Continuer à utiliser PostgreSQL et essayer de résoudre les problèmes de migration.
- Passer à Microsoft SQL Server.

**Décision:** Nous avons décidé de passer à Microsoft SQL Server pour nos environnements.

**Raisons:** Nous avons décidé de faire ce changement car nous n'étions pas en mesure de résoudre les problèmes de migration avec Entity Framework sur PostgreSQL. Microsoft SQL Server, étant un produit Microsoft comme Entity Framework, a démontré une meilleure compatibilité avec notre stack technologique.

**Impact:** Cette décision a eu un impact significatif sur notre équipe Ops, qui a dû déployer une instance de Microsoft SQL Server sur leur cluster AKS. Cela a nécessité un investissement de temps et de ressources pour la configuration et le déploiement. Cependant, maintenant que le nouvel environnement de base de données est en place, nous nous attendons à ce que les problèmes de migration soient résolus et que notre processus de déploiement soit plus fluide.

## Décision d'Utiliser Azure File Storage pour Stocker les Images des Sites Clients

| Date       | Sprint   | Participants                             |
|------------|----------|------------------------------------------|
| 15/05/2023 | Sprint 4 |  Equipe OPS et Equipe de développement   |

**Contexte:** Nous avions besoin d'une solution pour stocker les images de nos sites clients, qui serait facile à utiliser et performante.

**Options envisagées:**

- Utiliser un autre service de stockage de fichiers.
- Gérer notre propre solution de stockage de fichiers.
- Utiliser Azure File Storage pour le stockage des images.

**Décision:** Nous avons décidé d'utiliser Azure File Storage pour stocker les images de nos sites clients.

**Raisons:** Grâce aux API d'Azure, cela nous a simplifié la tâche. De plus, le stockage des fichiers est simple et l'appel des fichiers est plus performant avec Azure File Storage.

**Impact:** Cette décision a facilité notre processus de gestion des images et a amélioré les performances de nos sites clients. En utilisant Azure File Storage, nous avons été en mesure de stocker et de récupérer les images de manière plus efficace, ce qui a accéléré le chargement des pages et amélioré l'expérience utilisateur.

---
layout: Part
author: "Quentin ARDENOY"
---

# Déploiement des ressources Azure

[[toc]]

## Liste des ressources Azure par stack
Cette documentation vise à fournir un aperçu global du processus de déploiement des ressources Azure dans notre environnement de production. Elle a pour objectif de vous guider à travers les étapes essentielles nécessaires pour mettre en place notre infrastructure sur la plateforme Azure.

Lors de ce déploiement, nous utilisons des scripts Bicep, qui nous permettent de décrire notre infrastructure de manière programmable et reproductible. En adoptant une approche d'Infrastructure as Code (IaC), nous bénéficions de nombreux avantages tels que la facilité de gestion, la cohérence et la traçabilité de notre infrastructure.

Cette documentation vous présentera les différentes "stacks" ou ensembles de ressources qui composent notre environnement de production. Chaque "stack" représente un ensemble spécifique de ressources qui interagissent entre elles pour répondre à des besoins fonctionnels particuliers. Dans cette documentation, nous explorerons chaque stack individuellement, en détaillant les ressources qui la composent et en expliquant leur rôle dans notre environnement.

En comprenant comment les différentes ressources s'articulent au sein de chaque stack, vous obtiendrez une vision globale de notre infrastructure Azure et de son fonctionnement. Que vous soyez un membre de l'équipe d'exploitation, un développeur ou un responsable de projet, cette documentation vous permettra de mieux appréhender l'ensemble des ressources déployées par stack.

### Monitoring Stack

Dans cette stack, nous mettons en place les éléments nécessaires pour surveiller les coûts et collecter les métriques et les logs de notre cluster AKS. Voici les ressources déployées :

- **Budget** : Un budget est créé pour surveiller les coûts liés à notre infrastructure. Des actions groupées SMS et email sont configurées pour générer des alertes en cas de dépassement du budget.

- **Espace de travail Log Analytics** : Un espace de travail Log Analytics est provisionné pour recueillir les métriques, les logs et les événements de notre cluster AKS. Cela nous permet de surveiller les performances, d'effectuer des analyses approfondies et de déceler les problèmes éventuels.

### Storage Stack

Cette stack est dédiée au stockage des différentes ressources utilisées par notre application. Voici les ressources déployées :

- **Registre de conteneurs** : Un registre de conteneurs est configuré pour stocker les images des conteneurs utilisés par notre application. Cela facilite la gestion et le déploiement des nouvelles versions de nos conteneurs.

- **Serveur SQL avec Azure SQL Database** : Un serveur SQL est déployé, et une base de données Azure SQL y est créée. Cette base de données est utilisée pour stocker les données persistantes de notre application. Une règle de pare-feu Azure est également configurée pour autoriser les autres services Azure à accéder à la base de données.

- **Compte de stockage** : Un compte de stockage est créé pour stocker les images des sites web utilisés par notre application. Ce compte de stockage offre une solution scalable et sécurisée pour héberger les fichiers statiques.

### AKS Stack

Dans cette stack, nous déployons et configurons le cluster AKS. Voici la ressource déployée :

- **Cluster AKS** : Un cluster AKS est provisionné pour exécuter nos conteneurs. Ce cluster est hautement disponible, scalable et géré par Azure. Il offre un environnement optimisé pour le déploiement de nos applications conteneurisées.

### Automation Stack

Dans cette stack, nous configurons l'automatisation des tâches de gestion de notre infrastructure. Voici la ressource déployée :

- **Compte d'automatisation avec un Runbook** : Un compte d'automatisation est créé et associé à un Runbook spécifique. Ce Runbook est conçu pour éteindre automatiquement le cluster AKS selon une planification prédéfinie. Cela nous permet d'optimiser l'utilisation des ressources et de réduire les coûts.

### Role Assignment Stack

Cette stack est dédiée à la gestion des rôles et des autorisations nécessaires au bon fonctionnement du cluster AKS. Voici les ressource déployée :

- **Attribution de rôles pour récupérer les images ACR** : Une identité managée est associée au cluster AKS, lui permettant ainsi de récupérer les images du registre de conteneurs. Cela facilite le déploiement de nouvelles versions de nos conteneurs sur le cluster AKS.

- **Attribution de rôles pour l'Automation Account** : Le rôle contributeur sur le cluster AKS est attribué à l'Automation Account pour lui permettre d'effectuer des opérations de gestion sur le cluster.

Ainsi, en combinant les ressources déployées par chaque stack, nous obtenons une infrastructure complète et fonctionnelle pour prendre en charge notre plateforme uSite sur Azure.

Pour déployer ces ressources, nous utilisons des scripts Bicep qui organisent les fichiers de déploiement dans une structure spécifique. Vous pouvez vous référer à la documentation technique suivante pour obtenir plus de détails sur les étapes spécifiques du déploiement.

## Structure des fichiers Bicep

L'utilisation de fichiers Bicep permet de décrire et de déployer nos ressources Azure de manière efficace et reproductible. La structure des fichiers Bicep est conçue de manière à faciliter la gestion et la compréhension de nos déploiements.

Dans notre approche, nous adoptons une structure de fichiers Bicep organisée, qui se compose des éléments suivants :

1. **Fichiers principaux (`step1.bicep` et `step2.bicep`)** : Habituellement, un seul fichier (main.bicep) serait suffisant pour le déploiement complet. Cependant, dans notre cas, nous avons choisi de diviser le déploiement en deux étapes distinctes pour des raisons techniques. Cette approche nous permet de gérer de manière logique notre déploiement, en gérant les dépendance spécifiques. Ainsi, nous assurons un déploiement efficace et cohérent de notre infrastructure Azure.

2. **Fichiers de paramètres JSON (`step1-parameters.json` et `step2-parameters.json`)** : Ces fichiers contiennent les valeurs des paramètres utilisés à chaque étapes du déploiement. Ils permettent de personnaliser les configurations et de les adapter en fonction de nos besoins spécifiques.

3. **Répertoire de modules** : Ce répertoire contient tous les fichiers de déploiement Bicep, organisés dans des sous-répertoires pour chaque stack. Par exemple, pour la stack **Storage**, nous aurons un répertoire dédié contenant les fichiers Bicep relatifs au conteneur de registre et à la base de données.

Cette structure modulaire facilite la gestion de nos déploiements en nous permettant de travailler de manière indépendante sur chaque stack. Chaque stack dispose de son propre répertoire et de ses fichiers Bicep spécifiques, ce qui facilite la compréhension et la maintenance des ressources associées.

Voici un exemple concret de la structure de fichiers Bicep avec des modules :

```
📁 modules/
├── 📁 aks/
│   ├── aks.bicep
├── 📁 automation/
│   ├── auto-account-update.bicep
│   ├── auto-account.bicep
├── 📁 monitoring/
│   ├── action-group.bicep
│   ├── budgets.bicep
│   ├── loganalytics.bicep
├── 📁 role-assignements/
│   ├── aks-acr.bicep
│   ├── auto-account-aks.bicep
├── 📁 storage-stack/
│   ├── container-registry.bicep
│   ├── sql-database.bicep
├── ...
step1-parameters.json
step1.bicep
step2-parameters.json
step2.bicep
```

Cette structure organisée et modulaire nous permet de gérer efficacement nos déploiements, en facilitant la réutilisation des modules, la maintenance et les mises à jour ultérieures.

En suivant cette structure et en comprenant la fonction de chaque fichier, vous serez en mesure de naviguer facilement à travers notre code Bicep et d'effectuer des déploiements cohérents et structurés de nos ressources Azure.
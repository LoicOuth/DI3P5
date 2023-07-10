---
layout: Part
author: "L'équipe uSite"
---

# I. Contexte

## I.a. Object

Ce document décrit l’architecture technique de l’application « USite » ainsi que le déploiement de cette architecture. L'application en elle-même est destinée au client et aux encadrants pédagogiques.

La présentation de l’architecture fonctionnelle « USite » reprend les concepts techniques sur lesquels l’application se base pour fonctionner.

## I.a. Présentation du contexte

De nos jours, tout se trouve sur le web. Toutes les informations dont nous avons besoin et aurons besoin s’y trouvent et nous pouvons y accéder en quelques clics. Puisque tout se trouve sur le web, tout un chacun devrait posséder un site, et ce pour diverses raisons :

- Présentation de son entreprise / établissement (par exemple : hôtellerie, restaurant, etc…)
- CV en ligne / portfolio
- Blog traitant de divers sujets
- Site de e-commerce
- …

Malheureusement, encore très peu de personnes possèdent aujourd’hui un site personnel ou professionnel car il s’agit d’un concept sur lequel le public reste très peu informé. Créer et gérer un site web reste un concept hors de portée pour beaucoup de personnes.

La démarche de proposer des sites fonctionnels au grand public est déjà existante sur le marché, notamment grâce à l’arrivée d’acteurs tels que Wix.com sorti en 2006 ou encore Site123.com, pour n’en citer que ces deux-là. Pour rendre les sites web le plus accessible possible, ces compagnies mettent à disposition de leurs clients des « templates », c’est-à-dire des sites déjà existants sur lesquels il est possible de modifier les images, le texte et les couleurs principales. De ce fait, quelques clics suffisent pour créer un site web et cela facilite grandement la transition vers le digital pour le grand public.

Malgré cela, la démarche pour créer son propre site web reste onéreux en comparaison avec la qualité et les performances. Ces dernières peuvent être améliorées pour toucher un maximum de personnes.

Par ailleurs, la concurrence ne propose aux clients que peu de personnalisation de leur site. Il est par exemple rare de pouvoir fusionner plusieurs templates de sites. Les interfaces de personnalisations pourraient être améliorées.

Enfin, les modèles gratuits incluent des publicités directement sur le site du client, ce qui peut nuire à son professionnalisme.

## I.c. Prérimètre

Le périmètre de ce document couvre les fonctionnalités relatives à la création, la gestion et la publication de sites web par nos clients finaux. Notre infrastructure héberge l'application et permet à nos utilisateurs de créer leur(s) propre(s) sites web de manière intuitive. La plateforme uSite offre aux clients finaux la possibilité :

* De créer des sites web
* De gérer le contenu de leur site web
* D'accéder à des fonctionnalités supplémentaires par la souscription d'un abonnement mensuel ou annuel
* De générer son site web à partir d'éléments templatisés

L'application est accessible via une connexion internet. L'interface de gestion des sites web permet aux utilisateurs de :

* Modifier le contenu de leur site
* Déployer les modifications
* Prévisualiser leur site avant la publication
* Gérer les aspects liés à la facturation et au compte utilisateur

## I.d. Les besoins fonctionnels

L'architecture technique décrite dans le présent DAT a pour objectif de répondre aux besoins suivants :

- Permettre aux utilisateurs finaux de connaître la plateforme en consultant un site de présentation du projet
- Permettre aux utilisateurs finaux de concevoir leur propre site web personnalisé
- Permettre aux utilisateurs finaux de déployer leur site une fois l'édition terminée
- Permettre aux utilisateurs finaux d'héberger leur site déployé
- Permettre aux utilisateurs finaux de gérer leur(s) site(s) (les éditer, les supprimer)
- Permettre aux visiteurs des utilisateurs d'accéder aux sites depuis n’importe quel endroit du globe

Une indisponibilité de ces services pourrait avoir les impacts suivants :

- Édition des sites web impossible
- Consultation des sites web déployés impossible

Ainsi, ces axes constitueront la ligne directrice de nos choix techniques.

## I.e. Les besoins non fonctionnels

Les besoins non fonctionnels correspondent aux contraintes techniques que l’architecture devra respecter

Le projet uSite a pour objectif de fournir à ses utilisateurs une plateforme no-code capable de réaliser rapidement des sites web personnalisés et de les héberger. Certaines contraintes techniques nous sont imposées et sont listées ci-dessous :

### I.e.1. Les différentes technologies utilisées par environnement

#### Environnement de Dev/Test

Les **technologies** suivantes sont implémentés :

| Element | Valeur | Remarques |
|---|---|---|
| Technologie d'orchestration | Kubernetes 1.24.11 | Kubernetes est le meilleur candidat pour permettre un déploiement rapide et hautement disponible de nos applications |
| SGBDR | Microsoft SQL Server 2019 Express Edition | Facilite les migrations de bases de données et maîtrise du SGBDR par l'équipe de développement |
| Serveur de base de données relationelle | Windows Server 2019 Standard | Compatibilité accrue avec le SGBD choisi |
| Base de données non relationelle | Redis 7.0.8 | Nécessaire pour l'event driving |
| Langage de programmation backend | .NET Core | |
| Langage de programmation backend | .NET Framework | Nécessaire pour la génération de fichiers à partir de templates T4 |
| Langage de programmation frontend pour le site de présentation | NuxtJS | Technologie hautement performante |
| Langage de programmation frontend pour la génération des sites clients | Angular | Permet de "templatiser" les sites clients en modules tout en garantissant une génération optimisée |
| Hébergement des images | Azure File Storage, type File Storage | Permet de garantir un accès hautement disponible des images |
| Hébergement du code source | Dépôts Git Azure DevOPS | Pleinement intégrés avec les pipelines Azure DevOPS |
| Technologie d'intégration et de déploiement en continu | Pipelines Azure DevOPS | Permet de centraliser tout le processus de livraison en un point (Azure DevOPS) |
| Téchnologie d'hébergement des sites clients | Nginx | Solution hautement disponible |
|  |  |  |

Voici les composants installés sur le cluster Kubernetes :

| Element | Valeur | Remarques |
|---|---|---|
| Technologie de communication réseau | Cilium 1.12.2 |  |
| Technologie d'accès réseau externe | MetalLB 0.13.7 |  |
| Technologie de stockage installée | Longhorn 1.3.2 |  |
| Technologie d'ingress | Nginx Ingress Controller 1.5.1  |  |
| Scraper de métrics | Prometheus 2.43.1 | Intégration facilitée avec Kubernetes |
| Technologie de monitoring interne | Metrics Server 3.10.0 | Nécessaire pour le fonctionnement des Horizontal Pod Autoscaler |
| Technologie de déploiement GitOPS | ArgoCD 2.6.0 |  |

#### Environnement de Prod

Les **technologies** suivantes sont implémentés :

| Element | Valeur | Remarques |
|---|---|---|
| Technologie d'orchestration | Azure Kubernetes Service (AKS) 1.24.11 | Kubernetes est le meilleur candidat pour permettre un déploiement rapide et hautement disponible de nos applications |
| SGBDR | Azure SQL Database | Facilite les migrations de bases de données et maîtrise du SGBDR par l'équipe de développement |
| Base de données non relationelle | Redis 7.0.8 | Nécessaire pour l'event driving |
| Langage de programmation backend | .NET Core | |
| Langage de programmation backend | .NET Framework | Nécessaire pour la génération de fichiers à partir de templates T4 |
| Langage de programmation frontend pour le site de présentation | NuxtJS | Technologie hautement performante |
| Langage de programmation frontend pour la génération des sites clients | Angular | Permet de "templatiser" les sites clients en modules tout en garantissant une génération optimisée |
| Hébergement des images | Azure File Storage, type File Storage | Permet de garantir un accès hautement disponible des images |
| Hébergement du code source | Dépôts Git Azure DevOPS | Pleinement intégrés avec les pipelines Azure DevOPS |
| Technologie d'intégration et de déploiement en continu | Pipelines Azure DevOPS | Permet de centraliser tout le processus de livraison en un point (Azure DevOPS) |
| Téchnologie d'hébergement des sites clients | Nginx | Solution hautement disponible |
|  |  |  |

Voici les composants installés sur le cluster Kubernetes :

| Element | Valeur | Remarques |
|---|---|---|
| Technologie d'ingress | Nginx Ingress Controller 1.5.1  |  |
| Technologie de monitoring interne | Metrics Server 3.10.0 | Nécessaire pour le fonctionnement des Horizontal Pod Autoscaler |
| Technologie de déploiement GitOPS | ArgoCD 2.6.0 |  |

#### Composants en commun

Certains composants ont été mis en commun pour tous les environnements pour assurer une centralisation de certains services.

| Element | Valeur | Remarques |
|---|---|---|
| Système de centralisation du monitoring d'infrastructure | Grafana Cloud  | Grafana Cloud permet de centraliser toutes les métrics des environnements en un point |

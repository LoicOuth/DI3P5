---
layout: Part
author: "Loïc OUTHIER"
---

# Explications

[[toc]]

## Introduction

Dans notre processus de création de sites web pour nos clients, nous utilisons des templates T4 pour générer les fichiers HTML correspondants. Tous les éléments du site sont stockés dans une base de données. Lorsque l'utilisateur souhaite effectuer un déploiement, notre backend reçoit la requête et exécute les tâches suivantes :

 1. Vérification d'un nom de domaine/sous-domaine et réservation de celui-ci
 
 2. Génération des fichiers HTML à partir des templates T4.

 3. Création d'un référentiel dédié au client dans Azure DevOps via l'API Azure DevOps.

 4. Push des fichiers HTML dans le référentiel dédié au client via l'API Azure DevOps.

 5. Création d'une pipeline pour ce référentiel via l'API Azure DevOps.

 6. Exécution de la pipeline via l'API Azure DevOps.

 7. Mise en place de la livraison continue (Continuous Deployment) pour envoyer les fichiers HTML et CSS générés sur un PVC (Persistent Volume Claim) dans Kubernetes.

 ## Vérification et réservation du nom de sous-domaine de usite.fr via l'API OVH

Avant de procéder à la création et au déploiement du site web, nous effectuons une vérification de disponibilité et réservons le nom de sous-domaine de usite.fr spécifié par le client. Nous utilisons l'API OVH pour effectuer cette opération.

1. Utilisez l'API OVH pour vérifier la disponibilité du nom de sous-domaine souhaité.

2. Si le nom de sous-domaine est disponible, procédez à la réservation en utilisant l'API OVH.

3. Lié le site et le nom de domaine dans la base de données afin de garder une trace

La vérification et la réservation du nom de sous-domaine de usite.fr garantissent que le site web aura une adresse personnalisée correspondant aux préférences du client.

## Génération des fichiers HTML avec des templates T4

Avant de procéder à la création du référentiel et à la génération des fichiers HTML, nous utilisons des templates T4 pour générer dynamiquement le contenu des pages du site web. Les templates T4 sont alimentés par les données stockées dans la base de données.

1. Un template T4 est utiliser à la base pour générer chacune des pages

2. Utilisez les données de la base de données pour générer les fichiers HTML à l'aide des templates T4.

## Création du référentiel Azure DevOps via l'API

Pour créer un référentiel dédié au client dans Azure DevOps via l'API, suivez ces étapes :

1. Utilisation de l'API Azure DevOps pour créer un nouveau référentiel dans l'organisation Azure DevOps `USite-Clients`.

2. Associez le référentiel au projet du dans la base de données.

## Push des fichiers HTML dans le référentiel Azure DevOps via l'API

Une fois le référentiel créé, vous pouvez pousser les fichiers HTML générés à l'intérieur à l'aide de l'API Azure DevOps :

1. Utilisation de l'API Azure DevOps pour pousser les fichiers HTML générés dans le référentiel dédié au client.

2. Push de tout les fichiers HTML généré dans le repository créer précedement.

## Création de la pipeline dans Azure DevOps via l'API

La création d'une pipeline pour le référentiel du client dans Azure DevOps est réalisée à l'aide de l'API Azure DevOps :

Utilisation l'API Azure DevOps pour créer une nouvelle pipeline pour le référentiel du client basé sur un fichier yaml.

Tout les pipelines sont éxecuté depuis le code grâce à l'API Azure DevOps (Aucun trigger automatique n'a été ajouté)

Voir les détail de la [CI](CI.md)
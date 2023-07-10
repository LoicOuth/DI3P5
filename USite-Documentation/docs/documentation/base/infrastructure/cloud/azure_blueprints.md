---
layout: Part
author: "Quentin ARDENOY"
---

# Mise en place de Blueprints Azure

[[toc]]

## Introduction

Azure Blueprints est un service d'Azure qui vous permet de déployer et de gérer des environnements cloud conformes aux standards de votre organisation. Ce README vous guidera à travers les étapes nécessaires pour mettre en place des Blueprints Azure.

## Table des matières

- [Mise en place de Blueprints Azure](#mise-en-place-de-blueprints-azure)
  - [Introduction](#introduction)
  - [Table des matières](#table-des-matières)
  - [Prérequis](#prérequis)
  - [Création d'un Blueprint](#création-dun-blueprint)
  - [Ajout de ressources au Blueprint](#ajout-de-ressources-au-blueprint)
  - [Déploiement du Blueprint](#déploiement-du-blueprint)
  - [Gestion des versions du Blueprint](#gestion-des-versions-du-blueprint)
  - [Références](#références)

## Prérequis

Avant de commencer, assurez-vous de disposer des éléments suivants :

- Un compte Azure avec les droits nécessaires pour créer et gérer des Blueprints.
- Une compréhension de base des services et des ressources Azure que vous souhaitez inclure dans votre Blueprint.
- Une connaissance des exigences et des normes de votre organisation pour la conformité des déploiements Azure.

## Création d'un Blueprint

La première étape pour mettre en place un Blueprint Azure est de créer un brouillon. Voici comment procéder :

1. Connectez-vous à votre compte Azure et accédez au portail Azure.
2. Dans le menu de navigation, recherchez et sélectionnez "Blueprints".
3. Cliquez sur le bouton "Créer" pour commencer à créer un Blueprint.
4. Donnez un nom, une description et spécifiez l'emplacement de définition où sera enregistré votre Blueprint, puis cliquez sur "Suivant".
5. Choisissez les artefacts que vous souhaitez inclure dans votre Blueprint. Cela peut inclure des rôles d'accès, des groupes de ressources, des modèles ARM etc.
6. Configurez les paramètres et les attributs pour chaque artefacts, en tenant compte des exigences et des normes de votre organisation.
7. Cliquez sur le bouton "Enregistrer le brouillon" pour valider sa création.

## Déploiement du Blueprint

Une fois que le brouillon de votre Blueprint est prêt, vous pouvez le publier. Voici comment procéder :

1. Dans la vue du Blueprint, cliquez sur "Publier".
2. Spécifiez une version pour votre Bluebrint, vous pouvez sur cette même page ajouter des notes de changement facultatifs, puis cliquez sur le bouton "Publier".
2. Après la publication, cliquez sur "Affecter le blueprint" dans le menu principal du Blueprint.
4. Configurez les paramètres spécifiques à l'affectation, tels que le nom, l'abonnement, la description, les valeurs des paramètres, etc.
5. Vérifiez les détails de l'affectation et cliquez sur "Affecter" pour lancer le déploiement.
6. Suivez l'état du déploiement dans la vue des affectations du Blueprint.

## Références

- Documentation Azure Blueprints : [https://docs.microsoft.com/azure/governance/blueprints/](https://docs.microsoft.com/azure/governance/blueprints/)
- Exemples de Blueprints Azure au format JSON : [https://github.com/Azure/azure-blueprints](https://github.com/Azure/azure-blueprints)
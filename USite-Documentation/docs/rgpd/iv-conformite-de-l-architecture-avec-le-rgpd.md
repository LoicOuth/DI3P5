---
layout: Part
author: "Ruihau TETAHIO"
---

# IV. Conformité de l'architecture avec le RGPD

[[toc]]

## Introduction

Ce document a pour but de faire une cartographie du SI de notre projet.

## Les actifs du projet

Cete section a pour but de décrire l'ensemble des composants de l'infrastructure.

Notre infrastructure complète se décline en deux environnements :

- L'environnement de `dev` et de `test` hébergé sur un serveur du DIIAGE
- L'environnement de `prod` hébergé sur Azure

### Environnement DIIAGE

Tous des serveurs des environnements de `dev` et de `test` sont hébergés sur un serveur Proxmox, lui-même hébergé sur un serveur physique `HPE Proliant`. Ce dernier est hébergé directement au DIIAGE et est accessible depuis le réseau du DIIAGE en `10.4.0.0/16`.

Les deux environnements sont mutualisés sur l'infrastructure mise en place. L'infrastructure se compose des éléments suivants :

- Un pare-feu `pfSense`, mis au-devant de toute l'infrastructure permettant une protection des en provenance des réseaux externes
- Un serveur `Proxmox` qui virtualise l'ensemble des serveurs ainsi que le réseau privé de l'infrastructure qui est alors strictement interne
- Quatre machines virtuelles sous `Debian` créées à partir d'une machine templatisée

L'accès physique aux serveurs est protégé et restreint. L'accès réseau aux équipement est également restreint et surveillé par le pare-feu pfSense.

> **Note**  
> Ces environnements ne contiennent aucune donnée à caractère personnel autre que celles des membres de l'équipe uSite. Toutes autres données de tests sont fictives.

### Environnement Azure

L'environnement Azure se compose des éléments suivants :

- Un `Azure Container Registry` qui nous sert de dépôt pour nos images docker
- Un cluster `Azure Kubernetes Service`

L'accès physique aux équipements est géré par Microsoft et est régis par un contrat de sous-traitance. Une étude des données collectées par le service est disponible [ici](03-gestion-avancee-des-donnees-personnelles-et-de-leurs-acces.md#azure-et-azure-devops).

### Autres services

Pour l'agrégation des logs et le monitoring de nos services, nous utilisons les services suivants :

- Zabbix qui s'occupe du monitoring des machines virtuelles du DIIAGE
- Prometheus qui s'occupe du monitoring du cluster Kubernetes hébergé au DIIAGE
- Azure Monitor qui s'occupe du monitoring de toute l'infrastructure de production, hébergée sur Azure
- Grafana Cloud qui centralise la visualisation de tout le monitoring (aggrégation de tous les services en un point)

Pour la gestion du code source de l'application et la gestrion de projet, nous utilisons `Azure DevOPS`.

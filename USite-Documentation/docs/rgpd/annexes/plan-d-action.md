---
layout: Part
author: "Quentin ARDENOY"
---

# Plan d'action

[[toc]]

## Introduction

Dans le cadre de notre engagement envers la protection des données personnelles et la réduction des risques de violation de la vie privée des utilisateurs, nous avons élaboré un plan d'action visant à identifier les mesures de sécurité appropriées et à mettre en place une gestion efficace des risques de conformité.

Il convient de noter que nous intégrerons les mesures complémentaires au plan d'action en fonction de leurs priorités et par environnement, en prenant en compte les ressources disponibles et les risques spécifiquement identifiés dans notre contexte. Nous nous engageons à mettre régulièrement à jour notre plan d'action afin de prendre en compte les évolutions du paysage de la sécurité et les exigences réglementaires qui nous sont applicables.

## Plan d'action pour l'environnement On-Premises

1. Machines virtuelles Debian : Accès non autorisé par SSH
   - Restreindre l'accès SSH uniquement aux adresses IP autorisées.
   - Utiliser des clés SSH plutôt que des mots de passe pour l'authentification.
   - Activer la journalisation des tentatives de connexion SSH afin de détecter les activités suspectes.
   - Configurer un système de détection d'intrusion pour bloquer les tentatives d'accès non autorisées.

2. Pare-feu pfSense : Compromission des identifiants d'accès administratifs
   - Renforcer la sécurité des identifiants d'accès administratifs en mettant en œuvre des politiques de gestion des mots de passe robustes, telles que l'utilisation de mots de passe complexes et leur rotation régulière.
   - Utiliser l'authentification multifactorielle pour les comptes administratifs afin de réduire les risques de compromission des identifiants.

3. Pare-feu pfSense : Attaques par déni de service (DoS) et attaques par déni de service distribué (DDoS)
   - Mettre en place un système de détection des attaques DoS/DDoS.
   - Configurer des seuils de trafic et des règles de filtrage pour bloquer les attaques.
   - Effectuer des tests de charge pour évaluer la capacité du pare-feu à gérer les attaques.

4. Serveur Proxmox : Accès non autorisé aux machines virtuelles et aux données
   - Configurer des règles de pare-feu pour limiter l'accès aux machines virtuelles depuis des sources non autorisées.
   - Mettre en place des mesures de surveillance pour détecter toute activité suspecte ou tentative d'accès non autorisé.

5. Serveur Proxmox : Contournement de l'isolement des machines virtuelles
   - Configurer les paramètres de virtualisation de manière à ce que l'isolement entre les machines virtuelles soit maintenu.
   - Mettre en place des politiques de gestion des snapshots pour minimiser les risques de compromission des machines virtuelles.

6. Serveur Proxmox : Exploitation des vulnérabilités logicielles
   - Mettre en place une politique de mise à jour régulière du serveur Proxmox et des composants logiciels pour bénéficier des correctifs de sécurité les plus récents.
   - Effectuer des analyses de vulnérabilité régulières pour identifier les failles de sécurité potentielles et prendre les mesures nécessaires pour les corriger.

7. Pare-feu pfSense : Tentatives de contournement du pare-feu et d'exploitation de vulnérabilités
   - Mettre en place des mesures de sécurité supplémentaires telles que l'authentification à deux facteurs pour les comptes administratifs.
   - Effectuer régulièrement des audits de sécurité pour identifier les éventuelles vulnérabilités et les corriger rapidement.

8. Pare-feu pfSense : Configuration incorrecte ou insuffisante des règles de pare-feu
   - Réaliser une revue complète des règles de pare-feu existantes pour s'assurer de leur pertinence et de leur adéquation.
   - Mettre en place une politique de gestion des changements pour garantir que toutes les modifications apportées aux règles de pare-feu sont dûment documentées, testées et approuvées.

9. Machines virtuelles Debian : Exploitation des vulnérabilités logicielles
   - Surveiller les annonces de sécurité et les bulletins de vulnérabilité pour identifier rapidement les correctifs nécessaires.

10. Machines virtuelles Debian : Attaques de type "man-in-the-middle" et interception des communications
   - Sensibiliser les utilisateurs à l'importance de vérifier les certificats SSL/TLS lors de l'accès aux services en ligne.

## Plan d'action pour l'environnement Cloud

1. Azure SQL Database : Accès non autorisé à la base de données
   - Utiliser les fonctionnalités de pare-feu Azure pour restreindre l'accès à la base de données uniquement aux adresses IP autorisées.

2. Cluster Azure Kubernetes Service : Accès non autorisé aux ressources du cluster.
   - Surveiller les journaux d'activité et les événements du cluster pour détecter toute activité suspecte.

3. Cluster Azure Kubernetes Service : Attaques internes par des applications malveillantes ou compromises
   - Appliquer des politiques de réseau pour limiter la communication entre les différents espaces de noms.
   - Surveiller les activités des utilisateurs et des services pour détecter tout comportement suspect ou malveillant.

4. Cluster Azure Kubernetes Service : Exploitation des vulnérabilités dans les applications déployées
   - Utiliser des outils de sécurité spécifiques à Kubernetes pour scanner les configurations et les déploiements à la recherche de vulnérabilités connues.

5. Azure SQL Database : Attaques par injection SQL
   - Utiliser des requêtes préparées ou des procédures stockées pour minimiser les risques d'injection SQL.
   - Mettre en œuvre des outils de détection d'injection SQL pour surveiller et alerter en cas d'activité suspecte.

## Plan d'action pour les services externes

1. Grafana Cloud : Accès non autorisé aux tableaux de bord
   - Activer l'authentification à deux facteurs pour les comptes d'utilisateurs afin de renforcer la sécurité des connexions. 

2. Azure DevOPS : Accès non autorisé aux données
   - Activer l'audit des activités dans Azure DevOps pour enregistrer et surveiller les actions effectuées par les utilisateurs, facilitant ainsi la détection des activités suspectes ou non autorisées.

3. Zabbix : Accès non autorisé aux données de surveillance et de journalisation
   - Mettre en place des mécanismes de protection contre les attaques par force brute  pour bloquer automatiquement des adresses IP après un certain nombre d'échecs de connexion.

4. Service de paiement : Fournir une plateforme de paiement compatible RGPD
   - Etudier les différentes alternatives de paiement existantes
   - Comparer les bénéfices et les contraintes de chacune
   - Mener une étude poussée de la compatibilité de chacun des services avec le RGPD

   Services envisagés pour implémentation future :
     - [Lydia](https://www.lydia-app.com/pro/cgus-pro/)

## Suivi et mise à jour du plan d'action

Ce plan d'action doit être considéré comme un document vivant qui sera régulièrement révisé et mis à jour en fonction des évolutions technologiques, des nouvelles menaces de sécurité et des changements dans notre infrastructure. Pour mener à bien cette tâche, un responsable de la sécurité chargé de surveiller la mise en œuvre du plan d'action a été nommé, afin de suivre les progrès réalisés et de signaler toute anomalie ou non-conformité.

En suivant ce plan d'action et en mettant en place des mesures de sécurité appropriées, nous renforcerons notre conformité au RGPD et protégerons efficacement les données personnelles des individus concernés.

## Responsable d'implémentation

Le responsable chargé de la sécurité est **Ruihau TETAHIO**.

Contacter le responsable d'implémentation :

- [Teams](https://teams.microsoft.com/l/chat/0/0?users=ruihau.tetahio@diiage.org)
- [Mail](ruihau.tetahio@diiage.org)

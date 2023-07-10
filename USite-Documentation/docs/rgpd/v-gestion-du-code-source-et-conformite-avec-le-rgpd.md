---
layout: Part
author: "Arthur BOYER"
---

# V. Gestion du code source et conformité avec le RGPD

[[toc]]

## Introduction

Mettre en avant tous les processus permettant de gérer efficacement le code source. Mettre en avant le fait qu'on ne met pas de secrets dans le code et mettre en avant la sécurité mise en place au niveau d'Azure DevOPS.

## Gestion du code source

### Le contrôle d'accès

Nous avons mis en place des mécanismes d'identification et d'authentification à la plateforme pour accéder au code source. Ce qui veut dire que chaque acteur dispose d'un compte utilisateur unique qui permet d'accéder à la plateforme de manière sécurisé.

Les mécanismes mise en place, nous avons établie des rôles appropriés afin de garantir que seules les personnes autorisées ont accès au code source. Ces droits sont régulièrement révisés grâce à nos processus agiles.

### Le suivi des modifications

Nous utilisons un système de contrôle de version, tel que Git afin d'enregistrer toutes les modifications apportées au code source, permettant ainsi un suivi précis de l'évolution du code et la possibilité de revenir à des versions antérieures si nécessaire.

De plus, nous avons mise en place des politiques liés aux branches, c'est à dire que certaines branches choisis au préalable ne peuvent recevoir du code source uniquement par un système de "Pull request". Cela permet d'avoir un contrôle sur qui envoie envoie du code source et qui peut autorisés l'envoie sur la branche ciblé. Grâce à ces politiques mises en place, nous avons pû améliorer la qualité du code source et de prévenir aux problèmes qui pourraient survenir.

Les membres de l'équipe peuvent donc examiner le code, ajouter des commentaires et proposer des améliorations possible avant que le code source soit fusionner au code source de l'application en production.

## Gestion des secrets

La gestion des secrets est une partie importante dans les développements. Pour cela, nous suivons des bonnes pratiques de développement pour éviter d'inclure des informations sensibles telles que des mots de passe, ou encore des informations nécessaire à s'authentifier à notre application.

Afin de garantir la sécurité des secrets que nous venons de mentionner, nous utilisons des variables d'environnement et des fichiers sécurisés. Cela permet de séparer les données sensibles du code lui-même. Aussi, grâce aux politiques mises en place grâce à Azure DevOps, uniquement les utilisateurs qui auront les droits pourront accéder aux secrets de l'application.

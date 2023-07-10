---
layout: Part
author: "Loïc OUTHIER"
---

# Rapport d'analyse RGPD du projet uSite

## Feuille de route

> **Note**  
> L'état de la rédaction des parties est matérialisée par des cases à cocher :
>
> - [x] **Partie terminée**
> - [ ] **Partie non terminée ou non commencée**

- [x] **[I. Introduction](./i-introduction.md)**

    Présenter brièvement le projet, son contexte et son objectif. Décrire l'importance de la conformité RGPD pour le projet et les parties prenantes concernées.

- [x] **[II. Gestion en continu de la documentation RGPD](./ii-gestion-en-continu-de-la-documentation-rgpd.md)**

    Détailler comment la mise à jour de la documentation en rapport avec le RGPD s'intègre aux process. Mettre en avant le fait qu'on utilise le format Markdown, pourquoi, qu'est-ce que ça permet. Détailler également les moyens techniques qui permettre de mettre à jour le PDF final via les pipelines Azure.

- [x] **[III. Gestion avancé des données personnelles et de leurs accès](./iii-gestion-avancee-des-donnees-personnelles-et-de-leurs-acces.md)**

    Faire une cartographie des données avec tous les détails que l'on a déjà mis en avant (le nom de la donée, le type, la base légale...). Penser à détailler les différents aspects de l'application, notamment la partie **Premium** et la partie **Freemium**. Penser aussi à la sous-traitance des données. Intégrer les données sous-traitées.

- [x] **[IV. Conformité de l'architecture avec le RGPD](./iv-conformite-de-l-architecture-avec-le-rgpd.md)**

    Faire une cartographie du SI, mettre en avant les moyens techniques mis en place au niveau de l'infrastructure afin de garantir que le traitement des données personnelles est bien conforme.

- [x] **[V. Gestion du code source et conformité avec le RGPD](./v-gestion-du-code-source-et-conformite-avec-le-rgpd.md)**

    Mettre en avant tous les process permettant de gérer efficacement le code source. Mettre en avant le fait qu'on ne met pas de secrets dans le code et mettre en avant la sécurité mise en place au niveau d'Azure DevOPS.

- [x] **[VI. Procédures d'information aux personnes](./vi-procedures-d-information-aux-personnes.md)**

    En fonction des bases légales utilisées dans le projet, savoir ce qu'on a mis en place pour permettre aux gens
      - de faire valoir leurs droits en termes d'information (comment on informe les personnes d'une manière générale)
      - de connaître les problématiques liées à l'accessibilité (formulaire de demande de suppression de données ou intégré aux fonctions) ==> dans tous les cas, préciser ce que ça fait et qu'est-ce que ça supprime ou qu'est-ce qu'on s'engage à supprimer

- [x] **[VII. Conformité de l'analyse statistique](./vii-conformite-de-l-analyse-statistique.md)**

    Mettre en avant les éléments utilisés pour mettre en oeuvre de la statistique sur les données (mise en place d'une plateforme interne, utilisation d'une plateforme externe). Le concept de pseudonymisation de la donnée doit être abordé. Si les traceurs nous concernent, les inclure également.

- [x] **[VIII. Gestion des risques en termes de conformité](./viii-gestion-des-risques-en-termes-de-conformite.md)**

    Identifier les risques potentiels de sécurité (matrice des risques) et identifier les remédiations à ces risques :
      - Les mesures principales ont été implémentées
      - Les autres agrémentées au plan d'action dans l'ordre des priorité

- **[Annexes](./annexes/)**

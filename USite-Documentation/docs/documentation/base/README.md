---
layout: Part
author: "Loïc OUTHIER"
---

# Introduction

[[toc]]

Ce dépôt a pour but de centraliser toute la documentation du projet uSite.

## Organisation du dépôt

Le dépôt se décline notamment en deux parties :

- La partie `Infrastructure`
- La partie `Developement`
- La partie `Gestion de projet`

D'une manière générale, pour faciliter la navigation dans la documentation, il faudra créer un dossier avec **le nom de la technologie, le nom du chapitre ou autre**. Il faudra créer dans ces dossiers un fichier `README.md`.

Voici des exemples type d'organisation de la documentation dans un projet :

```markdown
# Projet d'infra
📁 infrastructure/
├── 📁 on-premises/
│ ├── 📁 k8s/
│ │ ├── README.md
│ │ ├── 00_table_des_matieres.md
│ │ ├── 📁 00_concepts/
│ │ │ ├── README.md
│ │ │ ├── 📁 concept_1/
│ │ │ │ ├── README.md
│ │ │ ├── 📁 concept_2/
│ │ │ │ ├── README.md
│ │ ├── 📁 01_introduction/
│ │ │ ├── README.md
│ │ ├── 📁 02_installation/
│ │ │ ├── README.md
│ │ │ ├── 📁 01_configuratio_de_base/
│ │ │ │ ├── README.md
│ │ │ ├── 📁 02_configuratio_avancée/
│ │ │ │ ├── README.md

# Projet dev
📁 projet-2/
├── 📁 installation/
│ ├── README.md
│ ├── 01-installation de base.md
│ ├── 02-installation avancée.md
├── 📁 configuration/
│ ├── 01-configuration de l'hote.md
│ ├── 02-configuration du service.md
├── ...
```

## Comment contribuer

Deux processus sont possibles afin de contribuer :

- Pousser directement la documentation sur la branche principale du dépôt
- Utiliser le système de pull request intégré à Azure DevOPS

> **Important**  
> La seconde méthode est à privilégier pour la modification de documentations existantes.

### **Méthode 1** : Pousser directement la documentation sur la branche principale du dépôt

1. Clonez le dépôt : `git clone https://U-Site@dev.azure.com/U-Site/USite/_git/USite_BaseDeConnaissance`
2. Apportez vos modifications ou ajouts à la documentation
3. Ajoutez tous les fichiers suivis : `git add *`
4. Commitez vos modifications : `git commit -m "Description des modifications"`
5. Poussez votre branche sur le dépôt distant : `git push origin`

### **Méthode 2** : Utiliser le système de pull request intégré à Azure DevOPS

1. Clonez le dépôt : `git clone https://U-Site@dev.azure.com/U-Site/USite/_git/USite_BaseDeConnaissance`
2. Créez une nouvelle branche pour vos modifications : `git checkout -b nom-de-la-branche`
3. Apportez vos modifications ou ajouts à la documentation
4. Commitez vos modifications : `git commit -m "Description des modifications"`
5. Poussez votre branche sur le dépôt distant : `git push origin nom-de-la-branche`
6. Créez une Pull Request pour proposer vos modifications à la branche principale

## Règles de contribution

- Assurez-vous que vos modifications sont claires, bien structurées et faciles à comprendre
- Utilisez le format Markdown pour tous les fichiers de documentation
- Respectez l'organisation et la structure existante
- Demandez une relecture de vos modifications par au moins un autre membre de l'équipe avant de les fusionner dans la branche principale
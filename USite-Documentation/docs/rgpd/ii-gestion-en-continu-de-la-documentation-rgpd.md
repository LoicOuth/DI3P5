---
layout: Part
author: "Loïc OUTHIER"
---

# II. Gestion en continu de la documentation RGPD

[[toc]]

## Introduction

Notre entreprise accorde une grande importance à la mise à jour continue de notre documentation RGPD. Cette mise à jour s'intègre naturellement à nos processus quotidiens pour garantir une conformité constante et adaptative face à l'évolution des réglementations et des technologies.

## Intégration de la documentation RGPD à la gestion de projet Agile

Dans le cadre de notre démarche de gestion de projet Agile, nous avons intégré la rédaction et la mise à jour de la documentation RGPD à notre backlog de développement par le biais de User Stories spécifiques.

Chaque User Story relative à la documentation RGPD suit ce format standard :

- **En tant que** membre de l'équipe de développement,
- **Je veux** mettre à jour la documentation RGPD,
- **Afin de** maintenir notre conformité et de communiquer efficacement les changements aux parties prenantes.

De cette façon, la documentation RGPD devient une partie intégrante de nos sprints de développement. Chaque modification ou ajout de fonctionnalité qui a une incidence sur les données personnelles implique la création d'une User Story dédiée à la mise à jour de la documentation RGPD.

Cela nous assure que la documentation RGPD est toujours à jour et reflète l'état actuel de notre produit, en respectant les principes du RGPD tels que la responsabilité et la transparence. De plus, cette approche facilite la responsabilisation de tous les membres de l'équipe en matière de conformité RGPD.

## Utilisation du format Markdown

Nous avons choisi d'utiliser le format Markdown pour plusieurs raisons :

1. **Simplicité et accessibilité** : Le Markdown est facile à utiliser et à comprendre, même pour les non-développeurs. Cela signifie que chaque membre de notre équipe peut contribuer à la documentation.

2. **Compatibilité et flexibilité** : Les fichiers Markdown peuvent être convertis en de nombreux autres formats, comme le HTML pour le web ou le PDF pour des documents imprimables.

3. **Gestion de version** : Les fichiers Markdown sont compatibles avec les systèmes de gestion de versions comme Git. Cela nous permet de garder une trace de toutes les modifications apportées à la documentation.

## Mise à jour du site web final via les pipelines Azure

Nous utilisons Azure Pipelines, un service d'intégration continue / livraison continue (CI/CD), pour automatiser la mise à jour du site web final de notre documentation RGPD. Lorsqu'une modification est apportée au fichier Markdown source, Azure Pipelines déclenche automatiquement une nouvelle génération du site web.

Ce processus nous permet de garantir que notre documentation est toujours à jour, précise et conforme aux exigences du RGPD. De plus, l'automatisation via Azure Pipelines permet d'économiser du temps et des efforts, améliorant ainsi l'efficacité globale de nos processus de conformité RGPD.

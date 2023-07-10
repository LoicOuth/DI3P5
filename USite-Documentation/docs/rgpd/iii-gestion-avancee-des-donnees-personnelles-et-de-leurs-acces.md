---
layout: Part
author: "Ruihau TETAHIO"
---

# III. Gestion avancé des données personnelles et de leurs accès

[[toc]]

## Introduction

Cette section contient l'ensemble des données à caractère personnel traitées dans le cadre de nos activités.

Les différentes données collectées pour chacune des parties prenantes y sont détaillés.

## Détail des différents aspects de l'application

L'utilisation de notre plateforme sera possible via la souscription d'un abonnement mensuel ou annuel (au choix). Les différents abonnements prévus à ce jour sont les suivants :

- Un abonnement à 5€ par mois donnant accès à un certain niveau de fonctionnalités
- Un abonnement à 10€ par mois donnant accès à plus de fonctionnalité sur la plateforme d'édition et un nom de domaine personnalisé
- Un abonnement à 15€ par mois avec en plus des statistiques de référencement par rapport à son ou ses sites

## Les parties prenantes

Dans le cadre du traitement des données à caractère personnel, il est important de distinguer les différentes parties prenantes. Relativement aux parties prenantes présentées dans l'[introduction](../01_introduction/README.md#importance-du-rgpd), les quatres profiles suivants sont à prendre en compte et à cartographier dans notre contexte :

- **Les utilisateurs** - Les visiteurs du site de présentation de l'application, les visiteurs des sites publiés par nos soins au titre de nos clients
- **Nos clients** - Les individus qui se sont inscrit sur notre plateforme
- **Le personnel** - Les personnes ayant pris part au projet
- **Les prestataires** - à savoir :
  - **[Azure](https://azure.microsoft.com/fr-fr)** - Hébergeur cloud sur lequel sont hébergées les données à caractère personnel
  - **[Azure DevOPS](https://dev.azure.com/)** - Plateforme Azure DevOPS pour la gestion du code et la gestion du projet
  - `Coming soon` **[OpenAI](https://openai.com/)** - Plateforme d'intelligence artificielle
  - `Coming soon` **[Stripe](https://stripe.com/fr)** - Plateforme de paiement

## Analyse des sous-traitants en matière de conformité

### Azure et Azure DevOPS

#### Présentation générale

Azure, l'une des plateformes cloud computing de Microsoft, s'engage fermement à protéger la confidentialité des données. Cette protection de la confidentialité des données est **intégrée à Azure dès la conception**. Microsoft a conçu Azure avec des **contrôles de sécurité** de pointe, des **outils de conformité** et des **politiques de confidentialité** pour protéger vos données dans le cloud, y compris les catégories de données personnelles identifiées par le RGPD​1​.

Azure offre plusieurs outils pour aider à atteindre les objectifs de confidentialité des utilisateurs, notamment :

- **Le portail Azure Data Subject Requests pour le RGPD**, qui fournit des conseils étape par étape sur la manière de se conformer aux exigences du RGPD pour trouver et agir sur les données personnelles qui résident dans Azure.
- **Azure Policy**, qui est profondément intégré dans Azure Resource Manager, aide votre organisation à appliquer la politique sur les ressources.
- **Compliance Manager**, qui est un outil gratuit d'évaluation des risques basé sur les flux de travail, peut vous aider à gérer la conformité réglementaire dans le modèle de responsabilité partagée du cloud.
- **Azure Information Protection**, qui offre une analyse des partages de fichiers pour les serveurs sur site afin de découvrir des données sensibles.
- **Azure Security Center**, qui fournit une gestion unifiée de la sécurité et une protection avancée contre les menaces.
- **Azure Security and Compliance GDPR Blueprint**, qui peut vous aider à construire et à lancer des applications cloud qui répondent aux exigences du RGPD​1​.

Dans le cadre de notre propre compliance, l'ensemble de ces outils rentrent dans notre processus qualité et compliance.

#### Données collectées par le sous-traitant

Pour lister toutes les données colectées pour ce sous-traitant, nous allons utiliser le modèle de tableau suivant :

| Donnée | Type de donnée | Caractère de la donnée | Utilité de la donnée | Base légale du traitement | Source de la donnée | Durée de conservation |
| ------ | -------------- | ---------------------- | -------------------- | ------------------------- | ------------------- | --------------------- |
| * | * | * | * | * | * | * |

Voici le détail de chaque élément du tableau :

- **Donnée** : le nom de la donnée en question (ex : nom, email, téléphone)
- **Type de donnée** : le type de donnée (ex : texte, nombre)
- **Caractère de la donnée** : s'il s'agit d'une donnée directement identifiante, indirectement identifiante ou pseudonymisée
- **Utilité de la donnée** : là où la donnée est utilisée dans le cadre du traitement (ex : Facturation, Relances, Contact par mail)
- **Base légale du traitement** : la base légale sur laquelle chaque traitement de données repose (consentement, exécution d'un contrat, obligation légale, intérêt légitime, etc.)
- **Source de la donnée** : si la donnée provient directement de la personne concernée, d'une source publique ou d'un tiers
- **Durée de conservation** : la durée pendant laquelle les données seront conservées ou les critères utilisés pour déterminer cette durée

> **Note**  
> La base légale se réfère directement à [l'article 6 du RGPD](https://www.cnil.fr/fr/reglement-europeen-protection-donnees/chapitre2#Article6) sur la licéité du traitement

Voici les données collectées de manière générale par le sous-traitant :

| Donnée | Type de donnée | Caractère de la donnée | Utilité de la donnée | Base légale du traitement | Source de la donnée | Durée de conservation |
|--------|----------------|------------------------|-----------------------|----------------------------|----------------------|----------------------|
| Nom | Texte | Directement identifiante | Identification() de l'utilisateur | Exécution d'un contrat | Personne concernée | Non précisé |
| Email | Texte | Directement identifiante | Communication | Consentement | Personne concernée | Non précisé |
| Numéro de téléphone | Texte | Directement identifiante | Communication | Consentement | Personne concernée | Non précisé |
| Adresse IP | Texte | Indirectement identifiante | Journalisation et débogage | Intérêt légitime | Collectée automatiquement | Non précisé |
| Logs d'audit | Texte | Indirectement identifiante | Audit et conformité | Obligation légale | Collectée automatiquement | Non précisé |

### `Coming soon` Stripe

Stripe, une plateforme de paiement, est compatible RGPD

#### Données collectées par Stripe

Dans le cadre d'une collaboration future avec Stripe pour la partie paiement de notre application, nous avons déterminé que les données à caractère personnel récoltées par ce sous-traitant sont les suivantes :

| Donnée | Type de donnée | Caractère de la donnée | Utilité de la donnée | Base légale du traitement | Source de la donnée | Durée de conservation |
| --- | --- | --- | --- | --- | --- | --- |
| Traqueurs | Texte | Indirectement identifiante | Analyse d'utilisation | Consentement | Directement de la personne concernée | Non précisé |
| Données d’utilisation | Texte | Indirectement identifiante | Analyse d'utilisation | Consentement | Directement de la personne concernée | Non précisé |
| Prénom | Texte | Directement identifiante | Facturation, Contact par mail | Exécution d'un contrat | Directement de la personne concernée | Non précisé |
| Nom de famille | Texte | Directement identifiante | Facturation, Contact par mail | Exécution d'un contrat | Directement de la personne concernée | Non précisé |
| Adresse e-mail | Texte | Directement identifiante | Facturation, Contact par mail | Exécution d'un contrat | Directement de la personne concernée | Non précisé |
| Adresse de facturation | Texte | Directement identifiante | Facturation | Exécution d'un contrat | Directement de la personne concernée | Non précisé |
| Données relatives au paiement | Texte | Directement identifiante | Facturation | Exécution d'un contrat | Directement de la personne concernée | Non précisé |
| Historique d’achat | Texte | Directement identifiante | Analyse d'utilisation, Facturation | Exécution d'un contrat | Directement de la personne concernée | Non précisé |

### `Coming soon` OpenAI

OpenAI est une organisation de recherche en intelligence artificielle. Dans le cadre de nos activités, nous utiliserons dans le futur OpenAI, et notamment ChatGPT, afin de générer du texte pour les sites web et des ébauches de sites HTML et CSS.

Dans le cadre de cet usage, le client sera ammené à interagir indirectement avec la plateforme et lui fournira de façon libre des informations sur son activité. Il est toutefois à noter qu'il pourra donner toutes informations qu'il souhaite, dont des données à caractère personnel directement ou indirectement identifiantes. Ainsi, le client sera tenu responsable dans le cas où il divulgerais toute information personnelle.

## Cartographie des données traitées directement par l'application

Dans cette partie, nous allons détailler toutes les données collectées dans le cadre de notre activité. Nous utiliserons le modèle de tableau présenté précédemment.

### Les utilisateurs

Dans le cadre du traitement des données, aucune donné à caractère personnel n'est collectée à ce jour.

Dans le cadre de l'évolution de nos services, nous nous réservons le droit de rajouter cette possibilité.

### Les clients

| Donnée         | Type de donnée | Caractère de la donnée   | Utilité de la donnée                    | Base légale du traitement | Source de la donnée | Durée de conservation                              |
| -------------- | -------------- | ------------------------ | --------------------------------------- | ------------------------- | ------------------- | -------------------------------------------------- |
| Nom de famille | Texte          | Directement identifiante | Facturation, relances, contact par mail | Consentement              | Individu            | Un an après la suppression du compte |
| Prénom | Texte          | Directement identifiante | Facturation, relances, contact par mail | Consentement              | Individu            | Un an après la suppression du compte |
| Adresse géographique | Texte | Indirectement identifiante | Facturation, statistiques à usage interne uniquement (amélioration des services) | Consentement | Individu | Suppression de l'adresse exacte après suppression du compte, conservation du code postal, de la région et du pays |
| Adresse email | Texte | Directement identifiante | Facturation, relances, contact par mail | Consentement | Individu | Suppression immédiate après suppression du compte |

### Le personnel

| Donnée | Type de donnée | Caractère de la donnée | Utilité de la donnée | Base légale du traitement | Source de la donnée | Durée de conservation |
| ------ | -------------- | ---------------------- | -------------------- | ------------------------- | ------------------- | --------------------- |
| Nom | Texte | Directement identifiante | Suivi des activités, support par mail | Consentement | Tier (Azure DevOPS) | Sous-traitance |
| Prénom | Texte | Directement identifiante | Suivi des activités, support par mail | Consentement | Tier (Azure DevOPS) | Sous-traitance |
| Adresse email | Texte | Directement identifiante | Suivi des activités, support par mail | Consentement | Tier (Azure DevOPS) | Sous-traitance |

## Processus d'amélioration continue

Pour respecter la reglementation en vigueur, nous nous engageons à continuellement revoir notre politique de gestion des données à caractère personel. Cette amélioration continue est au coeur même de nos processus Agile, comme expliqué dans la section [II. Gestion en continu de la documentation RGPD](./ii-gestion-en-continu-de-la-documentation-rgpd.md). De ce fait, nous nous assurons qu'il n'y ai pas d'abus de traitement.

## Gestion des risques

Le traitement des données à caractère personnel est un processus risqué. Pour se prémunir des risques liés, tel que la fuite de données ou l'abus de traitement de données à caractère personnel, nous avons mis en place les actions immédiates suivantes :

- **Sensibilisation des membres du groupe** - Chacun des membres du groupe uSite devra suivre tous les 5 ans un atelier RGPD avec l'ensemble des supports de formation **à jour** fournis par la CNIL
- **Contrôle régulier des sous-traitants** - La plateforme uSite étant ammenée à sous-traiter partie de son activité, nous nous engageons à suivre une démarche de révision continue de la conformité de nos sous-traitants en réalisant une veille
- **Notification en cas de fuite de données** - En cas de fuite de données avérée, uSite s'engage à communiquer l'ensemble des détails à la CNIL dans les 72 heures qui suivent la découverte de la faille. Nous nous engageons également à communiquer sur l'incident à tous les clients concernés par cette fuite de données

Vous pouvez retrouver tous les détails sur la gestion des risques dans le cadre de notre activité [ici](./viii-gestion-des-risques-en-termes-de-conformite.md).

## Gestion des accès aux données

L'ensemble des données à caractère personnel est hébergé dans une unique base de données hébergée par Azure. Comme décrit dans la section [Azure et Azure DevOPS](#azure-et-azure-devops), le sous-traitant fournis un panel complet d'outils pour la gestion de la conformité et la sécurité et l'audit du système.

En matière d'accès aux données, et notamment aux bases de données, tous les accès sont audités et toutes les actions réalisées sont enregistrées. Un mécanisme d'alerte sera prochainement mis en place afin de prévenir lors d'une commande suspecte ou d'une tentative d'accès aux données non autorisée. Cela nous permet :

- De s'assurer que **seuls les comptes explicitement autorisés ont accès aux données**
- De s'assurer que **l'accès aux données est limité** (exemple : l'éditeur de site n'a pas à accéder aux données à caractère personnel du client)

## Références

- [CNIL - Article 5 - Principes relatifs au traitement des données à caractère personnel](https://www.cnil.fr/fr/reglement-europeen-protection-donnees/chapitre2#Article5)
- [CNIL - Article 6 - Licéité du traitement](https://www.cnil.fr/fr/reglement-europeen-protection-donnees/chapitre2#Article6)
- [CNIL - Article 9 - Traitement portant sur des catégories particulières de données à caractère personnel](https://www.cnil.fr/fr/reglement-europeen-protection-donnees/chapitre2#Article9)
- [CNIL - Notifier une violation de données personnelles](https://www.cnil.fr/fr/notifier-une-violation-de-donnees-personnelles)
- [Microsoft - Protecting privacy in Microsoft Azure: GDPR, Azure Policy Updates](https://azure.microsoft.com/en-us/blog/protecting-privacy-in-microsoft-azure-gdpr-azure-policy-updates/)

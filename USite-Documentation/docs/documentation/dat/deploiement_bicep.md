---
layout: Part
author: "Ruihau TETAHIO et Quentin ARDENOY"
---

# Infrastructure as Code - Présentation du déploiement

[[toc]]

## Introduction

Cette documentation a pour objectif d'expliquer le processus de déploiement de notre infrastructure sur Azure en utilisant des fichiers Bicep. Elle détaille les étapes nécessaires pour lancer un déploiement à l'aide de l'interface de ligne de commande Azure (Az CLI) et fournit des instructions claires pour chaque étape. Vous y trouverez également des prérequis, des explications sur la structure des fichiers Bicep et des conseils pratiques pour personnaliser le déploiement en fonction de vos besoins spécifiques.

En suivant ces instructions, vous serez en mesure de déployer rapidement et facilement notre infrastructure sur Azure en utilisant des fichiers Bicep, en garantissant une configuration cohérente et reproductible.

## Lancer un déploiement avec Az CLI

### Prérequis

- **Az CLI installé** - Installer `az-cli` en s'aidant de la [documentation officielle de Microsoft](https://learn.microsoft.com/fr-fr/cli/azure/install-azure-cli)
- **Disposer d'une copie locale de ce dépôt** - Cloner ce dépôt en utilisant la commande suivante :

    ```bash
    git clone https://U-Site@dev.azure.com/U-Site/USite/_git/USite_bicep
    ```

    > **Note**  
    > A défaut d'avoir git installé, il est possible de télécharger les fichiers directement.

### Etapes à suivre

Le déploiement de notre infrastructure nécessite plusieurs étapes distinctes en raison de contraintes techniques spécifiques. Ces étapes sont conçues pour gérer de manière logique le déploiement de l'infrastructure en prenant en compte les dépendances entre les différentes ressources.

Voici les étapes du déploiement :

- **Étape 1** - Déploiement initial des ressources : Cette étape consiste à déployer les ressources de base requises pour notre infrastructure. Elle est réalisée en utilisant le fichier Bicep intitulé 'step1.bicep' qui permet de décrire et de provisionner les ressources de manière cohérente.

- **Étape 2** - Publication du runbook : Pour pouvoir publier notre runbook, nous devons effectuer une étape intermédiaire. En effet, ce dernier aura besoin d'installer une extension sans prompt avec une commande spécifique. Cette étape nous permet donc d'installer l'extension requise et de publier le runbook.

    **Note** : Dans le contexte de notre déploiement, le runbook a pour objectif de contrôler le cycle de vie de notre cluster AKS (Azure Kubernetes Service). En permettant d'automatiser les actions de démarrage et d'arrêt du cluster.

- **Étape 3** - Mise à jour des ressources : Une fois que le runbook est publié, nous procédons à l'assignation des rôles nécéssaires et à la mise à jour des ressources déployées précédemment afin de permettre au runbook d'exécuter les actions requises sur le cluster. Cette étape est réalisée en utilisant le fichier Bicep intitulé 'step2.bicep'.

#### Etape 1 - Déploiement initial des ressources

1. A l'aide d'`az cli`, se connecter à votre compte Azure :

    ```bash
    az login
    ```

    Une fenêtre devrait s'ouvrir dans votre navigateur. Sélectionnez ensuite le compte évec lequel vous voulez vous connecter.

2. Une fois connecté, spécifiez l'abonnement qu'`az cli` devra utiliser :

    ```bash
    az account set --subscription <id_de_la_subscription>
    ```

    > **Note**  
    > Il est possible de récupérer toutes les subscriptions configurées sur votre compte avec la commande `az account list`

3. Créer un groupe de ressources dans lequel les ressources vont être déployées :

    ```bash
    az group create --name "usite-ressources-rg" --location "west europe"
    ```

4. Une fois le groupe de ressources créé, lancer le déploiement dans ce groupe de ressources avec la commande suivante :

    ```bash
    az deployment group create --mode Incremental --resource-group "usite-ressources-rg" --template-file "step1.bicep" --parameters "step1-parameters.json"
    ```

#### Etape 2 - Publication du runbook

1. Permettre l'utilisation d'extensions az cli sans prompt

    ```bash
    az config set extension.use_dynamic_install=yes_without_prompt
    ```

    Pour publier un runbook via `az cli`, ce dernier aura besoin d'installer une extension. Cette commande permet d'installer l'extension nécessaire sans prompt.

2. Publier le runbook avec la commande suivante :

    ```bash
    az automation runbook publish --resource-group "usite-ressources-rg" --automation-account-name "AutomationAccount" --name "Stop-and-Start-AKS"
    ```

    > **Note**  
    > les paramètres `--automation-account-name` et `--name` sont renseignés directement dans le fichier **step1-parameters.json**

Le runbook est maintenant publié et prêt à être utilisé.

#### Etape 3 - Mise à jour des ressources

1. Lancer le déploiement dans le groupe de ressources créé avec la commande suivante :

    ```bash
    az deployment group create --mode Incremental --resource-group "usite-ressources-rg" --template-file "step2.bicep" --parameters "step2-parameters.json"
    ```

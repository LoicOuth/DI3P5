---
layout: Part
author: "Quentin ARDENOY et Ruihau TETAHIO"
---

# Redéploiement complet de l'infrastructure

[[toc]]

## Introduction

## Procédure

La procédure à suivre afin de redéployer l'infrastructure est la suivante.

### Etapes à suivre pour le redéploiement de l'infrastructure

- **Az CLI installé** - Installer `az-cli` en s'aidant de la [documentation officielle de Microsoft](https://learn.microsoft.com/fr-fr/cli/azure/install-azure-cli)
- **Disposer d'une copie locale de ce dépôt** - Cloner ce dépôt en utilisant la commande suivante :

    ```bash
    git clone https://U-Site@dev.azure.com/U-Site/USite/_git/USite_bicep
    ```

    > **Note**  
    > A défaut d'avoir git installé, il est possible de télécharger les fichiers directement.

Le déploiement se fait en 3 grandes étapes :

- **Etape 1** - Déploiement bicep initial des ressources
- **Etape 2** - Publication du runbook (commande `az cli`)
- **Etape 3** - Déploiement bicep pour mettre à jour les ressources

#### Etape 1 - Déploiement bicep initial des ressources

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

#### Etape 3 - Déploiement bicep pour mettre à jour les ressources

1. Lancer le déploiement dans le groupe de ressources créé avec la commande suivante :

    ```bash
    az deployment group create --mode Incremental --resource-group "usite-ressources-rg" --template-file "step2.bicep" --parameters "step2-parameters.json"
    ```

### Etapes à suivre pour le redéploiement des applications

Le déploiement se fait en 3 grandes étapes :

- **Etape 1** - Connexion à l'AKS
- **Etape 2** - Redéploiement d'ArgoCD
- **Etape 3** - Réinitialisation du projet
- **Etape 4** - Changement des entrées DNS

#### Etape 1 - Connexion à l'AKS

1. Génération du fichier .kube/config :

    ```bash
    az aks get-credentials --resource-group usite-ressources-rg --name usite-aks-cluster01rt
    ```

2. Sur une machine WSL locale, copier le fichier .kube/config :

    ```bash
    cp /mnt/c/Users/ruihau/.kube/config .kube/config
    ```

#### Etape 2 - Redéploiement d'ArgoCD

1. Déploiement d'ArgoCD :

    ```bash
    kubectl create namespace argocd
    kubectl apply -n argocd -f https://raw.githubusercontent.com/argoproj/argo-cd/stable/manifests/install.yaml
    ```

2. Changer l'éditeur "kubectl" par défaut :

    ```bash
    export KUBE_EDITOR=nano
    ```

3. Editer le déploiement ArgoCD pour rajouter l'argument `--insecure` :

    ```bash
    kubectl edit deployment -n argocd argocd-server
    ```

    Rechercher "args" puis rajouter la ligne suivante :

    ```yaml
    - --insecure
    ```

4. Récupérer le mot de passe ArgoCD généré par défaut :

    ```bash
    kubectl -n argocd get secret argocd-initial-admin-secret -o jsonpath="{.data.password}" | base64 -d
    ```

#### Etape 3 - Réinitialisation du projet

1. Récupération du dépôt `USite_k8s`:

    ```bash
    git clone https://U-Site@dev.azure.com/U-Site/USite/_git/USite_k8s
    ```

    > **Note**  
    > Pour cloner le dépôt, générer un git credential temporaire (Generate Git credentials).

2. Application des fichiers ArgoCD:

    ```bash
    kubectl apply -f USite_k8s/deployments/templates/argocd/
    ```

    > **Note**  
    > Ne pas tenir compte des erreurs. Elles sont causées par le fait que certains fichiers dépendent du template Helm.

3. Application du fichier `application.yaml`:

    ```bash
    kubectl apply -f USite_k8s/application.yaml
    ```

Les applications se rédéploient toutes. Les applications prennent environ 5 minutes à se redéployer.

#### Etape 4 - Changement des entrées DNS

> **Warning**  
> Bien faire attention à attendre 5 minutes le temps que les applications se redéploient.

1. Récupération de l'adresse IP externe de l'ingress

    ```bash
    kubectl get svc -n ingress-nginx | grep LoadBalancer
    ```

2. Changer les adresses DNS sur la plateforme OVH:

Une fois les DNS changés, les applications devraient être accessibles sous 5 minutes (ou jusqu'à 24h après).

> **Important** (temporaire au 06/07/23)  
> Les certificats SSL ne sont plus signés par l'API Let's Encrypt en production mais celle de staging.

---
layout: Part
author: "Ruihau TETAHIO"
meta:
  - name: env
    content: dev,test,prod
---

# Problèmes connus sur ArgoCD

[[toc]]

## Introduction

Ce répertoire a pour but de documenter tous les problèmes rencontrés avec ArgoCD.

## Le mot de passe défini ne fonctionne plus

Cette section décrit les symptômes et décrit les étapes à suivre pour diagnostiquer et résoudre ce problème.

### Symptômes

Lorsque je tente de me connecter sur l'interface utilisateur d'ArgoCD et que je renseigne le mot de passe défini lors de sa configuration, l'erreur **Invalid username or password** apparaît.

### Cause

Le problème est dû au fait que le secret utilisé pour stocker le mot de passe admin ait été corrompu ou modifié. Cela peut être dû au redémarrage du pod **argocd-server**, au **rollout** du déploiement ArgoCD ou au redémarrage du serveur hébergeant les pods ArgoCD (qui mène au redémarrage des pods du déploiement ArgoCD).

### Solution

1. Réinitialisez le secret admin.password et admin.passwordMtime de ArgoCD

    ```bash
    kubectl patch secret argocd-secret -n argocd \
     -p '{"data": {"admin.password": null, "admin.passwordMtime": null}}'
    ```

2. Effectuez un rollout du déploiement ArgoCD pour regénérer le mot de passe initial

    ```bash
    kubectl -n argocd rollout restart deploy
    ```

3. Récupérez le mot de passe généré

    ```bash
    kubectl -n argocd get secret argocd-initial-admin-secret \
     -o jsonpath="{.data.password}" | base64 -d
    ```

4. Changez le mot de passe admin

    Une fois connecté sur l'UI ArgoCD, naviguez dans `User Info` > `Update Password` et mettez à jour le mot de passe admin

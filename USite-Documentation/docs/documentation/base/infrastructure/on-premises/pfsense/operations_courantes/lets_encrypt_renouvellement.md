---
layout: Part
author: "Loïc OUTHIER"
---

# Renouvellement du certificat wildcard avec Let's Encrypt

[[toc]]

## Introduction

Les certificats Let's Encrypt sont gérés par le package ACME installé sur pfSense. Dans la configuration du package, la tâche de renouvellement automatique des certificats est activée. Il peut arriver cependant que la tâche ne renouvelle pas les certificats, ce qui oblige à renouveler les certificats manuellement.

Dans cette documentation, nous allons détailler les étapes à suivre pour mettre le certificat `*.usite.fr` à jour.

## Prérequis

- S'assurer que le package `ACME` est bien installé (ce qui est normalement le cas)

## Etapes à suivre

1. Se connecter à l'interface d'administration pfSense
2. Naviguer dans l'onglet `Services` (1) > `Acme Certificates` (2) :
    ![Navigation vers le service Acme Certificates](/assets/images/pfsense/14.png)
3. Naviguez dans la section `Certificates` (1) puis cliquez sur `Add` (2) :  
    ![Ajout d'un nouvel account key](/assets/images/pfsense/15.png)
4. Cliquez sur le bouton `Issue/Renew` et attendre que le certificat se genère :  
    ![Configuration des Domain SAN list](/assets/images/pfsense/16.png)
    > **Note**  
    > La génération du certificat peut prendre plusieures minutes
5. Lorsqu'un message en vert apparaît, le certificat est bien créé.

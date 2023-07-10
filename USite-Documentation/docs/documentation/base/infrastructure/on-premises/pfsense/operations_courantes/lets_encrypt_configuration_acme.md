---
layout: Part
author: "Loïc OUTHIER"
---

# Configuration de ACME pour gérer les certificats Let's Encrypt

[[toc]]

## Introduction

Le package ACME est un package permettant de faciliter la gestion des certificats Let's Encrypt.

## Installation du package ACME

1. Connectez-vous à l'interface d'adnistration pfSense
2. Une fois connecté, naviguez dans `System` > `Package Manager` :  
    ![Navigation vers le package manager](/assets/images/pfsense/01.png)
3. Cliquez sur l'onglet `Available Packages` (1), recherchez le package `acme` (2) puis cliquez sur le bouton `Search`. Une fois que le package `acme` apparaît, cliquer sur le bouton `Install` :  
    ![Installation du package ACME](/assets/images/pfsense/02.png)
4. Le package s'installe.

> **Note**  
> Le service est situé dans l'onglet `Services` > `Acme Certificates`

## Configuration du service ACME

### Configuration de la tâche de renouvellement automatique

1. Naviguez dans `Services` > `Acme Certificates` :  
    ![Navigation vers le service ACME](/assets/images/pfsense/03.png)
2. Naviguez dans la section `General Settings` (1), cochez la case `Cron Entry` (2) puis cliquez sur `Save` (3) :  
    ![Activation de la tâche Cron](/assets/images/pfsense/04.png)
3. La configuration est terminée.

### Création de l'Account Key

1. Toujours depuis la page `Acme Certificates`, naviguez dans la section `General Settings` (1) puis cliquez sur `Add` (2) :  
    ![Ajout d'un nouvel account key](/assets/images/pfsense/05.png)
2. Renseignez un nom (1) et une description (2).
3. Dans le champs ACME Server, sélectionner `Let's Encrypt Production ACME v2` (3).
4. Renseignez une adresse email valide (4).
    > **Note**  
    > Cette adresse email sera utilisée par Let's Encrypt pour les notificaitons importantes concernant le renouvellement du certificat
5. Générez un nouvel Account Key en cliquant sur le bouton `Create new account key` (5).
6. Enregistrez le tout auprès de Let's Encrypt en cliquant sur le bouton `Register ACME account key` (6). Si l'enregistrement se fait bien, cliquez sur `Save` (7) :  
    ![Ajout d'un nouvel account key](/assets/images/pfsense/06.png)

### Création du token OVH

1. Naviguez sur [le lien de création de tokens OVH](https://eu.api.ovh.com/createToken/).
2. Connectez-vous avec le compte OVH sur lequel le DNS est enregistré.
3. Renseignez un nom d'application (1), une description (2) et définir la validité souhaitée (3)(Unlimited dans notre cas).
4. Renseignez les droits `GET`,`PUT`,`POST`,`DELETE` pour `/domain` et `/domain/*` (4).
5. Spécifiez l'adresse IP autorisée à requêter avec ce token (5).
6. Une fois toutes ces informations renseignées, cliquez sur `Create` :  
    ![Création du token](/assets/images/pfsense/07.png)
7. Une fois le token créé, notez toutes les informations :  
    ![Création du token](/assets/images/pfsense/08.png)
    > **Warning**  
    > Une fois que vous fermez la fenêtre, il sera impossible de récupérer les identifiants plus tard

### Création du certificat

1. Toujours depuis la page `Acme Certificates`, naviguez dans la section `Certificates` (1) puis cliquez sur `Add` (2) :  
    ![Ajout d'un nouvel account key](/assets/images/pfsense/09.png)
2. Renseignez un nom (1), une description (2), le compte ACME créé plus tôt (3) et définir la taille de la clé privée à 2048 (4) :  
    ![Ajout d'un nouvel account key](/assets/images/pfsense/10.png)
3. Au niveau de la section `Domain SAN list`, renseignez le nom de domaine de base (ex : domain.fr) (1).
4. Au niveau de `Method`, recherchez et sélectionnez `Domain SAN list`, `DNS-ovh / kimsufi / soyoustart / runabove`.
5. Renseignez l'`Application Key` (3), l'`Application Secret` (4), la `Consumer Key` (5) et au niveau de API Endpoint, sélectionnez `OVH Europe` (6).
6. Une fois toutes ces informations renseignées, cliquez sur l'icône `Dupliquer` pour dupliquer l'entrée (7).
7. Sur la nouvelle entrée, rajoutez `*.` au niveau du domaine (1) pour obtenir le certificat wildcard (ex : *.domain.fr) :  
    ![Configuration des Domain SAN list](/assets/images/pfsense/11.png)
8. Cliquez sur le bouton `Save` tout en bas.
9. Cliquez sur le bouton `Issue/Renew` et attendre que le certificat se genère :  
    ![Configuration des Domain SAN list](/assets/images/pfsense/12.png)
    > **Note**  
    > La génération du certificat peut prendre plusieures minutes
10. Lorsqu'un message en vert apparaît, le certificat est bien créé.

### Vérification du certificat

1. Une fois le certificat généré, naviguez dans `System` > `Cert. Manager`, puis cliquez sur l'onglet `Certificates`.
2. Le certificat créé devrait être listé. Vérifiez la date de validité :  
    ![Configuration des Domain SAN list](/assets/images/pfsense/13.png)

## Références

- [pfSense : Créer et gérer ses certificats LetsEncrypt avec l’API OVH](https://vdays.net/fr/2020/05/01/tuto-pfsense-creer-et-gerer-ses-certificats-letsencrypt-avec-lapi-ovh/)

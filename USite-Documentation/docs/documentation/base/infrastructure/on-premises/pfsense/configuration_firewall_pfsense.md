---
layout: Part
author: "Loïc OUTHIER"
---

# Configuration d'un Firewall pfSense sur une machine virtuelle

[[toc]]

## Introduction

PfSense est un système d'exploitation open-source basé sur FreeBSD, conçu pour être utilisé comme un pare-feu, un routeur et une passerelle de sécurité. Dans ce README, nous allons expliquer comment installer pfSense sur une machine virtuelle. Puis, la procédure à suivre pour configurer les règles de pare-feu. Les règles de pare-feu permettent quant à elles de restreindre l'accès au réseau et de limiter les risques d'attaque.

## Prérequis

Avant de commencer, assurez-vous d'avoir Une image ISO de pfSense téléchargée depuis le site Web de pfSense ainsi que le matériel nécessaire.

La configuration matérielle minimale requise pour le logiciel pfSense est la suivante:

* Processeur compatible amd64 (x86-64) 64 bits
* 1 Go ou plus de RAM
* Disque dur de 8 Go ou plus (SSD, HDD, etc.)
* Une ou plusieurs cartes d'interface réseau compatibles

## Installation de pfSense

### Création de la machine virtuelle

1. Lancez votre logiciel de virtualisation et créez une nouvelle machine virtuelle. 
2. Choisissez le système d'exploitation FreeBSD et sélectionnez l'image ISO de pfSense que vous avez téléchargée précédemment.
3. Configurez la RAM, la taille du disque, le nombre de processeurs, etc. en fonction des ressources dont vous disposez.

### Configuration de la machine virtuelle

1. Allumez la machine virtuelle et démarrez à partir de l'image ISO.
2. Choisissez l'option "Installer pfSense" dans le menu de démarrage.
3. Sélectionnez la langue d'installation et appuyez sur "Entrée".
4. Acceptez le contrat de licence en appuyant sur "Entrée".
5. Sélectionnez l'option "Guided Root-on-ZFS" et appuyez sur "Entrée".
5. Sélectionnez le disque dur où vous souhaitez installer pfSense et appuyez sur "Entrée".
6. Une fois l'installation terminée, redémarrez la machine virtuelle.

## Configuration de base de pfSense

Maintenant que pfSense est installé sur votre machine virtuelle, vous devez le configurer pour qu'il fonctionne comme un pare-feu et une passerelle de sécurité. Voici les étapes à suivre pour configurer les interfaces WAN et LAN, ainsi que l'accès à distance à pfSense.

### Configuration de l'interface WAN

L'interface WAN est l'interface de réseau qui est connectée à Internet. Dans cette section, vous allez configurer l'interface WAN pour qu'elle soit connectée à Internet.

1. Connectez-vous à pfSense à partir d'un navigateur Web en utilisant l'adresse IP de l'interface LAN (par défaut, l'adresse IP est 192.168.1.1).
2. Sur la page d'accueil de pfSense, cliquez sur "Interfaces" dans le menu principal, puis sur "WAN".
3. Dans la section "General Configuration", sélectionnez "Static IPv4"
4. Entrez votre adresse IPv4 publique fournie par votre FAI.
5. Cochez la case pour bloquer les réseaux privés et les adresses de bouclage et bloquer les réseaux bogon.
6. Cliquez sur "Save" pour enregistrer les modifications.

### Configuration de l'interface LAN

L'interface LAN est l'interface de réseau qui est connectée à votre réseau local. Dans cette section, vous allez configurer l'interface LAN pour qu'elle soit connectée à votre réseau local.

1. Sur la page d'accueil de pfSense, cliquez sur "Interfaces" dans le menu principal, puis sur "LAN".
2. Dans la section "General Configuration", sélectionnez "Static IPv4"
3. Dans la section "Static IPv4 Configuration" Entrez votre adresse IPv4 privée .
4. Cliquez sur "Save" pour enregistrer les modifications. 

### Configuration de l'interface OPT

L'interface OPT est une interface réseau supplémentaire que vous pouvez configurer pour répondre aux besoins spécifiques de votre réseau. Dans cette section, vous allez configurer cette interface que nous allons dans notre cas renommer DIIAGE pour qu'elle soit connectée à votre réseau local.

1. Sur la page d'accueil de pfSense, cliquez sur "Interfaces" dans le menu principal, puis sur "OPT".
2. Dans la section "General Configuration",  renommez l'interface "DIIAGE", puis sélectionnez "Static IPv4"
3. Dans la section "Static IPv4 Configuration" Entrez votre adresse IPv4 privée .
4. Cliquez sur "Save" pour enregistrer les modifications. 


### Configuration de l'accès à distance

Vous pouvez configurer l'accès à distance à pfSense pour pouvoir vous connecter à l'interface Web de pfSense à partir d'un réseau externe. Cela peut être utile si vous devez gérer pfSense à distance.

1. Sur la page d'accueil de pfSense, cliquez sur "System" dans le menu principal, puis sur "Advanced".
2. Cliquez sur l'onglet "Admin Access".
3. Cochez la case "Enable HTTPS".
4. Sélectionnez votre certificat HTTPS, si vous n'en avez pas suivez les instructions suivantes : <br>
[https://vdays.net/fr/2020/05/01/tuto-pfsense-creer-et-gerer-ses-certificats-letsencrypt-avec-lapi-ovh/](https://vdays.net/fr/2020/05/01/tuto-pfsense-creer-et-gerer-ses-certificats-letsencrypt-avec-lapi-ovh/)
5. Choisissez un port TCP différent que celui par défaut, par exemple le 8443.

Maintenant, vous pouvez vous connecter à l'interface Web de pfSense à partir d'un navigateur Web sur un réseau externe en utilisant l'adresse IP des interfaces WAN et DIIAGE de pfSense et le port de gestion HTTPS que vous avez configuré.

### Création d'alias

Avant de commencer la configuration des règles de pare-feu, il est recommandé de créer des alias pour faciliter la gestion des adresses IP. Les alias permettent de donner un nom à une adresse ou un groupe d'adresses IP et de l'utiliser dans les règles de pare-feu à la place de l'adresse IP elle-même.

### Configuration de la règle d'accès à Internet

Pour permettre aux utilisateurs de votre réseau d'accéder à Internet, vous devez créer une règle de pare-feu pour autoriser le trafic sortant de l'interface LAN vers l'interface WAN.

1. Sur la page d'accueil de pfSense, cliquez sur "Firewall" dans le menu principal, puis sur "Rules".
2. Cliquez sur "LAN" pour ouvrir la page de configuration des règles de pare-feu pour l'interface LAN.
3. Cliquez sur "Add" pour créer une nouvelle règle.
4. Dans la section "General", configurez les paramètres suivants :

   - Action : "Pass"
   - Interface : "LAN"
   - Address Family : "IPv4"
   - Protocol : "Any"
   - Source : "LAN net"
   - Destination : "Any"
   
5. Cliquez sur "Save" pour enregistrer la règle.

## Création des règles de pare-feu
 
Maintenant que les interfaces sont configurées, vous pouvez créer des règles de pare-feu pour contrôler le trafic de vos différents réseaux . Les règles de pare-feu doivent être configurées pour autoriser le trafic nécessaire et bloquer le trafic indésirable. De plus, elles doivent être optimisées pour limiter les risques d'attaque et préserver la sécurité du réseau.

Dans notre cas, nous avons dû créer des règles de pare-feu pour nos réseaux WAN, LAN, DIIAGE et notre tunnel VPN. Pour chacune de ces interfaces, ces règles permettent de :

* WAN :
Bloque le trafic privé et les adresses blogon
Autorise le trafic du tunnel VPN et à l'HaProxy
Permet un accès restreint en HTTPS et à l'interface WEB du firewall
Bloque tout autre trafic
	
* LAN :
Autorise le trafic du Zabbix
Permet un accès restreint à l'interface WEB du firewall
Autorise l'accès à internet depuis le réseau LAN
	
* DIIAGE :
Bloque l'accès au pfSense et au réseau LAN depuis le réseau du DIIAGE
Permet un accès restreint à l'interface WEB du firewall
Autorise l'accès à internet depuis le réseau du DIIAGE

* OpenVPN:
Permet aux adresses IP autorisées d'accéder à l'interface WEB du firewall
Permet aux adresses IP autorisées d'accéder à l'interface WEB du Proxmox
Permet aux adresses IP autorisées d'accéder à la base de données MSSQL
Permet aux adresses IP autorisées d'utiliser le protocole SSH
Bloque tout autre trafic
 
Voici les règles créés en détail :

### Les règles du réseau WAN
   ![Les règles du réseau WAN](/assets/images/pfsense/WAN.png)

### Les règles du réseau LAN
   ![Les règles du réseau LAN](/assets/images/pfsense/LAN.png)

### Les règles du réseau DIIAGE
   ![Les règles du réseau DIIAGE](/assets/images/pfsense/DIIAGE.png)

### Les règles du réseau OpenVPN
   ![Les règles du réseau OpenVPN](/assets/images/pfsense/OPENVPN.png)

### Utilisation de séparateurs logiques

Il est également important d'ajouter des séparateurs pour permettre une lecture plus claire des règles de pare-feu. Dans notre cas, nous avons pu ranger nos règles de pare-feu en fonction de leur type :
 
* Rouge : règles de pare-feu qui bloquent ou refusent explicitement un trafic spécifique, comme les règles de blocage de ports ou les règles de blocage de sites web spécifiques.
 
* Jaune : règles de pare-feu qui limitent ou restreignent le trafic en spécifiant des limites ou des quotas, comme les règles de limitation de bande passante ou les règles de quotas de données.
 
* Vert : règles de pare-feu qui autorisent le trafic pour des adresses ou bien des réseaux entiers, comme les règles d'autorisation du trafic d'un réseau vers un autre.
 
* Bleu : règles de pare-feu qui autorisent spécifiquement le trafic pour des utilisateurs, des sites web ou des applications spécifiques, comme les règles d'accès SSH pour les utilisateurs VPN ou les règles de trafic autorisé pour des applications tierces.

## Configuration supplémentaire de pfSense

### Désactivation de l'utilisateur admin par défaut

Pour des raisons de sécurité, il est conseillé de désactiver l'utilisateur admin par défaut. Il est recommandé de créer à la place un nouvel utilisateur avec des droits d'administrateur et de supprimer l'utilisateur admin par défaut.

### Création d'un groupe pour les utilisateurs VPN

Pour faciliter la gestion des utilisateurs VPN, il est recommandé de créer un groupe spécifique pour ces utilisateurs. Ce qui permettra ensuite de pouvoir définir des adresses IP statiques pour chacun des utilisateurs et ainsi restreindre l'accès au réseau LAN.

### Axe d'amélioration

Pour améliorer la sécurité du réseau, il serait possible d'identifier les utilisateurs via un certificat plutôt qu'un nom d'utilisateur et un mot de passe. L'identification via certificat est plus sécurisée car elle permet de s'assurer que l'utilisateur est authentique avant de lui donner accès au réseau.

## Conclusion

Vous avez maintenant installé et configuré pfSense sur une machine virtuelle. La configuration des règles de pare-feu est une étape importante pour garantir la sécurité du réseau. Les étapes décrites dans ce document doivent être suivies avec attention pour limiter les risques d'attaque et préserver la sécurité du réseau.

## Références

- Site Web de pfSense: [https://www.pfsense.org/](https://www.pfsense.org/)
- Documentation de pfSense: [https://www.pfsense.org/](https://docs.netgate.com/pfsense/en/latest/install/install-pfsense.html)

---
layout: Part
author: "Loïc OUTHIER"
---

# II. Domaine d'application

[[toc]]

## 1. Objectifs du système

Le principal objectif de l'application CMS "USite" est de fournir à nos utilisateurs une plateforme intuitive et complète pour la création, la gestion et la publication de leurs propres sites web. Cela inclut les fonctions suivantes :

Création de Sites Web : Les utilisateurs peuvent facilement créer leur propre site web à partir de zéro ou en utilisant un de nos templates prédéfinis. Notre objectif est de rendre ce processus aussi simple et convivial que possible, même pour les utilisateurs sans expérience préalable en développement web.

Gestion du Contenu : Les utilisateurs peuvent ajouter, modifier et supprimer du contenu sur leur site web à leur guise. Cette fonctionnalité couvre aussi bien le texte que les images, les vidéos et d'autres formes de contenu multimédia.

Facturation et Gestion des Comptes Utilisateurs : USite offre également des fonctionnalités de gestion de compte et de facturation, permettant aux utilisateurs de suivre leur utilisation, de gérer leurs abonnements et de payer pour les services qu'ils utilisent.

Hébergement de site web : USite s'occupe non seulement de la création de sites web, mais aussi de leur hébergement. Une fois que les utilisateurs ont créé leur site web, ils peuvent le publier directement via notre plateforme. Cela comprend l'hébergement du site, le maintien de sa disponibilité, et la gestion de la bande passante et du stockage. Nos utilisateurs n'ont pas à se soucier des détails techniques de l'hébergement d'un site web, car USite s'occupe de tout cela pour eux.

Noms de Domaine : USite prend en charge non seulement la création et l'hébergement de sites web, mais aussi la réservation de noms de domaine. Les utilisateurs peuvent choisir et réserver leur nom de domaine directement via notre plateforme, simplifiant ainsi l'ensemble du processus de mise en ligne. Cela comprend la gestion des détails techniques tels que le DNS. Avec USite, nos utilisateurs n'ont pas à se soucier des détails complexes liés à la réservation d'un nom de domaine, car nous gérons tout cela pour eux.

Performance : USite se distingue non seulement par sa facilité d'utilisation et sa gamme complète de services, mais aussi par la performance qu'il offre. Les sites créés et hébergés sur notre plateforme bénéficient d'une vitesse de chargement rapide et d'une disponibilité fiable, garantissant ainsi une excellente expérience utilisateur.

L'objectif ultime de USite est de permettre à nos utilisateurs de se concentrer sur la création de contenu de qualité et la croissance de leur présence en ligne, sans se soucier des détails techniques de la création, de la gestion, de l'hébergement d'un site web, de la réservation de noms de domaine, ou de l'optimisation des performances. Notre priorité est de fournir une plateforme performante qui aide nos utilisateurs à se démarquer dans l'environnement en ligne concurrentiel d'aujourd'hui.



## 2. Interfaces

### 2.A. Interfaces utilisateur

L'application USite comprend deux interfaces utilisateur principales, chacune avec des fonctions distinctes pour faciliter la gestion des sites web par les utilisateurs.

Interface de Gestion : Cette interface utilisateur sert principalement de tableau de bord pour les utilisateurs. Elle permet de :

* Gérer les comptes utilisateurs : Les utilisateurs peuvent créer un compte, se connecter, modifier leurs informations, et gérer leurs paramètres de sécurité.

* Facturation : Les utilisateurs peuvent consulter leurs factures, mettre à jour leurs informations de paiement, et gérer leurs abonnements à nos services.

* Gestion de sites : Les utilisateurs peuvent créer de nouveaux sites, gérer leurs sites existants, et publier leurs sites sur le web.

Interface de Contenu : Cette interface utilisateur se concentre sur la gestion du contenu des sites web des utilisateurs. Elle permet de :

* Modifier le contenu : Les utilisateurs peuvent ajouter, modifier, ou supprimer du contenu sur leurs sites web.

* Déployer les modifications : Les utilisateurs peuvent voir les modifications en temps réel et déployer ces modifications sur leurs sites web.

Ces deux interfaces sont conçues pour être intuitives et conviviales, facilitant ainsi l'interaction des utilisateurs avec notre système. De plus ces deux interfaces sont des interfaces web accessibles depuis n'importe quelle navigateur.

### 2.B. Interfaces logicielles

Les interfaces logicielles mentionnées ici sont des interfaces internes au système USite :

* **Interface avec la base de données centrale :** L'application USite doit interagir avec la base de données centrale Microsoft SQL Serveur. Cette interaction se fait via des pilotes dédiés, de manière synchronisée. Cette base de données rassemble toutes les données manipulées par l'application, y compris les informations des utilisateurs, les détails de facturation, et le contenu des sites web.

* **Interface avec le Service de Génération de Pages HTML :** Ce service basé sur le .NET Framework 4.8, le monolithe pricipale doit interagir avec ce service grâce à des requêtes HTTP. Ce service n'a pas besoin d'avoir accès à la base de données.

* **Interface entre les "microservices" :** Chaque "microservice" dans l'architecture de USite a une interface de communication avec les autres services (Connector). Cela permet à ces services de partager des données et de coordonner les actions de manière efficace. Par exemple, le Service de Facturation doit communiquer avec le Service d'Authentification pour confirmer les détails de l'utilisateur lors de la facturation.


### 2.C. Interfaces de communication

L'application USite interagit avec un certain nombre de services externes pour fournir des fonctionnalités supplémentaires et améliorer l'expérience utilisateur. Ces services externes sont accessibles via des API (interfaces de programmation d'applications) spécifiques. Voici quelques-unes de ces interfaces de communication :

* **API OVH :** Nous utilisons l'API OVH pour gérer les noms de domaines de nos clients et les sous domaines de `usite.fr`. Cela inclut la réservation de noms de domaine et la gestion du DNS.

* **API Azure DevOps :** L'API Azure DevOps est utilisée pour gérer différents site web de nos clients.
   - Pour chaque nouveau projet, nous créons un référentiel dédié au client dans notre organisation Azure DevOps.
   - Une fois que le référentiel est créé et les fichiers HTML sont générés, nous utilisons l'API Azure DevOps pour pousser ces fichiers dans le référentiel.
   - Nous utilisons ensuite l'API Azure DevOps pour créer une nouvelle pipeline pour le référentiel du client. Cette pipeline est configurée pour s'exécuter depuis le code, sans déclencheur automatique.
   - La pipeline est ensuite exécutée à l'aide de l'API Azure DevOps. Cela permet de mettre en place une livraison continue, où les fichiers HTML et CSS générés sont envoyés à un Persistent Volume Claim (PVC) dans Kubernetes.


* **API Azure Portal :** Nous utilisons l'API Azure Portal pour gérer nos les images de nos clients. Pour cela nous utilisons un azure file storage.

* **Google OAuth :** Nous utilisons Google OAuth pour permettre à nos utilisateurs de se connecter à notre application avec leurs comptes Google. Cela offre une option de connexion supplémentaire pour nos utilisateurs et aide à améliorer la sécurité de l'authentification.

Chacune de ces interfaces de communication joue un rôle crucial dans le fonctionnement de notre application, en nous permettant d'intégrer des services externes efficaces et fiables.

## 3. Base de données externes

L'application USite interagit avec des bases de données externes pour le stockage des données des utilisateurs et le déploiement des sites web. Voici une description de ces interactions :

* **Azure File Storage :** Nous utilisons Azure File Storage pour stocker les images fournies par nos clients lors de la création de leurs sites web. L'interface entre notre application et Azure File Storage est gérée via l'API Azure Storage Service, qui nous permet d'envoyer, de récupérer et de gérer les fichiers de manière sécurisée et efficace.

* **Azure DevOps :** Nous utilisons également Azure DevOps comme dépôt pour le stockage et le déploiement des sites web de nos clients. Chaque site web est stocké dans son propre référentiel, créé et géré via l'API Azure DevOps. Les fichiers HTML générés pour chaque site sont poussés dans le référentiel correspondant, qui est ensuite utilisé pour déployer le site via une pipeline Azure DevOps.

Ces interfaces avec les bases de données externes jouent un rôle clé dans le fonctionnement de notre application, en nous permettant de stocker et de gérer les données des utilisateurs de manière efficace et sécurisée.

## 4. Contraintes général de conception

Lors de la conception du système USite, nous avons dû tenir compte de plusieurs contraintes issues de différentes sources. Ci-dessous, vous trouverez un récapitulatif des contraintes imposées par le maître d'ouvrage dans le cahier des charges :

* **Architecture .NET :** L'application utilise l'architecture .NET, avec des microservices conçus en C#. Nous avons également un microservice spécifique utilisant le .NET Framework pour la génération de pages HTML à partir de templates T4.

* **Base de données SQL Server :** La base de données utilisée par l'application est de type SQL Server. Celle-ci contient toutes les données nécessaires pour la gestion des utilisateurs, la création de sites web et la facturation.

* **Utilisation d'Azure DevOps et Azure File Storage :** Ces services sont utilisés pour le stockage et le déploiement des sites web de nos clients, ainsi que pour le stockage des images fournies par les clients.

* **Phase de conception après validation du modèle métier :** La phase de conception n'est démarrée qu'après validation du modèle métier (et du modèle conceptuel des données) par le maître d'ouvrage.

* **Respect des normes et règles de documentation :** Tout au long du processus de conception, nous respectons les normes et les règles de documentation applicables pour garantir la qualité et la clarté de notre documentation.

* **Localisation en français :** Toutes les interfaces utilisateur de l'application sont disponibles en français.
* **Localisation en anglais :** Toutes les interfaces utilisateur de l'application sont disponibles en anglais.

* **Sécurité des connexions :** Les connexions à l'application USite sont sécurisées par mot de passe.

Ces contraintes ont influencé la conception de notre application, et nous nous sommes efforcés de les respecter pour garantir une conception robuste et efficace de notre système.
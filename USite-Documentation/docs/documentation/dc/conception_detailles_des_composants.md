---
layout: Part
author: "Loïc OUTHIER, Antoine MAUCHOSSÉ"
---

# VI. Conception détaillés des composants

[[toc]]

## 1. Base de données

### 1.A. Modèle Conceptuel de Données

La représentation du système d’information a été réalisé à l’aide du modèle métier et de la méthode MERISE. Cette méthode nous a permis de réaliser le Modèle Conceptuel de Données ou **MCD**, qui fait le lien entre le modèle métier et le modèle logique. Le **MCD** est un modèle dit **« entités-relations »** qui modélise l’ensemble des règles de gestion et des contraintes du système. 

Il correspond globalement au modèle métier, en détaillant le contenu des entités métiers (attributs). La construction du **MCD** a été réalisé à partir des spécifications du cahier des charges, du modèle métier et des réunions avec l'équipe USite. Celui-ci a été généré grâce au logiciel `WinDesign`, qui permet de transformer le **MCD** en **MLR** et de générer le script de la base de données correspondant.

<iframe src="/assets/files/MCD.pdf" width="100%" height="600"></iframe>

### 1.B. Modèle Logique Relationnel

Le **MLR** est le modèle logique correspondant au **MCD**. Il représente les tables et les  colonnes de la base de données, le schéma de la base de données relationnelles. Chaque entité du **MCD** correspond à une table, de même que les associations **0..n  0..n** et les associations contenant des attributs. 

Dans le schéma du **MLR** ci-dessous (généré par WinDesign), les attributs obligatoires sont indiqués en gras. Les clés primaires sont regroupés dans des libellés contenant `PK` en préfixe (`FK` pour les clés étrangères, non affichées).

Le paramétrage consiste aussi à définir le type de données pour chaque champ. Les types de chaque champs sont définis à partir des informations contenues dans le cahier des charges. On utilisera ainsi des `NUMBER` pour les entiers (la taille du `NUMBER` variant en fonction de la précision attendue, ou de la quantité d’enregistrements attendue dans la table) , des `CHAR(32)` ou des `VARCHAR2(255)` pour les textes, des `DATE` pour les dates, etc.

Un element peut avoir un élément comme parent, ce qui explique la relation `PARENTID`. Les tables avec le préfixe `APSNET` sont des tables générées par identity serveur pour gérer l'authentification.

<iframe src="/assets/files/MLR.pdf" width="100%" height="600"></iframe>

## 2. Applications

L’application USite se divise en deux parties :  une partie destinée aux visiteurs, permettant principalement de consulter le site de présentation, et une partie client destiné aux utilisateurs enregistré. 

Le choix de l’architecture à 4 couches a été effectué pour la partie backend et angular js pour la partie frontend.  Nous allons dans les prochains paragraphes détailler le fonctionnement de chaque couche.

Pour mettre en œuvre cette architecture nous avons utilisé : 
* **Entity Framework Core** pour la persistance de la couche Infrastructure 
* **MediatR** pour la connexion entre la couche Présentation et Application 
* **Inversion de contrôle** pour les liens entre les différentes couches 
* **Angular** avec l'utilisation des composants, states et du store pour la partie front


### Inversion de contrôle 

L’inversion de contrôle permet de réduire la dépendance entre les couches de l’application. Il s’agit en fait de séparer l’appel à la couche inférieure de son implémentation.  L’inversion de contrôle permet de mettre en place cette séparation. Les éléments de la couche Infrastructure sont définis par des interfaces puis implémentés dans des classes distinctes. La couche supérieure ne connaît que l’existence des interfaces dont elle utilise les méthodes prédéfinies.  Ainsi le changement d’implémentation d’une couche n’affecte pas la couche supérieure 

### Entity Framework Core  

Entity Framework est un outil qui facilite la gestion des données dans une base de données depuis du code .NET. Il propose plusieurs fonctionnalités utiles, telles que : 

Le mapping objet-relationnel (ORM), cette fonctionnalité permet de mapper les données de la base de données aux objets .NET de manière transparente. Cela signifie que vous pouvez interagir avec les données en utilisant des objets .NET au lieu de devoir écrire du code SQL. 
Il gère automatiquement la connexion à la base de données et la libération des ressources lorsque vous avez fini d'interagir avec les données. 

### MediatR

MediatR est un bibliothèque open source pour la gestion de la communication entre les différentes parties d'une application .NET. Elle s'inspire du pattern de conception "Mediator" et permet de découpler les différentes parties de l'application en utilisant des "requêtes" et des "commandes" pour échanger des messages entre elles. 

MediatR peut définir des requêtes et des commandes : Vous pouvez définir des classes qui représentent les requêtes et les commandes que vous souhaitez envoyer entre les différentes parties de votre application. Ces classes peuvent contenir des données spécifiques à chaque message. 

Gérer la logique de traitement : Vous pouvez définir des "handlers" qui sont des classes chargées de traiter chaque requête ou commande. Ces handlers peuvent effectuer des tâches spécifiques, comme récupérer des données de la base de données ou envoyer un email. 

En utilisant MediatR, vous pouvez découpler les différentes parties de votre application et gérer la communication entre elles de manière plus efficace. Cela peut vous aider à développer une application plus maintenable et évolutive. 

### Les différentes couches 

Couche Domain :  
Cette couche contient 3 catégories de fichiers, les entités, les énumérations, les évènements. Nous allons présenter les 3 types de fichiers : 
Les entités sont les classes utiliser par notre application, elles contiennent des propriétés.  
Les énumérations sont une manière de définir un type de données qui peut avoir une liste fixe de valeurs possibles. Ils sont souvent utilisés lorsqu'il y a un nombre limité de valeurs que quelque chose peut prendre, comme les jours de la semaine ou les différents états d'un objet 
Les évènements sont une manière de créer des notifications de changements de données ou d'actions qui ont eu lieu dans votre code. Ils sont souvent utilisés pour permettre à une classe de signaler à d'autres classes qu'un événement intéressant s'est produit et que les autres classes devraient réagir en conséquence 

Couche Infrastructure :  
La couche "infrastructure" est celle qui gère les détails techniques de l'application, tels que la gestion de la base de données, l'accès aux fichiers, etc. Elle est responsable de mettre en œuvre les différentes fonctionnalités de l'application en utilisant les outils et les technologies nécessaires. Elle est utilisée par la couche application, pour accéder aux données et aux autres ressources dont elle a besoin pour exécuter sa logique métier. La couche "infrastructure" est donc une couche de support pour les autres couches de l'application. 

Couche Application :  
La couche "application" dans une architecture Clean Architecture est la couche qui contient le code qui utilise les règles métier de votre application pour accomplir des tâches spécifiques. C'est l'endroit où vous mettez le code qui coordonne les différentes parties de votre application pour accomplir des tâches concrètes. 

Couche Présentation 
La couche de présentation est la couche de l'application qui gère l'interface utilisateur et a la charge de la communication entre l'utilisateur final et les autres couches de l'application. Elle peut comprendre des éléments tels que l'interface graphique de l'application, les contrôleurs de l'application web, etc. Elle doit être indépendante des autres couches de l'application et ne doit pas contenir de logique métier. En utilisant une couche de présentation, vous pouvez séparer clairement la logique métier de l'interface utilisateur et rendre votre code plus maintenable et évolutif. 

<img align="center" width="200" height="200" src="https://mahedee.net/assets/images/posts/2021/clean.png" />

### Composants
Les composants sont l'une des principales caractéristiques d'Angular. Ils permettent de structurer l'application en éléments réutilisables et autonomes, dotés de leur propre logique et de leur propre affichage. Les composants d'AngularJS encapsulent des fonctionnalités spécifiques, ce qui facilite la gestion de l'état de l'application et la manipulation des données. Ils peuvent être utilisés pour créer des formulaires interactifs, des listes de données, des modèles de présentation, des menus et bien d'autres éléments d'interface utilisateur. Grâce à leur modularité et à leur capacité à communiquer entre eux, les composants en AngularJS offrent une approche structurée et efficace pour développer des applications web robustes et évolutives.

### State et store

En Angular, les states et le store jouent un rôle crucial dans la gestion de l'état de l'application. Les states représentent les différents états de l'application et sont utilisés pour la navigation entre les différentes vues. Ils permettent de définir les paramètres, les contrôleurs et les templates associés à chaque état. D'autre part, le store est utilisé pour gérer l'état global de l'application. Il permet de stocker les données partagées entre les différents composants et de maintenir un état cohérent et prévisible. En utilisant le store, les composants peuvent lire et mettre à jour les données de manière centralisée, simplifiant ainsi la gestion de l'état dans l'application Angular.

![State Management](/assets/images/dc/state-management.svg)

Dans cet exemple, on va changer le libellé des éléments, pour cela une action est déclenchée depuis le store, cette action va déclencher un effect du store élément pour appeler le service afin de mettre à jour le libellé, une fois que le libellé a été modifié en base, depuis l'effect on va pouvoir déclencher une nouvelle action pour utiliser le reducer qui va mettre à jour l'état du store. Ce qui va mettre à jour tous les composants concernés.

### Flux d'authentification

#### Flux d'authentification sur la présentation

<iframe frameborder="0" style="width:100%;height:731px;" src="https://viewer.diagrams.net/?highlight=0000ff&nav=1&title=USite%20-%20Schemas.drawio#R7V1Zc6JKG%2F41qTnnIik2ES41xplM9j0zN18htEqCYFgSzcX57V93QyNLR1FBIDJTNSMNNNA879Pv2hzwx5PZT1uZji8sDRgHHKPNDvjeAce1eR7%2BixrmfgMrybLfMrJ1LWhbNNzpnyBoZIJWT9eAEzvQtSzD1afxRtUyTaC6sTbFtq2P%2BGFDy4hfdaqMQKrhTlWMdOuTrrljv1Xi2ov2X0AfjcmVWTF4volCDg6exBkrmvURaeJPDvhj27Jc%2F9dkdgwMNHhkXJ5O50%2FG%2Bav48%2FeN86Y8dM%2FuLx8P%2Fc7665wSPoINTHfjrv%2FctVTztX%2Boqnf90wtd5n%2F3nUO2TR7OnZMRAxocwGDTst2xNbJMxThZtHZtyzM1gPpl4NbimHPLmsJGFja%2BANedB2hQPNeCTWN3YgR7wUx3n9HpR61g609kT28W9Iw35sGG49rWa%2FgOWXxmxoEJBtCxPFsFS0ajTQCq2CPgLhs2AhI0VhGcBSP%2FE1gT4NpzeIANDMXV3%2BNgVAJMj8LjFu8N%2Fghe3Rqvkdz4u2J4waXOAZIW2zJdYGrop6K76NLTKRbwg2P%2BoAPfPWP8UKY6GkQ4OOh%2B8Q5Z9ab%2BDxvY%2FkEqcBwsu6%2FAxMKO93MquoziJXr0ICJs%2FRM%2BuWVi2dZACmYROFjvwB4aWLYGhqW%2Bwib8%2F91UUXVzFBz1MdZdgJpQBx%2BQrWDbED5fgDJWPGoHLceWYdn4Knwf%2F4HtiqGPTNimQqjAZ4J4CjuXQ3RFThQYvteSwj1X6GgXYZFlmJgIoFtTBo5leC7o2Cq5G9Qabskp%2FHJHPGwzPFXXTnEv5vVh13Fm50fP9%2BgpdMOg3cx6gIfD6oLZUnwGezlJbkOJwmcRpg9B%2FrHgTa4VHDSOcKYsFQVrsVRyilDTgqjo5ARfhz2PMBra%2FBPdtzgNb8VJLfKmRbHfF8Vc6C4%2FbsKndmxbmUcOmFq66TqRnq9RwwJSvCxzRy0hhilekOJzVIZzBJFZeg6EH7PiHPjDv3t6DwIrttN3GsoDeWB%2F%2BgjOXXVDTOKGZCbemT%2FFpDrDYhK%2Boi0kp52aEA440YCo6Wr6e0ykxDcPaTCYOA8dLBQdeADLT2cYcmQ%2FuvmAQxeNAZtGDhJH6P8rTyfXg%2FePL%2BnvoAr0uTKAk1JMCFN0jbhMh2pdJ9gx0TXNl3cAb1oZ4P6QSAW4hJ23ugetHpkkgiuyiUmDp4rVUjYKNNXgkgv9MDvbHkLAignAHQab6wllCnwsw8W7ZZkEO1vDoQMKAZ3El0nXbBl0LcsMw%2BxKOxUyaqdS7hPAVrO4nOaiS6QW1pYICM5zIQKekxNEwG5HBIluihd7gsuo8fHDc3VDdxQXYKsitA083xyYTF1kN4wsa2SgH%2BjGkACMoZGCtp0fwLTBSIdShe0P5R2oqBu%2BX4wZYY%2BtycBz8jIo%2BCOJRhYnbbbHL3S7XZgUJ%2BLgyHl97qZNivBmijIpBJaPGxScxGSzJ8IT8zco5GaGKnCGErLOUIQyKjJDEWBStGUk%2FRuqy0Th9TuC7GCSNjz9hdpxdE%2Bk2b9yVbTmjCqynOPMKMiSmOvMuAMNuFUmv6zjTd2OXwplESmrF1aqGItQnLAJPUg19DcPKzh4M6nmYDUIq0hDy554hqLb2L06hWOL3beNC3WFvnN69XzVOTodpvWdwl2oAk98PMSFJGfUd5K%2BofwQKTV0tD0dZTa7xWrR0WqzzAaTqaEjisFWWJx0fJNLBSjqo5toHw7nOISnElyGexhYnuvikM9CEbqLk9xCP2rIrEZk1hKyGm9MUWwm18a9WKoRRmhoJV%2BRN1wRvpLFFF%2BV6SaMuAU3sYTkXH2EIumuuqaPXKprpe6x2hylWspbqjeK%2FgpcW4pNIO22lIBdJCSbFwjDySdCIjgUWbb7ZONYg5ynR0VkZDb2UrZ0qJCe2biusLvQg5SeM85pCqsGJgokGmxz%2Bw492Dk%2BEA%2BtbaPMwya%2BsL6K%2BnLWUu7%2F6z%2BlVdTi4wtJFbXNlh5fkNP00xkOdXWMklQhDH9g584EOA5pABB7yH5qjKEVSFNfR0f34MkrwxgShTjSpHbZnh2ZSwFtGYaiShQrZYALT3vrIVxo%2Bk7inS%2FeNYcUqZWaoAMVG7eD8r3hIaZlAqx%2BaaSFIB829XUjm212DWwdjjbCc1zvY2NaHz4%2BjrVWd%2FB8IfWZNNbCIVimKOas%2BhEQ5ecPj8A21JioNnxeOuKXuXJCm4SMEpl7u0qUY8UmZJMDRKWMEGXFasVsyI0nE%2BfDFBXgYF9nkBafiNJArVLH1TLA70LXSMa7fxow3605PieSHB9zwTbTfoWnfbZVtWmfJWnF1feBRtiKYaT4jCsKwgrOwlvJCbxUtyrLZA1Li7n7VTfLv29xSfiuSL5fcUJRLptWioJr7PcNJTQXhw18B4myhE30snSSOK3P4r01LNPU81SDolrtSmlhLJNOES%2FTbbs1B%2BRZKcInfLbcdhRAfLTc7qR%2BdV5UqB5jJdrXtSO%2B2kDNbtyzNXLP8qyY0CbKd8%2ByYpMQlcM0I2bNiBIqGotkiWX3pVuKSYB3%2FTMEZvkJvMxveQK%2F4pZ4obXtCeySMtr8PHCrE9XwtADSMwJ2xUyBh%2F6DTWitA8XP1weL6Rc0fpZ6%2BVlYhind0ULWPvi6JEQNh2iR0sgz%2BE%2B6CiQJ6MCtGKx5E%2Bg7fvcDe91ikC%2FV3bRi7GizU6jm9lpy%2BkVrA60lotNGtqLpYAHlIBzjjJUpuqIDpUzVjX%2FMx95tz7oY9a4%2FHhkTPPzWnPnJAde9Hc7PZsczp%2FNgXrXfx%2B3pyfDxbe7KLY1re%2BrLyYCf%2FzzTpYve7aVjPvw5Z7vOaPIw6FyOb7lfHy9%2Fz%2BT718Hvv%2FxoPOEsQVdOxQ7%2Ffv0qdO%2FunobyqTt%2FuzE%2B%2BdHbn7YFr8YdO%2BbprdqfaxcauH%2F5OztmnNefzx%2FX19PL64dTVRqCK5cxeKmjzy%2BhSdEXHn4Jc%2F62J7sv8jXs4HLefrplX9549kX7hNvS4Gz6%2BM52btCo8L1%2FaQIaDlUgyPdYEzhkFy1dy3WtSaLxHAzdRNNtAGbcZiDbpqtAWcQinhj%2FuB4SoTAaORUlrLSFQgSOIq7EVxoV12RsJz9xJesd7Fixq7CS1uayKmnVisiEN14Q80arjBNKQsO%2FDf9WnX95UUro6Dvk3icI%2Bc8Pzr19fn06nKvzE5M5O0ynPKVE7gcJh1Jq%2Br9YEaDx9dSo1J9nkqDcqa%2BHCsu003EZntZPkDri04Dx%2F6S8xjQArRUnKDO3ih41%2BTK36itNKHlCHLtgeOP0334ZaeyGMri5frSMtWqUKsXJQtyLVeCaYtQhS%2BtltBgNpAw3Pj0mgy5fzLIaGCoe1reyx3NoSfZR8DDob0JyOZr4hTBbA37LWGc1dW4ZwhWLiN9QH4mvF5EuzQmpUZLqZkTKP6mS2nF%2B7pxIo2Ym9bhW1Qk3L3lJe7Np6SyVp8nlGeBb0iThlMJp8rAl74on02lMKacvlF%2Ff7%2FBVTT2JczThiuqWxvN8YsE%2BgdTo7SJaQYVeugou3yl6KRVUbZ6lR9lznmd%2FTzrTT4XhdjfPtjLOsyy%2FLxMtbYnjvUX99iVQGVCfoS4qZ9RT6kr2W7tM16s0Baa1z4DgUwWmtIjqTlUKWvpDY%2FVX1Oq%2F%2BLg58y7A1e54mWgZK4k5awZg7YmZTcfBamn3F%2BoeJaySo92%2FXZCITb2gppwu7%2FwU%2BshTcoi3IpB9K6ajj2o6RlOPWrql0rnfpXRL599m4aPe%2FlXWJB2QItkuMdtijW%2BANDVe3waJ5dZ40ZFIWdGjyUdr8tFKx2XNPCpNQtpuE9LYyntKKpiRlm211%2F32ubAFOF3Kzkkj91YXMt1v9%2FTuk9LYrHFDbm%2Bi5URC6u6fLjQvjcuuatYnMY2j%2BIqazLQmM20XYWQu7RfKd6JeygZVm22%2FaWoalzUYzLN7M9ums4H3GPffNDmNo3yTZd%2B1zHQqcpOf1uSnFaBYFJ3923gAap2gRlSN1dyc9aud9efmdGSslh6AQr2lIa9UJUWNK%2FW7evucosZzeVNIk6OGhjVbomxNctSIeDY5al9OwU2OWm%2F%2FMoMqmKNG%2BLzJUdsrJFYwR42nfLaUukCsDTTd1kchEOFjI14Ms9Ki6x%2FjGA46VfVJVoMN%2BlBXsM4QntK4WVZA9RKof92Hi04JbpaFW4WQ5i4%2FOEVHalNkHbGGdhK%2F%2BTx8BG32nRJA3NBXsjosU3kXyOrUs5aUEAHfvMsj9ezm8OTyqfu%2Fk6c7sf95fvMqedc8Wfbo%2B35IYj2MrVyPmHxxaOVyxO2sMcLdLEfMZByHxJcVsnQtrFEzEeiiL2FDPE290UjXneavrjt%2FtdsJxSO9RCNdhpC1lvBtx7iLlXZpG9GxWPQSQrRwSphi3mSnr6civF4YQJmrWhq8qgYG0mBt8K6kb8JV%2BX3yp07hFPqTZvtm3d6EU5bzSgnhlKX4bMrD6lgeltMEXG55GB2X6fKwJkm3Wkm6OYGv1CRdum22%2BhMOqqG%2FeRh8eBNz3sDyXBd%2F7X7x1ZW7xCdVCG0Solx8naXB6AqMCsyV8uiZbyVgNOWIDAv7ysNozSq%2B9ttA2ah8djsDhVLxtZTsvr%2BB0krHOxsD5UteqWd1LP2Zmk821Cg3dqPq2N1wJTFK9oArm682ZODKmn%2B2gf5QlO82NFlqVc8NKsgW322WGh2PzcccotZKZStmt5uB5azWyndc2H5pgkCUhxf%2BoizJcRHXlON%2F4HcKZ2f4wjCHL3xRBo3eTb8bFVUm6tjNOrVwfxHex75%2FRUXHoHcHBSWtITR%2BrKR0dR1ndn70fF%2BGH0tm4n4sOXPdolQQvYtpX2tTIFtigWxeKR1lFsjSgVYzh%2Bl%2BOwE2KpDdSgVpUT4otZSxvr8KQjx0dXcCFOowFQtwmG5HdEV7O2tlMFV3qZWt2IqQUOOyXAyJlIL5N0tBX0%2Fq8kZc1oxHYrhWJGFdXJ1V3tSWlWShbFRbVlBKx05ry%2BhIbdZIW0moVagt287MyDpxE97ag4k77eG%2FtLx3oHiIYBXDT7T759iazv9Nv428qo%2FEDJWdyHxBUqXZ1vSevMBwLQ1gn7wDf0kN1KZC0VN0EyOPxduGoUwdfWEW2UD1bAe%2BkVtkL5HWbe2R7BnmhApbcpwK28nawdwojiWJxUW8QZZNu3h6ujKylckEuw7RP0PDmxV4C5T64HOduMXXuyL8YVvI2b%2BQNcjp4wtLQ2vAnPwf"></iframe>

#### Flux d'authentification sur le CMS

<iframe frameborder="0" style="width:100%;height:731px;" src="https://viewer.diagrams.net/?highlight=0000ff&nav=1&title=USite%20-%20Schemas.drawio#R7Z1Jd6O6EoB%2FjU%2Fft0gOAjMtPWTqOM7gjL15B4NsK8HIYfCQxfvtT2KeYogDtnPb6UWbAgkhqj6VpJJocJ3p8sxUZpMrrEG9wTLassF1Gyx7xHKA%2FEclK08COFb2JGMTab4sEgzQB%2FSFjC91kAatxIU2xrqNZkmhig0DqnZCppgmXiQvG2E9edeZMoYZwUBV9Kz0CWn2xJNKrBjJzyEaT4I7A8F%2FvqkSXOw%2FiTVRNLyIibiTBtcxMba9X9NlB%2Bq09oJ68dKdfnI2LJgJDbtMgtvXmd77fd52wHB5ez7hfg%2F%2Bi46aftnsVfDAUCPP7x9i057gMTYU%2FSSStk3sGBqkuTLkKLqmh%2FGMCAERvkLbXvkvU3FsTEQTe6r7Z0mBzdUzTX%2FMB4cvfnbuQXeZOFr5R9kn9ivBwo6p%2BuUfcJeT98ebD3A2OD0RR1MEzeujQHMUcwztNdXhX0frIHYDvz7PIJ5CUh5ygQl1xUbzpI4ovqqNw%2Buit0F%2B%2BC8k%2F%2BWsK%2FVc0R3%2FTg1W0En52yNMaoAqvY5N94zw7lA9IlXEMe5fXCSM6f%2B9X46NdGQpNnRMr9ImCrIhLbmqNjpcoyVrkJ5xf5McGF2hiow1NEIqeV5sUEuGblr603JTeyUamsF9Agl5ZK%2BYgTilaHNo2iRbvaWjsUFEU6Rprn4pvkAlr5mUh2vPLW150SUSXqbPjnS9Ez44pw01XqDJxqaiIZIkOGdgg4qJ2c3oHS0bGirS%2FzEeu3ddfDXu3iweGQM%2B%2FNas1UmDbd%2BNVpfLztJqPRjX4nwizk5Gj%2B8rW%2BY1VnTU15Mhtzq7RNJV965vGQ8vPdC2xtOHYas%2FuWPPF69%2FLuX7t%2BHvP9x4MmVxEykXQoub37w124PB00i%2BsFfvt%2FoHN35%2FETG5G9uxjIs79XSlXWnw%2FvXPssNYb2fPi5ubWf%2Fm4UKVRvDaZnROaqFVH5GqbD6cN1fcXVe2X%2BUbkkF%2FJT7dgdd3DrxqH%2BRYGl7OHuegdUtrhev%2Bhz65beI3mFdV1kxRkTG%2Bd%2B31CESSNrZtPE0Je3Bkp0R3PvFcma4Mod5W1LexS4ZU%2FSdpEaPAYkLUZ0Dyo29nQZqPdRZOlQUu19qkf5YDog9cv8nh%2FMNFhG82uGQSQ7fAfN%2BMc6Ei7ISxS2THEEuOXmJnIsDSg4CvtXJ5HW8LuczuisvrSh3jcpqt0KJ41jyovoY8DZwUV0xL5tB6M71ji9SeD9mUusReKyaGMNJdN2KoY%2FWNkpf%2BP%2FDsco1pURb72gKEY9GXxOhw6v7l8dcKM5fzsNJkCJml8Mw1vdqmugEYJqHKtGjK0MK6Y8OWqQalodLwKLpD4G6xxxyFjKMi7cLNxbg5urh%2Bvm4dX4yyzUFYmGpQIhJLiMMECDk04fksTeS6aCIeaFJAiUKacHtFE7aQJhZ0C2NolCmKYQWemUVY4jllkbc3IJZvxZ0%2FxvK9PcMF0HRGfbYDYPYVMBy7RcAg3nwxBHFhCPJdH7B9o8X2A%2Bs4AOZTcMQBk1uF%2B%2BWucIWAUXX07sCQFTr9OcTkJSXZcuX2BGF4id%2F%2FC1HToSXRQ%2FfHhBoy0di93s9YCUZcCMh%2BES2YkFcQ61wesLSvWGpKW8TSg4kg%2FG0vLtuGuFL7H48XAg5s6mdhieESYDpmmnIBnNyjG2giUmtUCcsTC3euhVWnaSq90XgEcGd%2B8pI78JVbuzsb%2BFpX6jixKDJGJrFczw3qXA2oQEEUNMps5g47R%2BNWv5QZojWGXez4HSzVmXk%2FTBdJDoWbgZ25m5Yww6KOlU0skJ4Ye4lYFQb9tHjuhFzYRB%2FBmJiKtYNTVUyvtmUte8fP9%2FXSi5WzvTYxGJUvxJdUk0r%2FSHx9xatKvn7gJt6cWyyfBdc6VFQHLjdpyzSVVeyCGUaGbcVyvqGCeIvJ8J%2FpXKQ1Xq6RDoXF21ytin0765fnjUHDhGNE3pILP2UO1YCBQaeQGWM8dh27BndaD87MCZ4OHasqsHHHUh7aTkTQ5YStou1EGB5bb8%2FtLNrCwtTjmMmgpGMGOL4mtDUzOrhOeeLEAlKJZo9zqzulHd5fcKV%2FI5CrLWntEITTU0H4hFgp3Yh0gqXAKyS2RXhlt%2Bi0cyOcACEOSyAJrIWITpGeoXSM0RGx075gIZ%2FTCZKKCke31un7uZ5V1NDgNqd22YG1mK5Sk8zqaiCsitrRpHga3AybATeT7sN4jZCfMmUsFTBc%2BFn2I8vu%2FPL%2B2Q%2FYhv1wT6qktqyzOu2n0OmR9t3OqjINMWMa%2FZxxItJ62knH14QW6Z0N3Qvcd5g%2FRa3BkeK4wQrZmIPPohPSBpN2Wxj6L22CeXYU6kt5PVqLj2Iv4mtvOcPKI15OJsGjkQVrQaJU6NZSU3XHIOlsB4OMETanbnecHtJC0cTo0C3fn0HFpgiyLW3ZbnkFo4q5iibX3PauNfx9a0DzhwkqbkB%2FT1uzD4Vha2lApZINKBD%2BlhY06B4eNHwDFxEk9DtS9%2FUazreHz1eS5wRUruEgZ0T%2F7%2FYRQXa2oDUaIXUSzDO6g1xTaFmBAJqm60QcPIMCz4D4y8f38Mmp2TMQQWbAvrnNMIh8tWJrBuehW16hV3G1uL10ruB1PcwVyroVZWcjfj50sxMPP7JnztbZMw8RUmHX%2FHtM281yn7UgKJyFZBgp6YYJzWYg%2BEoQxdeI%2BQ1aiFXToua5S17ONr%2BiFFe2TdLUM98J%2BFLcoVXao2RJamJ5lqQx5VciyZxvN%2FhuTicoSZ%2BSgPDt0V8X4N%2BvES4ZLO9DHTHHLCs1E2%2BE26QJybxqkJdnpSOAuQFJu41C3YxUG8RL5M1LbsNREatGz7di%2B7IzyNcO%2BgFGvU5zK7FpgWeTkP2eSdc%2FcA%2Byk5m9vBF6DU4Vw1ttanlrWUnm7oVu1ZEOukqFh0CUr3bZXy955f5%2Fp09Z77XSQJScwXxW9s14d5EoIDtfmJ43CqM43emhcCVeoIRTxQ1gP%2Bjdnuody2X9zt1HQAHp7%2FVWdtR5CgJEK%2B48lYrs5NntBgiB7Czlz%2FCP1hpLJQ4SJyf7J%2Bz3HCQ%2FlyBau35%2FKdwc5l8eF152cnBzcoS7A%2B0NOXJCC7n0JhPeY2XIsZcYCpvron0P%2FIV%2FoXNFLJp69mF4uRueM4OOF5buR6SrXjdB81YEKu6QVZjkMFNX4H71ofrHfrhqZd2vapfWBPMSodMv7nqeLkD%2BIcAhn%2BhJPfk4eoQimOfEeoWPtKMF17kLqUszfe8n3ko1D0DatHnIySyzoVHdzUN2SrBaQzysAyk7Yf7lJR8bWW%2FhNgg%2F0CgBn1lgJaaWhH7HJre9tIQtN7L%2Bd0%2FTh%2BCqPYJe2F6%2FMjtP2ou26ijjtMe2AvH2%2BWBmRCmiPcqCJaR63vC%2B4a%2B4p0F3yI3T95fix8b93bA8RU2uuz84%2Bnu7hj7sbm5jDf269mSDjWzSO9kMYDQAFV8PHfQ3oz1tDgpZoJBN5lp5dIz3bfc8w6Z0Zxtl7TSYaidDd18LUVi3NVZhj25ne%2FTlljrbmu5oAD5b8etUs5ohdtCUNnG%2Fawo7yH3e3W6xm9tfKzLFhCFGdlnWFMPgxfpNcT%2FCFFmJTbYAoj9X%2BFn%2FK5MASKDaCMXcWs0uXd1RgGJJVAgVokIGPJeo8qNKYxDr50h2LLnnbbRqWbHd7%2FXPYkrgcoa82YxDAMmP2UFH4LcZP7Ju972aPvTg9e%2FTe4%2FHJ%2BGm0caehy87HL7ssKdfdmDl1JcdgJw3%2B1jRtx3IYfRtHq99iT5xxJ38Hw%3D%3D"></iframe>
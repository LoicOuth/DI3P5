---
layout: Part
author: "Antoine MAUCHOSSÉ"
---

# IV. Normes, standard et outils

[[toc]]

## 1. Méthodes de conception
Les méthodes de conception sont utilisées pour améliorer la qualité de la conception finale.

Une méthode de conception couramment utilisée est MERISE, qui a été employée pour mettre en place le Modèle métier du système, c'est-à-dire le Modèle Conceptuel de Données (MCD). Grâce au MCD, nous avons pu définir les différentes entités de la couche Domain de notre application, et générer des migrations pour créer les différentes bases de données.

Une autre source importante de recommandations en matière d'architecture est la norme IEEE Xplore (2022). Elle propose une architecture en 5 couches pour notre application nationale. Selon cette norme, l'utilisation intensive de vues, notamment le Modèle-Vue-Contrôleur (MVC), est préconisée pour la couche "Client".

Les designs patterns sont utilisés pour améliorer l'architecture du logiciel. Ce sont des modèles de conception réutilisables qui abordent des problématiques courantes de conception, indépendamment du langage utilisé. Ces modèles de conception offrent un solide support pour la mise en œuvre des principes fondamentaux de l'approche orientée objet : la flexibilité, la réutilisabilité, la modularité et la maintenabilité.

[https://learn.microsoft.com/en-us/archive/msdn-magazine/2001/july/design-patterns-solidify-your-csharp-application-architecture-with-design-patterns](https://learn.microsoft.com/en-us/archive/msdn-magazine/2001/july/design-patterns-solidify-your-csharp-application-architecture-with-design-patterns )
## 2. Environnement et outils de développement

Pour développer notre application web, plusieurs éléments et outils seront nécessaires. Voici une description détaillée des éléments mentionnés :

Ordinateur avec système d'exploitation Windows : 
Pour développer l'application, il est recommandé d'avoir un ordinateur équipé d'un système d'exploitation Windows. Cela permettra de bénéficier de l'écosystème de développement couramment utilisé pour les applications web.

Éditeur de code : 
Deux options populaires pour l'édition du code sont Visual Studio Code et Visual Studio. Visual Studio Code est un éditeur de code léger et polyvalent, tandis que Visual Studio est un environnement de développement intégré (IDE) plus complet. Ils offrent tous deux une gamme d'outils et d'extensions pour faciliter le développement d'applications web.

Navigateur web : 
Il est essentiel de disposer d'un ou plusieurs navigateurs web pour tester et déboguer l'application. Les navigateurs recommandés incluent Chrome, Firefox et Safari. Il est préférable de tester l'application sur plusieurs navigateurs pour assurer une compatibilité maximale.

Serveur web : 
Un serveur web est nécessaire pour héberger et exécuter l'application. Il existe différentes options pour cela, Nginx et Internet Information Services (IIS). Le choix du serveur dépendra des besoins spécifiques de l'application et de l'environnement de déploiement.

Bases de données : 
Dans le cadre de notre application, deux types de bases de données sont mentionnés : les bases de données SQL et les bases de données NoSQL. Une base de données SQL, comme SqlServer est utilisée pour stocker et gérer des données structurées selon un modèle relationnel.

Gestion des dépendances : 
Lors du développement d'une application, il est courant d'utiliser des bibliothèques et des frameworks tiers pour accélérer le processus de développement. Des outils de gestion des dépendances tels que npm (pour JavaScript) ou Nugget (pour .NET) sont souvent utilisés pour installer, gérer et mettre à jour ces dépendances de manière efficace.

Processus de build automatisé : 
Pour simplifier et automatiser le processus de compilation, de packaging et de déploiement de l'application, il est recommandé de configurer un processus de build automatisé. Cela garantit une intégration continue (CI) fluide et une livraison continue (CD) de l'application. Les pipelines CI/CD d'Azure DevOps sont mentionnés comme un exemple d'outil permettant de mettre en place cette automatisation.

En résumé, le développement de l'application web nécessite un environnement de développement (ordinateur avec Windows, éditeur de code, navigateur web), un serveur web, des bases de données (SQL), des outils de gestion des dépendances et un processus de build automatisé. En utilisant ces composants et outils, l'équipe de développement pourra créer, tester et déployer efficacement l'application web.

## 3. Convention de nommage

Lors de la conception d'une application, il est essentiel de suivre une convention de nommage cohérente afin de maintenir un code lisible et facilement maintenable. La manière dont nous nommons nos variables, fonctions et classes peut avoir un impact significatif sur la compréhension et la collaboration entre les développeurs.

Voici quelques lignes directrices à suivre lors de la définition des noms de variables, de fonctions et de classes : 

Utiliser des noms descriptifs : les noms de variables, de fonctions et de classes doivent être choisis de manière à refléter leur fonction ou leur rôle dans l'application. Par exemple, si vous définissez une variable qui représente l'âge d'un utilisateur, vous pourriez l'appeler "userAge" au lieu de "a". 

Utiliser des noms significatifs : les noms de variables, de fonctions et de classes doivent être choisis de manière à être compréhensibles par d'autres développeurs, même si vous êtes le seul à travailler sur l'application. Par exemple, "positionElement" sera plus significatif que "ep". 

Utiliser la casse appropriée : il existe différentes conventions de casse, telles que camelCase, snake_case et PascalCase. Choisissez une convention et utilisez-la de manière cohérente dans tout le code. Dans notre cas les variables seront en camelCase et le nom des méthodes/classes/propriétés seront en PascalCase. 

Éviter les abréviations non courantes : il peut être tentant d'utiliser des abréviations pour raccourcir les noms de variables, de fonctions et de classes, mais cela peut rendre le code difficile à comprendre pour les autres développeurs. Évitez d'utiliser des abréviations qui ne sont pas couramment utilisées ou qui peuvent être ambiguës. 

En suivant ces directives de convention de nommage, nous nous assurerons que notre code est maintenable, compréhensible et favorise une collaboration efficace entre les membres de l'équipe de développement. Une bonne pratique consiste à documenter la convention de nommage utilisée dans le projet, afin que tous les développeurs puissent s'y référer et l'appliquer de manière cohérente.

## 4. Standards de programmation

Les standards de programmation pour une application web utilisant une API .NET sont des règles et des bonnes pratiques qui doivent être suivies lors de la création d'une application web. Ils permettent de garantir la qualité, la sécurité et la stabilité de l'application :  

Conventions de nommage : il s'agit des règles de nommage des variables, des méthodes et des classes. Par exemple, en .NET, les variables et les méthodes sont généralement écrites en camelCase (première lettre en minuscule) tandis que les classes sont écrites en PascalCase (première lettre en majuscule). 

Design patterns : les design patterns sont des modèles de conception qui permettent de structurer le code de manière efficace et modulaire. Ils sont particulièrement utiles pour résoudre des problèmes courants dans le développement de logiciels. 

Error handling : il s'agit de la gestion des erreurs qui peuvent survenir dans l'application. Il est important de gérer ces erreurs de manière appropriée pour éviter que l'application ne s'arrête ou ne produise des résultats inattendus. 

Security : la sécurité est une préoccupation majeure pour les applications web. Il est important de mettre en place des mesures de sécurité adéquates pour protéger les données de l'application et des utilisateurs. 
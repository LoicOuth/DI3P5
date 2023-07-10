---
layout: Part
author: "Ruihau TETAHIO"
meta:
  - name: env
    content: prod
---

# Mise en place de Cert Manager

Cert Manager est un composant Kubernetes permettant de gérer des certificats TLS à l'échelle d'un cluster. Sa mise en place a été nécessaire dans notre projet afin de garantir une disponibilité optimale des services en réduisant les risques :

- D'erreurs humaines lors du renouvellement des certificats TLS pour nos sites
- De fuite de certificats en automatisant le processus de renouvellement et en ne laissant personne interagir avec les certificats

Dans le cadre de notre projet, il a été nécessaire de demander un certificat de type **wildcard**. Pour cela, il est faut que Cert Manager interagisse directement avec le fournisseur DNS pour compléter la chaîne de demande et de renouvellement de certificats wildcard.

Actuellement, nous utilisons OVH pour notre nom de domaine **usite.fr**. Cert Manager ne prenant pas en charge ce fournisseur pour l'heure, nous avons dû installer au sein de notre clustrer un service tier nommé **Cert Manager Webhook OVH** pour permettre à Cert Manager d'avoir un accès complet au DNS OVH.

[[toc]]

## Installation de Cert Manager

**Prérequis** :

- Avoir [Helm 3](https://helm.sh/docs/intro/install/) installé

Cert Manager peut être installé soit directement avec des manifests, soit avec Helm. Dans notre cas, nous allons utiliser Helm.

1. Ajout du repo jetstack :

    ```bash
    helm repo add jetstack https://charts.jetstack.io
    ```

2. Mise à jour des repo Helm :

    ```bash
    helm repo update
    ```

3. Création d'un fichier `cert-manager-values.yaml` afin d'override certaines valeures par défaut de la chart Helm :

    ```bash
    nano cert-manager-values.yaml
    ```

4. Définition des valeures à override :

    ```yaml
    # On défini les serveurs DNS que Cert Manager devra utiliser (au niveau du service directement)
    extraArgs:
      - --dns01-recursive-nameservers=213.186.33.99:53,1.1.1.1:53
      - --dns01-recursive-nameservers-only

    # On défini la configuration DNS par défaut des pods à None
    podDnsPolicy: None

    # On défini les serveurs DNS que le pod devra utiliser (au niveau du pod)
    podDnsConfig:
      nameServers:
        - "213.186.33.99"
        - "1.1.1.1"
    ```

    > **Note**  
    > Le serveur `213.186.33.99` est l'un des serveurs DNS OVH.

    **Explications**

    Cert Manager fonctionnant avec Cert Bot pour renouveler les certificats, **ce dernier va interroger les DNS inscrits dans sa configuration** afin qu'il puisse initier le processus de challenge. Par défaut, Cert Manager prends les serveurs DNS de son pod, qui sont eux-même tirés de la configuration de son hôte. Ainsi, pour accélérer le processus, on défini directement les serveurs DNS s'occupant de notre domaine (ici, `213.186.33.99`).

5. Déploiement de la chart Helm avec les values définies :

    ```bash
    helm install \
    cert-manager jetstack/cert-manager \
    --namespace cert-manager \
    --create-namespace \
    --version v1.12.1
    --values cert-manager-values.yaml
    ```

6. Vérification de l'installation de la chart :

    ```bash
    $ kubectl get all -n cert-manager
    NAME                                           READY   STATUS    RESTARTS   AGE
    pod/cert-manager-76f478df96-pkzwv              1/1     Running   0          46h
    pod/cert-manager-cainjector-7dc99b8847-4wjx4   1/1     Running   0          46h
    pod/cert-manager-webhook-64557dc659-znqqd      1/1     Running   0          46h

    NAME                           TYPE        CLUSTER-IP     EXTERNAL-IP   PORT(S)    AGE
    service/cert-manager           ClusterIP   10.0.220.223   <none>        9402/TCP   2d23h
    service/cert-manager-webhook   ClusterIP   10.0.25.97     <none>        443/TCP    2d23h

    NAME                                      READY   UP-TO-DATE   AVAILABLE   AGE
    deployment.apps/cert-manager              1/1     1            1           2d23h
    deployment.apps/cert-manager-cainjector   1/1     1            1           2d23h
    deployment.apps/cert-manager-webhook      1/1     1            1           2d23h

    NAME                                                 DESIRED   CURRENT   READY   AGE
    replicaset.apps/cert-manager-76f478df96              1         1         1       2d23h
    replicaset.apps/cert-manager-cainjector-7dc99b8847   1         1         1       2d23h
    replicaset.apps/cert-manager-webhook-64557dc659      1         1         1       2d23h
    ```

L'installation de Cert Manager est maintenant terminée.

`🐙 GitOPS`

Tout ce processus a été adapté au `GitOPS` [ici](https://dev.azure.com/U-Site/USite/_git/USite_k8s?path=/core/templates/cert-manager&version=GBmain&_a=contents).

## Installation de Cert Manager Webhook OVH

Puisque nous utilisons un certificat Wildcard pour tout notre cluster, nous devons utiliser l'option `dns01` de Cert Manager. Cependant, OVH n'est pour l'heure pas encore pris en charge par Cert Manager, d'où l'installation du service tiers.

L'installation se fera avec Helm.

> **Warning**  
> Tous le déploiement devra se trouver dans le namespace des applications (usite-prod dans notre cas) car Cert Manager créé les secrets qui contiennent les certificats directement dans ce dernier.

### Déploiement de la chart de base

1. Clonage du dépôt Github :

    ```bash
    git clone https://github.com/baarde/cert-manager-webhook-ovh.git
    ```

2. Création d'un fichier `ovh-values.yaml` afin d'override certaines valeures par défaut de la chart Helm :

    ```bash
    nano ovh-values.yaml
    ```

3. Définition des valeures à override :

    ```yaml
    # Le groupName équivaut au domaine à certifier
    groupName: usite.fr
    ```

4. Déploiement de la chart Helm avec les values définies et le namespace de déploiement :

    ```bash
    helm install cert-manager-webhook-ovh cert-manager-webhook-ovh/deploy/cert-manager-webhook-ovh \
    --namespace usite-prod \
    --values ovh-values.yaml
    ```

5. Vérification de l'installation de la chart :

    ```bash
    $ kubectl get pods -n usite-prod -o wide
    NAME                                                              READY   STATUS    RESTARTS   AGE
    backend-prod-usite-backend-service-c4f7b4f66-48p2b                1/1     Running   0          46h
    cert-manager-webhook-ovh-9755564bf-272c9                          1/1     Running   0          46h
    ```

La chart Helm est maintenant installée.

### Création du token OVH

Afin de modifier la zone DNS et délivrer des certificats TLS, le webhook OVH a besoin d'un token d'authentification avec les droits nécessaires.

1. Naviguer sur [le lien de création de tokens OVH](https://eu.api.ovh.com/createToken/).
2. Connection avec le compte OVH sur lequel le DNS est enregistré.
3. Renseignez un nom d'application (1), une description (2) et définir la validité souhaitée (3)(Unlimited dans notre cas).
4. Renseignez les droits `GET`,`PUT`,`POST`,`DELETE` pour `/domain` et `/domain/*` (4).

    > **Note**  
    > Pour restreindre l'accès du token à un unique domaine, remplacer `/domain` et `/domain/*` par `/domain/zone/usite.fr` et `/domain/zone/usite.fr/*`.

5. Spécifiez l'adresse IP autorisée à requêter avec ce token (5).
6. Une fois toutes ces informations renseignées, cliquez sur `Create` :  
    ![Création du token](/assets/images/pfsense/07.png)
7. Une fois le token créé, notez toutes les informations :  
    ![Création du token](/assets/images/pfsense/08.png)
    > **Warning**  
    > Une fois que vous fermez la fenêtre, il sera impossible de récupérer les identifiants plus tard

### Ajout des éléments de configuration nécessaires

Maintenant que le token est créé, nous allons maintenant pouvoir finaliser la configuration du webhook OVH. Pour cela, il faudra créer mes éléments de configuration suivants :

- Secret contenant l'application key
- Service account en read only sur ce secret
- Issuer (tiers délivrant les certificats)
- Certificate qui va générer le certificat et le mettre dans un secret accessible par les applications du namespace

#### Création du secret

Le secret est nécessaire pour le webhook OVH. Exécuter la commande suivante pour créer le secret :

```bash
kubectl create secret generic ovh-credentials \
--namespace usite-prod \
--from-literal=applicationSecret='<appSecret>'
```

> **Note**  
> On créé le secret nomme `ovh-credentials` dans le namespace `usite-prod` qui contient la valeur `applicationSecret='<appSecret>'`.

#### Création du service-account

Nous allons créer un service-account qui sera le seul à pouvoir lire le secret que l'on vient de créer.

1. Création du fichier `service-account.yaml`

    ```bash
    nano service-account.yaml
    ```

2. Ajout du contenu suivant :

    ```yaml
    apiVersion: rbac.authorization.k8s.io/v1
    kind: Role
    metadata:
      name: cert-manager-webhook-ovh:secret-reader
    rules:
    - apiGroups: [""]
      resources: ["secrets"]
      resourceNames: ["ovh-credentials"]
      verbs: ["get", "watch"]
    ---
    apiVersion: rbac.authorization.k8s.io/v1
    kind: RoleBinding
    metadata:
      name: cert-manager-webhook-ovh:secret-reader
    roleRef:
      apiGroup: rbac.authorization.k8s.io
      kind: Role
      name: cert-manager-webhook-ovh:secret-reader
    subjects:
    - apiGroup: ""
      kind: ServiceAccount
      name: cert-manager-webhook-ovh
    ```

3. Application du fichier :

    ```bash
    kubectl apply -n usite-prod -f service-account.yaml
    ```

Le service-account est maintenant prêt à être utilisé.

#### Création de l'issuer

L'issuer est le tier qui fournis les certificats pour notre cluster. Il s'agira dans notre cas de Let's Encrypt.

1. Création du fichier `issuer.yaml` :

    ```bash
    nano issuer.yaml
    ```

2. Ajout du contenu suivant :

    ```yaml
    apiVersion: cert-manager.io/v1
    kind: Issuer
    metadata:
      namespace: usite-prod
      name: letsencrypt-ovh
    spec:
      acme:
        server: https://acme-v02.api.letsencrypt.org/directory
        email: 'contact-usite@diiage.org'
        privateKeySecretRef:
          name: letsencrypt-account-key
        solvers:
        - dns01:
            webhook:
              groupName: 'usite.fr'
              solverName: ovh
              config:
                endpoint: ovh-eu
                applicationKey: '<ovhApplicationKey>'
                applicationSecretRef:
                  key: applicationSecret
                  name: ovh-credentials
                consumerKey: '<ovhConsumerKey>'
    ```

    > **Note**  
    > Afin de tester le service, il est fortement conseillé de définir la valeur `spec.acme.server` à `https://acme-staging-v02.api.letsencrypt.org/directory` jusqu'à ce que les certificats se créent correctement. En effet, Let's Encrypt fournis une API de tests (staging) et une api de production. Des limites sont appliquées sur celle de prod, et si cette limite est atteinte, **les demandes de certificats sont bloquées pour une semaine**.

3. Application du fichier :

    ```bash
    kubectl apply -n usite-prod -f issuer.yaml
    ```

L'issuer est maintenant créé. Tout est prêt pour que l'on initie notre première demande de certificat.

#### Création de la demande de certificat

Comme toutes les ressources Kubernetes, les certificats sont créés avec des fichiers `yaml`. Il faut donc créer et appliquer un fichier `certificate.yaml` (toujours dans le namespace usite-prod).

1. Création du fichier `certificate.yaml` :

    ```bash
    nano issuer.yaml
    ```

2. Ajout du contenu suivant :

    ```yaml
    apiVersion: cert-manager.io/v1
    kind: Certificate
    metadata:
      namespace: usite-prod
      name: usite-wildcard-tls-certificate
    spec:
      dnsNames:
      - "usite.fr"
      - "*.usite.fr"
      issuerRef:
        name: letsencrypt-ovh
      secretName: usite-wildcard-tls-certificate
    ```

3. Application du fichier :

    ```bash
    kubectl apply -n usite-prod -f issuer.yaml
    ```

La demande de certificat pour usite.fr est d_s lors initiée.

Pour suivre l'avancée de cette demande de certificat, on peut vérifier les logs du pod cert-manager :

```bash
$ kubectl get pods -n cert-manager
NAME                                       READY   STATUS    RESTARTS   AGE
cert-manager-76f478df96-pkzwv              1/1     Running   0          47h
cert-manager-cainjector-7dc99b8847-4wjx4   1/1     Running   0          47h
cert-manager-webhook-64557dc659-znqqd      1/1     Running   0          47h
```

Attendre ensuite jusqu'à ce que cette ligne apparaisse

```bash
$ kubectl logs -n cert-manager cert-manager-76f478df96-pkzwv -f

I0616 13:56:48.202760       1 conditions.go:192] Found status change for Certificate "usite-wildcard-tls-certificate" condition "Ready": "False" -> "True"; setting lastTransitionTime to 2023-06-16 13:56:48.10298595 +" -> "True"; setting lastTransitionTime to 2023-06-16 13:56:48.202755363 +0000 UTC m=+62367.068767010   
```

Cette ligne notifie que le certificat a bien été déliveré.

#### Utilisation du certificat dans les applications

Le certificat généré se trouve dans le secret défini dans la demande de certificat (champs `spec.secretName`). Dans notre cas, il s'agit de `usite-wildcard-tls-certificate`.

Pour utiliser le certificat, il suffit simplement de faire référence à ce secret dans l'ingress de l'application. Pour nos applications, il suffit de configurer l'ingress dans le fichier `values.yaml` du déploiement comme suit (exemple pour le backend) :

```yaml
ingress:
  className: nginx
  enabled: true
  annotations:
    nginx.ingress.kubernetes.io/rewrite-target: "/"
  hosts:
    - host: api.usite.fr
      paths: 
      - path: "/"
        pathType: Prefix
  tls:
    - hosts: ['usite.fr', '*.usite.fr']
      secretName: usite-wildcard-tls-certificate
```

Il suffit maintenant de redéployer l'application pour que l'ingress prenne le certificat en compte dans sa configucation.

`🐙 GitOPS`

Tout ce processus a été adapté au `GitOPS` [ici](https://dev.azure.com/U-Site/USite/_git/USite_k8s?path=/apps/templates/cert-manager-webhook-ovh&version=GBmain&_a=contents).

## Références

- [Github - Baarde - Cert Manager Webhook OVH](https://github.com/baarde/cert-manager-webhook-ovh)
- [Helm - Installing Helm](https://helm.sh/docs/intro/install/)
- [Cert Manager - Installation with Helm](https://cert-manager.io/docs/installation/helm/)
- [Devpress - Use OVH as a DNS-01 provider for cert-manager](https://devpress.csdn.net/k8s/62ebf31d19c509286f415e47.html)

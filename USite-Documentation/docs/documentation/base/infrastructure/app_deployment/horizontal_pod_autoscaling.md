---
layout: Part
author: "Ruihau TETAHIO"
meta:
  - name: env
    content: dev,test,prod
---

# Horizontal Pod Autoscaling

Afin de supporter la charge et permettre aux différents déploiements d'être élastiques, de l'Horizontal Pod Autoscalind a été mis en place dans le projet.

## Définition

L'Horizontal Pod Autoscaling est un moyen de rendre les déploiements applicatifs élastiques en rajoutant ou en supprimant des pods du déploiement. Il met à l'échelle ces derniers selon plusieurs critères :

- Le % de ressources utilisé par les pods d'un déploiement
- Le nombre de répliques minimum
- Le nombre de répliques maximum

## Mise en place

### Prérequis

- Avoir installé `metrics-server` sur le cluster Kubernetes

    **metrics-server** est le composant permettant de monitorer les ressources utilisées par les pods d'un cluster. Sans ce composant, le HPA ne peut pas fonctionner.

### Activation du HPA

Dans notre projet, la mise en place des HPA se fait automatiquement grâce aux templates Helm utilisés. Lors de la génération des templates par les développeurs, un fichier `hpa.yaml` est automatiquement créé dans le dossier `📁 templates` de la chart.

Il suffit de rajouter les valeurs suivantes dans le fichier `values.yaml` :

```yaml
autoscaling:
  enabled: true
  minReplicas: 1
  maxReplicas: 4
  targetCPUUtilizationPercentage: 80
  targetMemoryUtilizationPercentage: 80
```

Les valeurs sont assez explicites et n'ont à mon sens pas besoin d'être expliquées. Cependant, pour que les HPA jouent leurs rôles, il faut qu'ils aient une valeure sur laquelle se baser. Actuellement, aucune limite de consommation n'est donnée aux conteneurs du déploiement. Pour cela, nous avons besoin de spécifier les ressources à alouer : les **requests** et les **limits**.

Encore une fois, les charts Helm étant bien faites, il suffit de rajouter les valeurs suivantes dans le fichier `values.yaml` pour activer les requests et les limites :

```yaml
resources:
  requests:
    memory: 256Mi
    cpu: 150m
  limits:
    memory: 512Mi
    cpu: 500m
```

Une fois ces modifications faites, le HPA est prêt à remplir son rôle. On peut vérifier cela avec la commande `kubectl get hpa -A` :

```bash
$ kubectl get hpa -A
NAMESPACE    NAME                                                       REFERENCE                                                             TARGETS           MINPODS   MAXPODS   REPLICAS   AGE
usite-prod   backend-prod-usite-backend-service                         Deployment/backend-prod-usite-backend-service                         45%/80%, 2%/80%   1         4         1          77m
```

## Bugs connus

### Le HPA retourne des unknown

**Symptômes** : Lorsque la commande `kubectl get hpa -A`, le HPA me retourne des valeurs **unknown** :

```bash
$ kubectl get hpa -A
NAMESPACE    NAME                                                       REFERENCE                                                             TARGETS           MINPODS   MAXPODS   REPLICAS   AGE
usite-prod   frontend-presentation-prod-frontend-presentation-service   Deployment/frontend-presentation-prod-frontend-presentation-service   45%/80%, 6%/80%   1         4         2          77m
```

**Causes et solutions** :

1. Les ressources **requests** et **limits** n'ont pas été définies

    Commencer par vérifier les logs.

    Vérifier le déploiement.  Dans la chart Helm, vérifier que la ligne suivante est bien renseignée :

    ```yaml
            resources:
                {{- toYaml .Values.resources | nindent 12 }}
    ```

    Vérifier ensuite que les valeurs sont bien renseignées dans le fichier **values.yaml** (le fichier par défaut ou le fichier d'override) :

    ```yaml
    resources:
      requests:
        memory: 256Mi
        cpu: 150m
      limits:
        memory: 512Mi
        cpu: 500m
    ```

    Vérifier que l'indentation est bien respéctée (pas de tab) car le langage YAML est très sensible à cela.

2. Les logs de mon HPA me retournent une erreur **unable to fetch metrics from resource metrics API: the server is currently unable to handle the request (get pods.metrics.k8s.io)**

    Ce message d'erreur veut dire que soit le pod metrics-server n'est pas encore prêt, soit qu'il y a un problème avec le déploiement.

    Dans le premier cas de figure, attendre quelques minutes. Les metrics devraient remonter.

    Si le problème n'est toujours pas résolu, redéployer le déploiement metrics-server.

## Références

- [Kubernetes Documentation - Horizontal Pod Autoscaling](https://kubernetes.io/docs/tasks/run-application/horizontal-pod-autoscale/)
- [Kubernetes Documentation - HorizontalPodAutoscaler Walkthrough](https://kubernetes.io/docs/tasks/run-application/horizontal-pod-autoscale-walkthrough/)
- [Github - kubernetes-sigs/metrics-server](https://github.com/kubernetes-sigs/metrics-server)

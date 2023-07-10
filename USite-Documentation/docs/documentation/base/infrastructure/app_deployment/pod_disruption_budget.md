---
layout: Part
author: "Ruihau TETAHIO"
meta:
  - name: env
    content: dev,test,prod
---

# Pod Disruption Budget

Afin de garantir une disponibilité optimale de nos applications lors de maintenances prévues, des Pod Disruption Budgets ont été configurés.

## Définition

Un Pod Disruption Budget permet de limiter l'interruption de services lorsqu'un pod doit être reschédulé. Ce dernier assure qu'un minimum de pod tourne lors de diverses maintenances prévues, comme des upgrades de cluster ou des upgrades de worker.

## Cas d'usage

Comme mentionné, les PDB sont utiles uniquement lors de "disruptions" prévues, comme des upgrades. Les PDB servent alors uniquement dans le processus de reschedule des pods. Par nature, tous les évènements imprévus (coupure de courant, panne matériel) ne sont pas pris en charge.

Pour cela, on effectue les commandes suivantes :

1. Drain du noeud concerné par la maintenance :

    ```bash
    kubectl drain --ignore-daemonsets <node_name>
    ```

    Cette commande décomissionne tous les pods d'un node et les reschedule sur un autre worker. Une fois cette commande effectue sans erreur, la maintenance peut commencer.

    > **Note**  
    > Si des pods dépendent d'un DaemonSet, il sera nécessaire de rajouter l'option `--ignore-daemonsets`.

2. Une fois la maintenance terminée, uncordon du noeud concerné :

    ```bash
    kubectl uncordon <node_name>
    ```

    Cette commande notifie le cluster de la disponibilité du worker. Kubernetes peut dès lors reschéduler des pods.

## Application du Pod Disruption Budget dans nos applications

Pour que nos applications incluent du PDB, nous avons dû rajouter le manifest `pdb.yaml` dans le dossier `Deploy/<app_name>/templates`. Voici le contenu du manifest (exemple pris de l'application frontend-cms-service):

```yaml
{{- if .Values.PodDisruptionBudget.enabled }}
apiVersion: policy/v1
kind: PodDisruptionBudget
metadata:
  name: {{ include "frontend-cms-service.fullname" . }}
  labels:
    {{- include "frontend-cms-service.labels" . | nindent 4 }}
spec:
  minAvailable: {{ .Values.pdb.minAvailable }}
  selector:
    matchLabels:
      {{- include "frontend-cms-service.selectorLabels" . | nindent 6 }}
{{- end }}
```

Les valeures ajoutées dans le fichier `values.yaml` sont les suivantes :

```yaml
PodDisruptionBudget:
  enabled: true
  minAvailable: 1
```

## Références

- [Kubernetes Documentation - Horizontal Pod Autoscaling](https://kubernetes.io/docs/tasks/run-application/horizontal-pod-autoscale/)
- [Kubernetes Documentation - HorizontalPodAutoscaler Walkthrough](https://kubernetes.io/docs/tasks/run-application/horizontal-pod-autoscale-walkthrough/)
- [Github - kubernetes-sigs/metrics-server](https://github.com/kubernetes-sigs/metrics-server)

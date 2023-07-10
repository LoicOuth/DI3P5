---
layout: Part
author: "Ruihau TETAHIO"
meta:
  - name: env
    content: dev,test,prod
---

# Metrics-server

Metrics-server est un service à part de Kubernetes qui permet de monitorer l'utilisation des ressources par les pods d'un cluster.

Metrics-server est un prérequis pour des services tel que les [Horizontal Pod Autoscaling (HPA)](horizontal_pod_autoscaling.md).

## Mise en place

Dans le cadre de notre projet, le déploiement est géré entièrement par ArgoCD avec un process GitOPS. Le dossier `📁 metrics-server` contenant les éléments suivants ont été ajoutés dans le dossier `📁 core/templates` :

```txt
📁 core/
├── 📁 templates/
│ ├── 📁 metrics-server/
│ │ ├── _values.yaml
│ │ ├── application.yaml
```

### Contenu du fichier _values.yaml

```yaml
{{ define "prod.core.metrics-server.values" }}

defaultArgs:
  - --cert-dir=/tmp
  - --kubelet-preferred-address-types=InternalIP,ExternalIP,Hostname
  - --kubelet-use-node-status-port
  - --metric-resolution=15s
  - --kubelet-insecure-tls

{{ end }}
```

Le but est d'overrider le paramètre `defaultArgs` pour ajouter spécifiquement la propriété `- --kubelet-insecure-tls`.

Cet argument est nécessaire dans les environnements de dev/test car sans ce dernier, la communication avec les nodes n'est pas possible.

> **Note**  
> Cet override n'est pas implémenté en production (pas besoin).

### Contenu du fichier application.yaml

```yaml
apiVersion: argoproj.io/v1alpha1
kind: Application
metadata:
  name: metrics-server
  namespace: argocd
  finalizers: []
spec:
  syncPolicy:
    automated:
      prune: true
      selfHeal: true
    syncOptions:
      - CreateNamespace=true
      - ApplyOutOfSyncOnly=true
      - PruneLast=true
  destination:
    namespace: metrics-server
    server: {{ .Values.spec.destination.server }}
  project: core
  source:
    chart: metrics-server
    repoURL: https://kubernetes-sigs.github.io/metrics-server
    targetRevision: 3.10.0
    helm:
      values: {{ include "dev.core.metrics-server.values" . | toYaml | nindent 8 }}
```

Ce YAML est un fichier de déploiement d'application type ArgoCD. Il se base sur la chart Helm officielle fournie par Kubernetes.

## Références

- [Github - kubernetes-sigs/metrics-server](https://github.com/kubernetes-sigs/metrics-server)

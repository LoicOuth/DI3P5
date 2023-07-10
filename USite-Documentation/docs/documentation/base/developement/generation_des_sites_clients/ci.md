---
layout: Part
author: "Loïc OUTHIER"
---

# Génération des sites

[[toc]]

## Introduction

Nous utilisons les classes CSS de [TailwindCSS](https://tailwindcss.com/) pour le style des différents sites. Pour la génération du site, une fois qu'il est ajouter dans un repo GIT la CI devra comporter les étapes suivantes :

## Etapes pour la génération du CSS

Dans un premier temps installer [node.js](https://nodejs.org/en) sur la machine.

1. Initier le projet node

```bash
npm init
```

2. Installer tailwind

```bash
npm install tailwindcss
```

3. Créer le fichier de configuration tailwind

```bash
npx tailwindcss init
```

4. Modifier la configuration pour prendre en compte les fichiers HTML

```js
module.exports = {
  content: [
    "*.html"
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}
```

5. Créer le fichier css

```bash
touch style.css
```

6. Ajoutez l'importation de Tailwind dans le fichier CSS :

```css
@import 'tailwindcss/base';
@import 'tailwindcss/components';
@import 'tailwindcss/utilities';
```

7. Executer le build du fichier CSS finale :

```bash
tailwindcss build styles.css -o dist/style.min.css --minify
```

8. Mettre les fichiers HTML dans le dossier 📁`dist`

```bash
mv *.html dist/
```

Voici le YAML correspondan à la CI de chaque client résultant des commandes ci dessus :

```yml
stages:
  - stage: build_project
    pool:
      vmImage: ubuntu-latest
    jobs:
      - job: build
        steps:
          - task: NodeTool@0
            inputs:
              versionSpec: 14.x
            displayName: Install Node.js
          - script: |
              npm init -y
              npm install tailwindcss
              npx tailwindcss init
            displayName: Initialize npm and Tailwind CSS
          - script: >
              sed -i "s|  content: \\[],|  content: [ \"*.html\" ],|g"
              tailwind.config.js
            displayName: Update tailwind.config.js
          - script: |
              echo "@import 'tailwindcss/base';" > styles.css
              echo "@import 'tailwindcss/components';" >> styles.css
              echo "@import 'tailwindcss/utilities';" >> styles.css
            displayName: Create styles.css
          - script: >
              npx tailwindcss build -i styles.css -o
              IdDuSIte/style.min.css --minify
            displayName: Build and minify CSS
          - script: |
              mv *.html IdDuSIte/
            displayName: Move HTML files to IdDuSIte/
          - task: PublishPipelineArtifact@1
            inputs:
              targetPath: >-
                $(System.DefaultWorkingDirectory)/IdDuSIte
              artifact: IdDuSIte
              publishLocation: pipeline
            displayName: Publish artifact
```


## Etapes pour le déploiement sur les environnements de dev et de test

```yml

- stage: deploy_project
    pool: diiage-hosted
    jobs:
      - job: deploy
        steps:
          - task: DownloadPipelineArtifact@2
            inputs:
              artifact: IdDuSIte
              path: >-
                $(System.DefaultWorkingDirectory)/IdDuSIte
          - task: CmdLine@2
            displayName: Changing  the configuration file's encoding
            inputs:
              script: |
                #/bin/sed -i '1s/^\xEF\xBB\xBF//' $(System.DefaultWorkingDirectory)/IdDuSIte.conf && echo "Encoding changed"
                # vi -c ":set nobomb" -c ":wq" $(System.DefaultWorkingDirectory)/IdDuSIte.conf
                # mv $(System.DefaultWorkingDirectory)/IdDuSIte.conf $(System.DefaultWorkingDirectory)/IdDuSIte-bom.conf
                # tail -c +4 $(System.DefaultWorkingDirectory)/IdDuSIte-bom.conf > $(System.DefaultWorkingDirectory)/IdDuSIte.conf
                dos2unix $(System.DefaultWorkingDirectory)/IdDuSIte.conf
          - task: CmdLine@2
            displayName: Sending the website files and nginx conf to the remote PVCs
            inputs:
              script: |
                # /bin/sed -i '1s/^\xEF\xBB\xBF//' $(System.DefaultWorkingDirectory)/IdDuSIte.conf && echo "Encoding changed"
                for pod in `kubectl get pods -n usite-$(currentEnvironment) -o=name | grep 'usite-websites' | sed "s/^.\{4\}//"`
                do
                    kubectl cp $(System.DefaultWorkingDirectory)/IdDuSIte/ usite-$(currentEnvironment)/$pod:/var/www/clients/
                    dos2unix $(System.DefaultWorkingDirectory)/IdDuSIte.conf && kubectl cp $(System.DefaultWorkingDirectory)/IdDuSIte.conf usite-$(currentEnvironment)/$pod:/etc/nginx/conf.d/
                    break
                done
          - task: CmdLine@2
            displayName: Applying the client ingress
            inputs:
              script: |
                kubectl apply -f $(System.DefaultWorkingDirectory)/ingress.yaml -n usite-$(currentEnvironment)
          - task: CmdLine@2
            displayName: Restarting the nginx pods through a deployment rollout
            inputs:
              script: |
                for pod in `kubectl get pods -n usite-$(currentEnvironment) -o=name | grep 'usite-websites' | sed "s/^.\{4\}//"`
                do
                    kubectl delete -n usite-$(currentEnvironment) pod $pod
                done
```
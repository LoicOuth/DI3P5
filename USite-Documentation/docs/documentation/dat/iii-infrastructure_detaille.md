---
layout: Part
author: "L'équipe uSite"
---

# III. Infrastructure détaillés

[[toc]]

## III.a. Serveurs et ressources déployées

### III.a.1 Environnement de Dev/Test

Pour des soucis de coûts et d'accessibilité, les environnements de Dev et de Test ont été mutualisés sur les mêmes serveurs. L'environnement de Dev/Test se compose des **éléments** suivants :

| Element | Valeur | Remarques |
|---|---|---|
| 2x serveurs Debian 11 faisant office de Masters Kubernetes | 4Go de RAM, 1 CPU avec 2 vCore, 80Go d'espace disque |  |
| 2x serveurs Debian 11 faisant office de Workers Kubernetes | 6Go de RAM, 1 CPU avec 2 vCore, 80Go d'espace disque |  |
| 1x pare-feu pfSense 2.6.0 | 1Go de RAM, 1 vCPU avec 1 vCore, 10Go d'espace disque | Permet l'accès sécurisé à l'environnement via VPN, filtre du trafic web |
| 1x serveur Windows Server 2019 Standard | 4Go de RAM, 1 vCPU avec 2 vCore, 60Go d'espace disque | Nécessaire pour la génération de templates T4 (backend) |
| 1x serveur Ubuntu 20.04 LTS | 4Go de RAM, 1 vCPU avec 2 vCore, 32Go d'espace disque | Hébergement du SGBDR MSSQL Server 2019 Essentials |
| 1x serveur Debian 11 Zabbix Server | 2Go de RAM, 1 vCPU avec 2 vCore, 48Go d'espace disque | Hébergement du service de monitoring on-premises Zabbix |
| 1x serveur Debian 11 AzAgent | 512Mo de RAM, 1 vCPU avec 1 vCore, 16Go d'espace disque | Permet le déploiement de sites sur nos environnements de Dev/Test |

### III.a.2 Environnement de Prod

L'environnement de Prod se compose des **éléments** suivants :

| Element | Valeur | Remarques |
|---|---|---|
| 1x cluster AKS composé de 2 workers (vm type b2s) | 2Go de RAM, 1 CPU avec 1 vCore |  |
| 1x base de données Azure SQL Server |  |  |
| 1x Azure Container Registry |  | Hébergement des images personnalisées |
| 1x Log Analytics Workspace |  | Monitoring des services Azure |
| 1x Storage Account |  | Hébergement des images des sites web clients (jpg, png...) |

## III.b. Schéma d'infrastructure complet

<iframe frameborder="0" style="width:100%;height:800px;" src="https://viewer.diagrams.net/?tags=%7B%7D&highlight=E71D36&edit=_blank&layers=1&nav=1&title=USite%20-%20Schemas.drawio#R7V1tc5u4Fv41ntt%2BMCMQrx8TJ%2Bnu3PRudtO9237KyCDbNBhRAYnTX78SSOZF4LixcdzG7UwLx0KA9JyjR%2BcciRGcLFcfKEoWH0mAo5EBgtUIXowMw7Kgw%2F7jkqdSotuuV0rmNAyErBLcht%2BxEAIhzcMAp42CGSFRFiZNoU%2FiGPtZQ4YoJY%2FNYjMSNe%2BaoDlWBLc%2BilTpP2GQLUqpaziV%2FDcczhfyzrot3m%2BJZGHxJukCBeSxJoKXIzihhGTl0XI1wRFvPdkuXz7Mz%2F78uIrG3vfH4BrZ%2Blf9YlxWdvUjl6xfgeI4e3HV1uU0QJ%2Fgn%2Bnfv9Hkr29%2FeBd%2BNtaheNs0e5IthgPWgOKU0GxB5iRG0WUlPackjwPM6wXsrCpzTUjChDoTfsVZ9iTQgPKMMNEiW0biV7wKs8%2B14y%2B8Ks0SZxcrUXNx8iRO0oyS%2B3UfGvznODjjCGGnMYlxKbkKo0heHmf06XP9pHYfflrdqDiTdypbhDdDC0LPtL8ol5Kc%2Bnhjo1tCERCd42xTSROugcZUFJMlZg%2FKrqQ4Qln40HxAJFRlvi63vvSGhOzRDSD0eqw7nkC11Guge81ayocTF1a4Yge1J6lEBdp%2BBHmm%2FZrIA4Mib3AEDYYL12zjQpq%2FQ%2BECiqd%2BQFEu3mNk2BFrtvMpO5hnRfuWghkpnt4nEaFFUftbTsoCEEIX%2BkFdVF579j2nWFbAHrGso1kvE9fu1UJpDU3kAdNZVOBgGhH%2Fnj8j%2F%2F82QX4Yz0Wpx0WYYS7iFTyycVY8uQCpbmuOkEzEiwR4hvLiSVAUzmMmifCMn6ZVxRxn7PZZyMa6M1Eq41og8DpZN4psCvnLH7yS7KmoBDSUij8tmqYkyjN8Rn35gFy6PvMUjYBMEuV%2BGPxe1BHfjL%2FFf5OvC8ja5ZyN6xkKY0xFRayvIpSk4bRoTn77eYTSVBzPmA6pjSDe%2BlOh46ASXBeNUtM43h549QKdUzVJaoQB2hrhCerwWNEJRyrJokYlbNPo17%2BG2vy4jsAOHWnBlNGVhB%2BGy4IhrbFyjaY4uiFpmIWEY2ZKsowseRfyH86Rfz8vAKF2QzfaUJqUvG0WrjiIzosbnkkpkBJ2vMgyzvrO%2BNsbV3kSERRoj%2BF9uMRBiDRC50zMzxN%2BznWTlpp4NWWDxdXH0KckJbNsXOiwlnBF2K7nN5ma3r637VbPG0rHmx39boKBut0xnreMF%2FiB1f0Jp9wuviPxOKF4GaY4ff%2BL2rbzC%2F53MNtm%2FNK2zQAthOuep2C8ogB1lLPm3UAvdsK5YSo4%2F2s0gaMzL8UoZz%2Fktwx0HN%2B6Y2gMYkBjD3il2%2B9PeH7beNattsWGKp69Ljh7bZq7NzS7dq%2FVFvS1wqvkqvyHcVr0IRssgW4mq5KdtrisYv4TSlZLsho%2F4G5zL4pTKbnF9AHn7BTcLJ7S8FuO1TK9XLmla88SjPQeZ%2F5CgEChDTVV3UZVxLxL6pzPgMNxe07yLGIQnqzdSQUGBRdarubczaUxlaLhSsvII6Z3KW8DuhOTKC1WLzBNYGqW00SmnObUDa0ONNftMLWmNRSRBOpk6zZjFJCJ%2FptPMY1xhtNfwKqatu1j%2BEvPgAKULtavMBiULTbW6g0kQ1dFsttlYg1nKMLgOs9Ph%2FikJultFuF9RlNZHGw7hwCbxyTd8DTHbDSY9LzV2suygWbBLpJlDjWX8A44Kt276fiR0HtMeWttMy59RP6CqQy7y%2F9DmuXshU4D00u0udSMXnRCQ9VdxzswFN1%2Bh9%2F%2Bobi3mjmmewAooG5sROz0BPWDQt3sGKYOD3V1Zjus1V2iNDtZ3WOB4qtCz3od6Bkn6B0D9GQ1tko%2BDw5FXTrSN%2FF1GbbYuqsSHpEsHtU6H1kXXT1Sn3JyR5iMS4RLHniIQh5pWKY%2BYtC7qqa%2FWvqwY5zhmTmCY3qa9LtK94AaY7K6Qg3uUF0kHcOvMKV6ZgZqWMqMyumgsSY49JTKO6SjD31n0I2zbUf268%2BTkzl9keqCjWA8igmU5x4QeN%2FRdBquxqJtT%2BB7DfC9KtgO6Thapum36IS1IzB0R0Ecna45zE%2Ffh7OQ4kfEW2K4yKjuQFOz7CbH1C1ds9X4qA48TTL0Rr9CS4NDufAdR%2BnJQ2aDrjNAv9R%2B6c4G5Sc3mIbsvYuQTpk%2FvDkfVI3Bl287XIaoI%2Fr62RTjUqd2yCTdMZPNPJ5%2BBxqAP9z1x5ccXHb9ALnBNjf4juuZDrRd0wBuw5y47by3gROFna7pVgtKm2194THA9PIBl44DvbTR5SKPRkgVNOGjhJcBsC%2FP2PuVOZS1UWc9IDw7LPUkVyoujFp4mN1YnyIdGx1jS%2BHG0IqrEhqmWCteNeamoAVZMGpG3lmts%2BLPsOORY2tOE0DrFJz6WORBzbI6xqLB0nWcASdTyewWx%2BkGx2aaoLj%2FDo%2BiAfg9YkKXKOq8ywZXKmuR8hZ99HlbpszwkG3SCWH46mjto0pt4C%2FDICisfFfSSNPyF%2F488VD6cFi1TagZsMWdYAdYrapcHaxwKKwyBtcL1vUc6YaSgBV5N4lIHpyygEenrMk2vj2PjesNeBtyplVDty1TKevQtgZjhpYaKj3PGWfK1HS07TB6wuN%2B8dhgSgMSBasNTqhbmgrPtYOikXIGhjO9XTyhJ4Z1gLAV4iti%2BDstQ0YAeXb81YSk2d1HFLPCS1bVHYqDu3PW26wbdw5pWZtjNLDpaXDUsbIrz9UdLJqlq3MFsQywkeZqc0bN4czTokM2mJ0szZu2NJZ1FJZmi%2BzW17A0PlkmedaMld8JzdlD0FzvcWOsc%2Bo12IwCu50eathByJ3h%2BkqNjkhDM1krS2Vn%2FsLzMJU3PhmaN2JorCOlNPIFj8%2FQiN5PC1YjTu6E8oR7MTbeZj5jaq7pVX%2BaKyAcr6PzPKAZHRMmdzBPuqGuCZem5%2FbP65rRuUAZmqL0xG7euNE5DnYjbcwx5wIy%2FbmTWnM3xwwSob%2B7zTF69nCRBMdrExzH1Gw1L9DWtQ6K4wLNtgfrM9XteE1Ys4GzGEVPTK%2Frc6l%2FCL1PC9fgyd68IXvj8SVtx0hyjnQ2hSrVuWLKdLdWpbu1Au2B5xg90X5pc4xWn7ldXeZ0BOScwZYoG6qjTfKajyQOM6YBJ8Pypg2LZb2%2BWYFb5Mu%2Fiju48v%2FOuVrEKOYj8ZVQnT1YFHezRbHaFsXWXKM2lXJVQtMxbZKy%2FXeczFo5igSk0cbsoxduZ6luVaBYJ%2BzakJFNZhpy%2BrA2Qx07ChzFpph9kDtQypqp%2BvjE9kR%2BnpQHFBXpPQYIirBCK8liWRbKaDHad4CvSBJqAmb7tAmK0%2FA7qmy7alF6tf3luyCYm7OHx9wZApspFGPR3zumpTWti94y8GQ2S%2FFA%2B5eqBv5IM1a3MwDuT2MAvF31v7j0jFL0VCsg9KQXaOMq6UxmSQCRJl1Bq6x0v0CzVGqxrb3pyCSbEhpgOhZbpp4V9dJ343Fd%2Fn7UlVf2K5otY6PZApoLrKYPxjZ3s1oHMExQ9c58oGjGiCATFllgpwnTG5owKZs8dubZHnq6tEX2zF6yENu74NYxDBvgBAoKRcZqNwDVJOruXW4LaaoVWNNQkqxF7ABbMwM4AR4HPvvHNAM4njpTd2x5FoSBbwYO51VF%2FKuAQMfWuhWGwfnjl2g6%2FxR%2FvSS7TengZicRhJrtgeqP1aRgHck%2BnqHZhoovb7ANBuS6%2BJ%2BRmzVIV4OOqft6644dFNn%2FjDNlctGJz21T6EuxWHlyEApnwm0p3F7WqPwwhdMN83UonBqbnZDlMo%2FZQCiYG46Ldity7lvzxW%2F5f4rF%2FjzAUno%2Bfw3ytdlzBDTHA60po7QiO84ZWxcMOUnsD8pf4Ic%2Fbm5PVOwNUTEdGsdHxcyuNSQ9nuvXCsAXOnPHNSbZQxCs7%2BMpVWZha4vTDkoj9%2BJvxMCMwfqoK2j5%2FCdQZhb%2FWxftc3OG3%2BWKwg0bM%2Fwc68iGU3gHtLNuvI7tX7s0frhFY5audMNPQ45f4ris4iZVqGQdbfk5PgPWt4HYrku6%2BZbjDXRW6xplLeVrKKu4uz4d5ba5tdEyiD0rwnf3xOqWBZ55Orv9wbPWJUMx%2F95vWZyctzvOH6zNxhdoht1KoPN2mz3sdb7QaTocR335fX1FsmPH7%2F%2BR%2FKH8ogrr1OLbD%2BDdhCRP71%2FyFOyg%2BhZm2SDVJ0Xh5b8%3D"></iframe>

### III.b. Déploiement automatisé de l'environement de Prod

Le processus de déploiement de notre infrastructure sur Azure se fait de manière simplifiée en utilisant des fichiers Bicep. Tous les éléments de configuration et de sécurisation sont donc renseignés sous forme déclarative.

Tous les éléments décrits dans la section [III.a.2](#iiia2-environnement-de-prod) sont renseignés dans ces fichiers Bicep.

Lien vers le dépôt : [USite_bicep](https://U-Site@dev.azure.com/U-Site/USite/_git/USite_bicep)

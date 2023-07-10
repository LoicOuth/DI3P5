module.exports = {
   title: 'USite base de connaissance',
   head: [
      ["link", { rel: "icon", href: "/assets/images/usite-logo-only-red.svg" }],
   ],

   plugins: ['vuepress-plugin-reading-time',
      ['@vuepress/last-updated',
         {
            transformer: (timestamp) => {
               return new Date(timestamp).toLocaleString('fr-FR')
            }
         }
      ],
   ],

   themeConfig: {
      lastUpdated: "Dernière mise à jour",
      smoothScroll: true,
      searchPlaceholder: 'Rechercher...',
      logo: '/assets/images/usite-logo-only-red.svg',

      nav: [
         { text: 'RGPD', link: '/rgpd/' },
         {
            text: 'Documentation',
            ariaLabel: 'Documentation',
            items: [
               { text: 'Documentation de base', link: '/documentation/base/' },
               { text: 'Document de conception', link: '/documentation/dc/' },
               { text: 'Document d\'architecture technique', link: '/documentation/dat/' }
            ]
         },
         { text: 'USite', link: 'https://usite.fr' }
      ],

      sidebar: {
         '/documentation/base/': [
            {
               title: 'Documentation de base',
               collapsable: false,
               children: [
                  '',
                  'suivi_des_decisions',
                  {
                     title: "Gestion de projet",
                     collapsable: true,
                     children: [
                        'gestion_de_projet/DOR_DOD'
                     ]
                  },
                  {
                     title: "Developement",
                     collapsable: true,
                     children: [
                        'developement/git_flow',
                        'developement/tests_utilisateurs',
                        'developement/lighthouse',
                        {
                           title: "Tests",
                           collapsable: true,
                           children: [
                              'developement/tests/integrations_tests',
                              'developement/tests/e2e_tests'
                           ]
                        },
                        {
                           title: "Charte graphique",
                           collapsable: true,
                           children: [
                              'developement/charte_graphique/palette',
                              'developement/charte_graphique/logos'
                           ]
                        },
                        {
                           title: "Génération des sites clients",
                           collapsable: true,
                           children: [
                              'developement/generation_des_sites_clients/explication',
                              'developement/generation_des_sites_clients/ci'
                           ]
                        }
                     ]
                  },
                  {
                     title: "Infrastructure",
                     collapsable: true,
                     children: [
                        'infrastructure/',
                        {
                           title: "Déploiements applicatifs",
                           collapsable: true,
                           children: [
                              'infrastructure/app_deployment/cert_manager',
                              'infrastructure/app_deployment/horizontal_pod_autoscaling',
                              'infrastructure/app_deployment/pod_disruption_budget',
                              'infrastructure/app_deployment/metrics_server',
                              'infrastructure/app_deployment/problemes_connus'
                           ]
                        },
                        {
                           title: "Cloud",
                           collapsable: true,
                           children: [
                              'infrastructure/cloud/azure_blueprints',
                              'infrastructure/cloud/deploiement_des_ressources_azure',
                           ]
                        },
                        {
                           title: "Chaos",
                           collapsable: true,
                           children: [
                              'infrastructure/chaos/',
                              'infrastructure/chaos/scenarios',
                              'infrastructure/chaos/redeploiement_complet_de_l-infra'
                           ]
                        },
                        {
                           title: "Monitoring",
                           collapsable: true,
                           children: [
                              'infrastructure/monitoring/',
                              'infrastructure/monitoring/choix_de_la_solution'
                           ]
                        },
                        {
                           title: "On Premises",
                           collapsable: true,
                           children: [
                              {
                                 title: 'K8S',
                                 collapsable: true,
                                 children: [
                                 ]
                              },
                              {
                                 title: 'PfSense',
                                 collapsable: true,
                                 children: [
                                    'infrastructure/on-premises/pfsense/configuration_firewall_pfsense',
                                    {
                                       title: 'Opérations courantes',
                                       collapsable: true,
                                       children: [
                                          'infrastructure/on-premises/pfsense/operations_courantes/lets_encrypt_configuration_acme',
                                          'infrastructure/on-premises/pfsense/operations_courantes/lets_encrypt_renouvellement'
                                       ]
                                    }
                                 ]
                              }
                           ]
                        }
                     ]
                  }
               ]
            },
         ],
         '/documentation/dat/': [
            {
               title: 'Document d\'architecture technique',
               collapsable: false,
               children: [
                  '',
                  'i-contexte',
                  'ii-representation_fonctionnelle',
                  'iii-infrastructure_detaille',
                  'iv-decisions_d_architecture',
                  'v-les_risques',
                  'vi-les_couts'
               ]
            }
         ],
         '/documentation/dc/': [
            {
               title: 'Document de conception',
               collapsable: false,
               children: [
                  '',
                  'domaine_application',
                  'document_de_reference',
                  'normes_standard_et_outils',
                  'conception_general',
                  'conception_detailles_des_composants'
               ]
            }
         ],
         '/rgpd/': [
            {
               title: 'RGPD',
               collapsable: false,
               children: [
                  '',
                  'i-introduction',
                  'ii-gestion-en-continu-de-la-documentation-rgpd',
                  'iii-gestion-avancee-des-donnees-personnelles-et-de-leurs-acces',
                  'iv-conformite-de-l-architecture-avec-le-rgpd',
                  'v-gestion-du-code-source-et-conformite-avec-le-rgpd',
                  'vi-procedures-d-information-aux-personnes',
                  'vii-conformite-de-l-analyse-statistique',
                  'viii-gestion-des-risques-en-termes-de-conformite',
                  {
                     title: 'Annexes',
                     collapsable: true,
                     path: '/rgpd/annexes/',
                     children: [
                        'annexes/plan-d-action',
                        'annexes/contrat-de-services-web',
                        'annexes/politique-de-confidentialite'
                     ]
                  }
               ]
            }
         ]
      }
   },

   markdown: {
      extendMarkdown: md => {
         md.use(require('markdown-it-task-lists'))
      }
   }
}
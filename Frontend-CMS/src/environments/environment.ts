// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,

  api_url: 'https://localhost:7149/api/',
  auth_url: 'https://localhost:7149/',
  default_hub_url: 'https://localhost:7149/hub',
  deployment_hub_url: 'https://localhost:7149/deployment',


  client_id: 'local-dev',
  client_secret: 'secret',
  redirect_url: 'http://localhost:4200/callback',
  home_url: 'http://localhost:3000'
}

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.

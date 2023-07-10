import { createActionGroup, emptyProps, props } from '@ngrx/store';
import { ISite } from '../../interfaces/ISite.interface';
import { IProgress } from '../../interfaces/IProgress.interface';

export const DeploymentActions = createActionGroup({
  source: 'Deployment',
  events: {
    'Load site': props<{ siteId: string }>(),
    'Load success': props<{ site: ISite }>(),
    'Change publishing': props<{ isPublishing: boolean }>(),

    'Create subdomain': props<{ subDomain: string }>(),
    'start Deployment': props<{ comment: string }>(),
    'On Progress': props<{ progress: IProgress }>(),
  },
});

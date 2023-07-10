import { createActionGroup, props } from "@ngrx/store"


export const SplashActions = createActionGroup({
   source: 'Splash',
   events: {
      'Set Splash': props<{ isLoading: boolean}>(),
   }
})
import { createActionGroup, props } from "@ngrx/store"
import IAuth from "../../interfaces/IAuth.interface"

export const AuthActions = createActionGroup({
   source: 'Auth',
   events: {
      'Auth': props<{ code: string, verifier: string, siteId: string }>(),
      'Auth Success': props<{ auth: IAuth, siteId?: string }>(),
      'Refresh Auth': props<{ refreshToken: string, siteId?: string }>()
   }
})
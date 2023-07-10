import { createFeature, createReducer, createSelector, on } from "@ngrx/store"
import { AuthActions } from "./auth.action"
import IAuth from "../../interfaces/IAuth.interface"

export interface AuthState {
   auth: IAuth | null,
}

const initialState: AuthState = {
   auth: null
}

export const AuthFeature = createFeature({
   name: 'auth',
   reducer: createReducer(
      initialState,
      on(AuthActions.authSuccess, (_, { auth }) => ({ auth }))
   )
})

export const {
   name,
   reducer,
   selectAuth,
   selectAuthState
} = AuthFeature

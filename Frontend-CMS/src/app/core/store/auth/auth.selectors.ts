import { createSelector } from "@ngrx/store"
import { selectAuth } from "./auth.feature"
import IAuth from "../../interfaces/IAuth.interface"

export const selectAccessToken = createSelector(
   selectAuth,
   (auth: IAuth | null) => auth ? auth.access_token : null
 );
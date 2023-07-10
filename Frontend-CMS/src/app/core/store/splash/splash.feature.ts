import { createFeature, createReducer, on } from '@ngrx/store'
import { SplashActions } from './splash.actions'

interface State {
   isLoading: boolean,
}

const initialState: State = {
   isLoading: false,
}

export const SplashFeature = createFeature({
   name: 'splash',
   reducer: createReducer(
      initialState,
      on(SplashActions.setSplash, (state: State, { isLoading }) => ({isLoading: isLoading})),
   )

})

export const {
   name,
   reducer,
   selectIsLoading,
   selectSplashState
} = SplashFeature
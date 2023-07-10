import { createFeature, createReducer, on } from "@ngrx/store"
import { IPage } from "../../interfaces/IPage.interface"
import { PagesActions } from "./pages.actions"

export interface PagesState {
   pages: Array<IPage>,
   idSite: string | undefined,
   selectedPage: IPage | undefined
}

const initialState: PagesState = {
   pages: [],
   idSite: undefined,
   selectedPage: undefined
}

export const PagesFeature = createFeature({
   name: 'pages',
   reducer: createReducer(
      initialState,
      on(PagesActions.pagesLoadedSuccess, (state: PagesState, { pages, idSite}) => 
         ({ ...state, pages: pages, idSite: idSite })),

      on(PagesActions.changeSelectedSuccess, (state: PagesState, { page }) => 
         ({ ...state, selectedPage: page })),

      on(PagesActions.updatePageSuccess, (state: PagesState, {page}) => 
         ({...state, pages: state.pages.map(el => el.id === page.id ? page : el)}))
   )
})

export const {
   name,
   reducer,
   selectPagesState,
   selectPages,
   selectSelectedPage,
   selectIdSite
} = PagesFeature
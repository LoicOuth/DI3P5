import { createActionGroup, props } from "@ngrx/store"
import { IPage } from "../../interfaces/IPage.interface"

export const PagesActions = createActionGroup({
   source: 'Page',
   events: {
      'Load Pages': props<{ idSite: string, page: IPage | null }>(),
      'Pages Loaded Success': props<{ pages: Array<IPage>, idSite: string }>(),
      'Change Selected': props<{ page: IPage }>(),
      'Change Selected Success': props<{ page: IPage }>(),
      "Add Page": props<{ name: string, description: string }>(),
      "update Page": props<{name: string, description: string, isFirst: boolean}>(),
      "update Page Success": props<{page: IPage}>()
   }
})
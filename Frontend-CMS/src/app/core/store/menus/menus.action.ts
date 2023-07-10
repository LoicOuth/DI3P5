import { createActionGroup, props } from "@ngrx/store"
import { IMenu } from "../../interfaces/IMenu.interface"


export const MenusAction = createActionGroup({
   source: 'Menu',
   events: {
      'Load Menus': props<{ idSite: string, menu: IMenu | null }>(),
      'Menu Loaded Success': props<{ menu: IMenu, idSite: string }>(),

      'Add Menu': props<{ linkName: string, pageId: string }>(),
      'Update Menus': props<{ menu: IMenu }>(),
   }
})
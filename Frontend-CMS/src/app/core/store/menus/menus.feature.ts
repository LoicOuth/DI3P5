import { createFeature, createReducer, on } from "@ngrx/store"
import { MenusAction } from "./menus.action"
import { IMenu } from "../../interfaces/IMenu.interface"
export default interface MenuState {
    menu: IMenu | null
}

const initialState: MenuState = {
    menu: null
}

export const MenusFeature = createFeature({
    name: 'menus',
    reducer: createReducer(
        initialState,
        on(MenusAction.menuLoadedSuccess, (state: MenuState, { menu, idSite }
        ) => ({ ...state, menu: menu, idSite: idSite })),
        on(MenusAction.updateMenus, ({ menu }) => ({ menu: menu })),
    )
})

export const {
    name,
    reducer,
    selectMenu,
    selectMenusState
} = MenusFeature
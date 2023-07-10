import { Injectable } from "@angular/core"
import { Actions, createEffect, ofType } from "@ngrx/effects"
import { Store } from '@ngrx/store'
import { MenusAction } from "./menus.action"
import { MenuService } from "../../services/menu/menu.service"
import { EMPTY, catchError, of, switchMap, withLatestFrom } from "rxjs"
import { AddLinkDto } from "../../models/Dtos/AddLink.dto"
import { PagesFeature } from "../pages/pages.feature"

@Injectable()
export class MenusEffects {

   constructor(
      private actions$: Actions,
      private menuService: MenuService,
      private store$: Store

   ) { }

   loadMenu$ = createEffect(() => this.actions$.pipe(
      ofType(MenusAction.loadMenus),
      switchMap(action => this.menuService.getAll(action.idSite)
         .pipe(
            switchMap(menu => of(
               MenusAction.menuLoadedSuccess({ menu, idSite: action.idSite })
            )
            ),
            catchError(() => EMPTY)
         )
      )
   ))

   addMenu$ = createEffect(() => this.actions$.pipe(
      ofType(MenusAction.addMenu),
      withLatestFrom(this.store$.select(PagesFeature.selectPagesState)),
      switchMap(([action, state]) => this.menuService.addLink(new AddLinkDto(action.linkName, action.pageId, state.idSite!, state.selectedPage!.id))
         .pipe(
            switchMap(() => of(
               MenusAction.loadMenus({ idSite: state.idSite!, menu: null })
            )))
      )
   ),
      { dispatch: false })
}
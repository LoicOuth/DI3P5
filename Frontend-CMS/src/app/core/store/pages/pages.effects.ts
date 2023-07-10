import { ElementsActions } from './../elements/elements.actions'
import { Injectable } from "@angular/core"
import { createEffect, ofType, Actions } from "@ngrx/effects"
import { catchError, EMPTY, of, switchMap, tap, withLatestFrom } from "rxjs"
import { PagesService } from "../../services/pages/pages.service"
import { PagesActions } from "./pages.actions"
import { AddPage } from '../../models/Dtos/AddPage.dto'
import { PagesFeature } from './pages.feature'
import { Store } from '@ngrx/store'
import { UpdatePage } from '../../models/Dtos/UpdatePage.dto'

@Injectable()
export class PagesEffects {

   constructor(
      private actions$: Actions,
      private pagesService: PagesService,
      private store$: Store

   ) { }

   loadPages$ = createEffect(() => this.actions$.pipe(
      ofType(PagesActions.loadPages),
      switchMap(action => this.pagesService.getAll(action.idSite)
         .pipe(
            switchMap(pages => of(
                  PagesActions.pagesLoadedSuccess({ pages, idSite: action.idSite }),
                  PagesActions.changeSelected({page: action.page === null ? pages.find(p => p.isFirst) ?? pages[0] : action.page})
               )
            ),
            catchError(() => EMPTY)
         )
      )
   ))

   changSelected = createEffect(() => this.actions$.pipe(
      ofType(PagesActions.changeSelected),
      switchMap(action => {
         return of(
            ElementsActions.loadElements({ pageId: action.page.id }),
            PagesActions.changeSelectedSuccess({page: action.page}),
            ElementsActions.resetSelectedElement()
         )
      } )
   ))

   addPage$ = createEffect(() => this.actions$.pipe(
      ofType(PagesActions.addPage),
      withLatestFrom(this.store$.select(PagesFeature.selectIdSite)),
      switchMap(([action, idSite]) => this.pagesService.addPage(new AddPage(action.name, action.description, idSite!))
         .pipe(
            switchMap(page => of(
               PagesActions.loadPages({ idSite: idSite!, page }),
            )),
            catchError(() => EMPTY)
         )
      )
   ))

   udpatePage$ = createEffect(() => this.actions$.pipe(
      ofType(PagesActions.updatePage),
      withLatestFrom(this.store$.select(PagesFeature.selectSelectedPage)),
      switchMap(([action, selectedPage]) => 
      this.pagesService.updatePage(
            new UpdatePage(action.name, action.description, action.isFirst, selectedPage!.id)
         )
      )
   ), {dispatch: false})

   onUpdatePageSuccess$ = createEffect(() => this.actions$.pipe(
      ofType(PagesActions.updatePageSuccess),
      tap(data => {
         this.store$.dispatch(PagesActions.changeSelectedSuccess({page: data.page}))
      })
   ),
      { dispatch: false }
   )

}
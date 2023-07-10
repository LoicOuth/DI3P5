import { ElementsFeature } from './elements.feature'
import { Store } from '@ngrx/store'
import { SplashActions } from './../splash/splash.actions'
import { ElementsActions } from './elements.actions'
import { ElementsService } from './../../services/elements/elements.service'
import { Injectable } from "@angular/core"
import { createEffect, ofType, Actions } from "@ngrx/effects"
import { catchError, EMPTY, mergeMap, of, tap, withLatestFrom, switchMap } from "rxjs"
import AddStyleDto from '../../models/Dtos/AddStyle.dto'
import UpdateContentDto from '../../models/Dtos/UpdateContent.dto'
import { PagesFeature } from "../pages/pages.feature"
import AddElementDto from '../../models/Dtos/AddElement.dto'
import UpdatePositionDto from '../../models/Dtos/UpdatePosition.dto'
import { UpdateLinkPositionDto } from '../../models/Dtos/UpdateLinkPosition.dto'

@Injectable()
export class ElementsEffects {

   constructor(
      private actions$: Actions,
      private store$: Store,
      private elementsService: ElementsService
   ) { }

   loadElements$ = createEffect(() => this.actions$.pipe(
      ofType(ElementsActions.loadElements),
      mergeMap((action) => this.elementsService.getAll(action.pageId)
         .pipe(
            mergeMap(elements => of(
               ElementsActions.elementsLoadedSuccess({ elements }),
               SplashActions.setSplash({ isLoading: false })
            )),
            catchError(() => EMPTY)
         )
      )
   ))

   addElement$ = createEffect(() => this.actions$.pipe(
      ofType(ElementsActions.addElement),
      withLatestFrom(this.store$.select(PagesFeature.selectSelectedPage)),
      switchMap(([action, store]) =>
         this.elementsService.addElement(
            new AddElementDto(action.name, action.description, action.idParent || store!.id, action.elementType)
         )
      )
   ),
      { dispatch: false })

   deleteElement$ = createEffect(() => this.actions$.pipe(
      ofType(ElementsActions.deleteElement),
      withLatestFrom(this.store$.select(ElementsFeature.selectSelectedElement)),
      switchMap(([_, store]) =>
         this.elementsService.deleteElement(store?.element.id!)
      )
   ),
      { dispatch: false })


   addStyle$ = createEffect(() => this.actions$.pipe(
      ofType(ElementsActions.addStyle),
      withLatestFrom(this.store$.select(ElementsFeature.selectSelectedElement)),
      switchMap(([action, store]) =>
         this.elementsService.addStyle(
            new AddStyleDto(store?.element.id!, action.property, action.value)
         )
      )
   ),
      { dispatch: false })

   updateElementContent$ = createEffect(() => this.actions$.pipe(
      ofType(ElementsActions.updateElementContent),
      withLatestFrom(this.store$.select(ElementsFeature.selectSelectedElement)),
      switchMap(([action, store]) =>
         this.elementsService.updateContent(
            new UpdateContentDto(store?.element.id!, action.content)
         )
      )
   ),
      { dispatch: false })


   updateElementPosition$ = createEffect(() => this.actions$.pipe(
      ofType(ElementsActions.updateElementPosition),
      withLatestFrom(this.store$.select(PagesFeature.selectSelectedPage)),
      switchMap(([action, store]) =>
         this.elementsService.updatePosition(
            new UpdatePositionDto(action.elementId, action.positionCounter, store!.id)
         )
      )
   ),
      { dispatch: false })

   updateLinkPosition$ = createEffect(() => this.actions$.pipe(
      ofType(ElementsActions.updateLinkPosition),
      withLatestFrom(this.store$.select(PagesFeature.selectSelectedPage)),
      switchMap(([action, page]) =>
         this.elementsService.updateLinkPosition(
            new UpdateLinkPositionDto(page?.id!, action.elementId, action.positionCounter)
         )
      )
   ),
      { dispatch: false })

}
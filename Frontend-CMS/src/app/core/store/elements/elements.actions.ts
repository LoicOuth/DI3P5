import { StyleProperty } from './../../enums/StyleProperty.enum'
import { IElement } from './../../interfaces/IElement.interface'
import { createActionGroup, emptyProps, props } from "@ngrx/store"
import { TypeElement } from '../../enums/TypeElement.enum'


export const ElementsActions = createActionGroup({
   source: 'Element',
   events: {
      'Load Elements': props<{ pageId: string }>(),
      'Elements Loaded Success': props<{ elements: Array<IElement> }>(),

      'Add Element': props<{ name: string, description: string, elementType: TypeElement, idParent?: string }>(),

      'Update Element': props<{ element: IElement }>(),

      'Delete Element': emptyProps(),
      'Delete Element In Store': props<{ elementId: string }>(),

      'Update Element Content': props<{ content: string }>(),
      'New Element': props<{ element: IElement }>(),

      'Set Selected Element': props<{ htmlElement: HTMLElement }>(),
      'Reset Selected Element': emptyProps(),

      'Add style': props<{ property: StyleProperty, value: string }>(),

      'Update Element Position': props<{ elementId: string, positionCounter: number }>(),
      'Update Link Position': props<{ elementId: string, positionCounter: number }>(),
   }
})
import { getElementById, removeElement } from './../../../utils/ElementUtils'
import { IElement } from '../../interfaces/IElement.interface'
import { ElementsActions } from './elements.actions'
import { createFeature, createReducer, on } from "@ngrx/store"
import { newElement, updateElement } from 'src/app/utils/ElementUtils'
import { ISelectedElement } from '../../interfaces/ISelectedElement'

export interface ElementsState {
   elements: Array<IElement>,
   selectedElement: ISelectedElement | null
}

const initialState: ElementsState = {
   elements: [],
   selectedElement: null
}

export const ElementsFeature = createFeature({
   name: 'elements',
   reducer: createReducer(
      initialState,
      on(ElementsActions.elementsLoadedSuccess, (state: ElementsState, { elements }) => ({ ...state, elements: elements })),
      on(ElementsActions.newElement, (state: ElementsState, { element }) => ({ ...state, elements: newElement(state.elements, element) })),
      on(ElementsActions.deleteElementInStore, (state: ElementsState, { elementId }) => ({ elements: removeElement(state.elements, elementId), selectedElement: null })),
      on(ElementsActions.updateElement, (state: ElementsState, { element }) => {
         let elements = updateElement(state.elements, element)
         let selectedElements = null
         if (state.selectedElement != null) {
            selectedElements = getElementById(elements, element.id)!
         }

         return {
            elements: elements,
            selectedElement: selectedElements == null ? null : {
               element: selectedElements,
               html: state.selectedElement?.html!
            }
         }
      }),
      on(ElementsActions.setSelectedElement, (state: ElementsState, { htmlElement }) => {
         state.selectedElement?.html?.classList.remove('selected')
         let element = getElementById(state.elements, htmlElement.id)!

         htmlElement?.classList.add('selected')

         return {
            ...state,
            selectedElement: {
               element,
               html: htmlElement
            }
         }
      }),

      on(ElementsActions.resetSelectedElement, (state: ElementsState) => {
         state.selectedElement?.html?.classList.remove('selected')

         return {
            ...state,
            selectedElement: null
         }
      })
   )
})


export const {
   name,
   reducer,
   selectElements,
   selectSelectedElement,
   selectElementsState
} = ElementsFeature
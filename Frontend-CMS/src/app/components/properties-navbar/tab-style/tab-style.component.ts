import { Component } from '@angular/core';
import { Store } from '@ngrx/store'
import { StyleProperty } from 'src/app/core/enums/StyleProperty.enum'
import { TypeElement } from 'src/app/core/enums/TypeElement.enum'
import { ElementsFeature } from 'src/app/core/store/elements/elements.feature'

@Component({
  selector: 'app-tab-style',
  templateUrl: './tab-style.component.html'
})
export class TabStyleComponent {
  TypeElement = TypeElement
  StyleProperty = StyleProperty

  constructor(private store: Store) { }

  selectedElement = this.store.select(ElementsFeature.selectSelectedElement)
}

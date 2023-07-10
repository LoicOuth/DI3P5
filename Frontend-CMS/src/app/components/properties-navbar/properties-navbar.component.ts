import { Component } from '@angular/core'
import { Store } from '@ngrx/store'
import { TypeElement } from 'src/app/core/enums/TypeElement.enum'
import { ElementsFeature } from 'src/app/core/store/elements/elements.feature'

@Component({
  selector: 'properties-navbar',
  templateUrl: './properties-navbar.component.html',
  styleUrls: ['./properties-navbar.component.css']
})
export class PropertiesNavbarComponent {

  selectElement = this.store.select(ElementsFeature.selectSelectedElement);

  constructor(private store: Store) { }

  getEnumString(type: TypeElement) {
    return TypeElement[type]
  }
}

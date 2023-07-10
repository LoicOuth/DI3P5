import { Component, OnInit } from '@angular/core'
import { Store } from '@ngrx/store'
import { StyleProperty } from 'src/app/core/enums/StyleProperty.enum'
import { ElementsService } from 'src/app/core/services/elements/elements.service'
import { ElementsFeature } from 'src/app/core/store/elements/elements.feature'

@Component({
  selector: 'app-flex-edit',
  templateUrl: './flex-edit.component.html',
  styleUrls: ['./flex-edit.component.css']
})
export class FlexEditComponent implements OnInit {

  constructor(private store: Store, private elementService: ElementsService) { }

  direction: string = 'flex-row'
  selectedElement = this.store.select(ElementsFeature.selectSelectedElement)


  ngOnInit(): void {

    this.selectedElement.subscribe(el => {
      let directionElement = el?.element.styles.find(el => el.property === StyleProperty.FlexDirection)
      if (directionElement) {
        this.direction = directionElement.value
      }
    })
  }

  onDirectionChange(value: string): void {
    this.elementService.updateClassInStore({
      property: StyleProperty.FlexDirection,
      value
    })
  }
}

import { Component, OnInit } from '@angular/core';
import { MatButtonToggleChange } from '@angular/material/button-toggle'
import { Store } from '@ngrx/store'
import { StyleProperty } from 'src/app/core/enums/StyleProperty.enum'
import { ElementsService } from 'src/app/core/services/elements/elements.service'
import { ElementsFeature } from 'src/app/core/store/elements/elements.feature'

@Component({
  selector: 'app-text-decoration',
  templateUrl: './text-decoration.component.html'
})
export class TextDecorationComponent implements OnInit {

  selectedElement = this.store.select(ElementsFeature.selectSelectedElement)

  textDecoration: Array<string> = []

  constructor(private store: Store, private elementService: ElementsService) { }

  ngOnInit(): void {
    this.selectedElement.subscribe(el => {
      let textDecorationElement = el?.element.styles.find(el => el.property === StyleProperty.TextDecoration)
      if(textDecorationElement) {
       this.textDecoration = textDecorationElement.value.split(' ')
      }
      else {
        this.textDecoration = []
      }
     })
  }

  onTextDecorationChange(event: MatButtonToggleChange): void {
    this.elementService.updateClassInStore({
      property: StyleProperty.TextDecoration,
      value: event.value.length === 0 ? null : event.value.join(' ')
    })
  }

}

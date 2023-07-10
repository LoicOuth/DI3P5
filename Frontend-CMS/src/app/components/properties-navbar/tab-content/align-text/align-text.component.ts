import { Component,OnInit } from '@angular/core';
import { MatButtonToggleChange } from '@angular/material/button-toggle'
import { Store } from '@ngrx/store'
import { StyleProperty } from 'src/app/core/enums/StyleProperty.enum'
import { ElementsService } from 'src/app/core/services/elements/elements.service'
import { ElementsFeature } from 'src/app/core/store/elements/elements.feature'

@Component({
  selector: 'app-align-text',
  templateUrl: './align-text.component.html'
})
export class AlignTextComponent implements OnInit {

  constructor(private store: Store, private elementService: ElementsService) { }

  selectedElement = this.store.select(ElementsFeature.selectSelectedElement)
  
  textAlign?: String

  ngOnInit(): void {
     this.selectedElement.subscribe(el => {
     let textAlignElement = el?.element.styles.find(el => el.property === StyleProperty.TextAlign)
     if(textAlignElement) {
      this.textAlign = textAlignElement.value
     }
     else {
      this.textAlign = 'text-left'
     }
    }) 
  }

  onTextAlignChange(event: MatButtonToggleChange) {
    this.elementService.updateClassInStore({
      property: StyleProperty.TextAlign, 
      value: event.value
    })
  }
}

import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store'
import { StyleProperty } from 'src/app/core/enums/StyleProperty.enum'
import { ElementsService } from 'src/app/core/services/elements/elements.service'
import { ElementsFeature } from 'src/app/core/store/elements/elements.feature'
import { getValueInString } from 'src/app/utils/StyleUtils'

@Component({
  selector: 'app-font-size',
  templateUrl: './font-size.component.html'
})
export class FontSizeComponent implements OnInit {

  constructor(private store: Store, private elementService: ElementsService) { }

  fontSize?: string
  showAdvanced: boolean = false
  predefinedSize: Array<string> = ["60", "36", "24", "16"]

  selectedElement = this.store.select(ElementsFeature.selectSelectedElement)
  
  ngOnInit(): void {
    this.selectedElement.subscribe(el => {
     let fontSizeElement = el?.element.styles.find(el => el.property === StyleProperty.FontSize)
     if(fontSizeElement) {
      this.fontSize = getValueInString(fontSizeElement.value)
     }
     else {
      this.fontSize = '16'
     }
     this.showAdvanced = !this.predefinedSize.some(el => el === this.fontSize!)
    })
  }

  onFontSizeChange(value: number) {
    this.elementService.updateClassInStore({
      property: StyleProperty.FontSize, 
      value: `text-[${value}px]`
    })
  }

}

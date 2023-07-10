import { Component, Input, OnInit } from '@angular/core'
import { Store } from '@ngrx/store'
import { StyleProperty } from 'src/app/core/enums/StyleProperty.enum'
import { ElementsService } from 'src/app/core/services/elements/elements.service'
import { ElementsFeature } from 'src/app/core/store/elements/elements.feature'
import { getValueInString } from 'src/app/utils/StyleUtils'

@Component({
  selector: 'app-size-edit',
  templateUrl: './size-edit.component.html',
  styleUrls: ['./size-edit.component.css']
})
export class SizeEditComponent implements OnInit {
  @Input() property: StyleProperty = StyleProperty.Width
  @Input() label: string = "Width"
  @Input() unit: string = "%"

  constructor(private store: Store, private elementService: ElementsService) { }

  showAdvanced: boolean = false
  size: string = "auto"
  selectedElement = this.store.select(ElementsFeature.selectSelectedElement)
  predefinedSize: Array<string> = ["10", "20", "30", "40", "50", "60", "70", "80", "90", "100", "auto"]

  ngOnInit(): void {
    this.selectedElement.subscribe(el => {
      let sizeElement = el?.element.styles.find(el => el.property === this.property)
      if (sizeElement) {
        let sizeValue = getValueInString(sizeElement.value)
        this.size = sizeValue === "" ? 'auto' : sizeValue
      }
      else {
        this.size = "auto"
      }

      this.showAdvanced = !this.predefinedSize.some(el => el === this.size!)
    })
  }

  onSizeChange(value: string | number) {
    this.elementService.updateClassInStore({
      property: this.property,
      value: `w-[${value === 'auto' ? value : value + this.unit}]`
    })
  }

}

import { Component, Input, OnInit } from '@angular/core'
import { Store } from '@ngrx/store'
import { StyleProperty } from 'src/app/core/enums/StyleProperty.enum'
import { ElementsService } from 'src/app/core/services/elements/elements.service'
import { ElementsFeature } from 'src/app/core/store/elements/elements.feature'
import { getColorFromClass } from 'src/app/utils/StyleUtils'

@Component({
  selector: 'app-choose-color',
  templateUrl: './choose-color.component.html'
})
export class ChooseColorComponent implements OnInit {
  @Input() property: StyleProperty = StyleProperty.TextColor
  @Input() label: string = "Text color"

  private timeout: NodeJS.Timeout | null = null;

  constructor(private store: Store, private elementService: ElementsService) { }

  selectedElement = this.store.select(ElementsFeature.selectSelectedElement)

  color?: string

  ngOnInit(): void {
    this.selectedElement.subscribe(el => {
      let colorElement = el?.element.styles.find(el => el.property === this.property)
      if (colorElement) {
        this.color = getColorFromClass(colorElement.value)
      }
      else {
        this.color = "#000"
      }
    })
  }

  onColorChange(event: Event): void {
    if (this.timeout) clearTimeout(this.timeout)

    this.timeout = setTimeout(() => {
      let htmlElement = event.target as HTMLInputElement
      let classToApply

      switch (this.property) {
        case StyleProperty.TextColor:
          classToApply = `text-[${htmlElement.value}]`
          break

        case StyleProperty.BorderColor:
          classToApply = `border-[${htmlElement.value}]`
          break

        default:
          classToApply = `bg-[${htmlElement.value}]`
          break
      }
      this.elementService.updateClassInStore({
        property: this.property,
        value: classToApply
      })
    }, 100)
  }
}

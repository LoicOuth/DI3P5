import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { StyleProperty } from 'src/app/core/enums/StyleProperty.enum';
import { ElementsService } from 'src/app/core/services/elements/elements.service';
import { ElementsFeature } from 'src/app/core/store/elements/elements.feature';

@Component({
  selector: 'app-box-shadow',
  templateUrl: './box-shadow.component.html'
})
export class BoxShadowComponent implements OnInit {

  shadow?: string;
  selectedElement = this.store.select(ElementsFeature.selectSelectedElement)


  constructor(private store: Store, private elementService: ElementsService) { }

  ngOnInit(): void {
    this.selectedElement.subscribe((el) => {
      const shadow = el?.element.styles.find(s => s.property === StyleProperty.Shadow)
      if(shadow) {
        this.shadow = shadow.value
      } else {
        this.shadow = ""
      }
    })
  }

  public onShadowChange(value: string): void {
    this.elementService.updateClassInStore({
      property: StyleProperty.Shadow, 
      value
    })
  }
}

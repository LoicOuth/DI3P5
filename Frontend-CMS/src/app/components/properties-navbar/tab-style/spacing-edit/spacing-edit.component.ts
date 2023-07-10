import { Component, OnInit, Input } from '@angular/core'
import { Store } from '@ngrx/store'
import { StyleProperty } from 'src/app/core/enums/StyleProperty.enum'
import { ElementsService } from 'src/app/core/services/elements/elements.service'
import { ElementsFeature } from 'src/app/core/store/elements/elements.feature'
import { getValueInString } from 'src/app/utils/StyleUtils'

@Component({
  selector: 'app-spacing-edit',
  templateUrl: './spacing-edit.component.html',
  styleUrls: ['./spacing-edit.component.css']
})
export class SpacingEditComponent implements OnInit {
  @Input() label = 'Set padding'
  @Input() property = StyleProperty.Padding

  private selectedElement = this.store.select(ElementsFeature.selectSelectedElement)

  constructor(private elementService: ElementsService, private store: Store) { }

  showAdvanced: boolean = false
  spacing = {
    global: "0",
    top: "0",
    bottom: "0",
    left: "0",
    right: "0"
  }

  ngOnInit(): void {
    this.selectedElement.subscribe(el => {
      const spacingElement = el?.element.styles.find(el => el.property === this.property);
      if (spacingElement) {
        this.resetSpacing();
        const joinSpacingElement = spacingElement.value.split(' ');
    
        if(joinSpacingElement.length > 1) {
          joinSpacingElement.forEach(el => {
            const value = getValueInString(el);
            if(el.includes('t') || el.includes('b') || el.includes('l') || el.includes('r')) {
              this.assignSpacingValue(el, value);
              this.showAdvanced = true
            }
          });
        } else {
          const spacingValue = getValueInString(spacingElement.value);
          this.assignSpacingValue('', spacingValue);
          this.showAdvanced = false

        }
      } else {
        this.resetSpacing();
      }
    });
  }

  private resetSpacing(): void {
    this.spacing = {
      global: "0",
      top: "0",
      bottom: "0",
      left: "0",
      right: "0"
    };
  }
  
  private assignSpacingValue(location: string, value: string): void {
    const letter = this.property === StyleProperty.Border ? location[7] : location[1];
    switch (letter) {
      case 't':
        this.spacing.top = value;
        break;
      case 'b':
        this.spacing.bottom = value;
        break;
      case 'l':
        this.spacing.left = value;
        break;
      case 'r':
        this.spacing.right = value;
        break;
      default:
        this.spacing.global = value;
        break;
    }
  }

  private getLetter(): string {
    switch(this.property){
      case StyleProperty.Padding: return 'p';
      case StyleProperty.Margin: return 'm';
      case StyleProperty.Border: return 'border';

      default: return '';
    }
  }

  onSizeChange(value: number | null) : void {
    this.elementService.updateClassInStore({
      property: this.property,
      value: `${this.getLetter()}-[${value}px]`
    })
  }

  onInputSize() {
    this.elementService.updateClassInStore({
      property: this.property,
      value: this.getInputValue()
    })
  }

  private getInputValue(): string {
    switch(this.property){
      case StyleProperty.Border:
        return `${this.getLetter()}-t-[${this.spacing.top}px] ${this.getLetter()}-b-[${this.spacing.bottom}px] ${this.getLetter()}-r-[${this.spacing.right}px] ${this.getLetter()}-l-[${this.spacing.left}px]`
      default:
        return `${this.getLetter()}t-[${this.spacing.top}px] ${this.getLetter()}b-[${this.spacing.bottom}px] ${this.getLetter()}r-[${this.spacing.right}px] ${this.getLetter()}l-[${this.spacing.left}px]`
    }
  }
}

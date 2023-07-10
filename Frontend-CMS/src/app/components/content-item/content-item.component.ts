import { Component, Input } from '@angular/core'

@Component({
  selector: 'app-content-item',
  templateUrl: './content-item.component.html'
})
export class ContentItemComponent {
  @Input() enum = '';
  @Input() isRow = false;
}

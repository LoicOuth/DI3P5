import { Component } from '@angular/core'
import { Store } from '@ngrx/store'
import { TypeElement } from 'src/app/core/enums/TypeElement.enum'
import { ElementsActions } from 'src/app/core/store/elements/elements.actions'
import { ElementsFeature } from 'src/app/core/store/elements/elements.feature'

@Component({
  selector: 'app-tab-content',
  templateUrl: './tab-content.component.html'
})
export class TabContentComponent {
  TypeElement = TypeElement

  private timeout: NodeJS.Timeout | null = null;

  constructor(private store: Store) { }

  selectedElement = this.store.select(ElementsFeature.selectSelectedElement)

  onContentChange(event: any) {
    if (this.timeout) clearTimeout(this.timeout)
    this.timeout = setTimeout(() => {
      this.store.dispatch(ElementsActions.updateElementContent({ content: event.target.value }))
    }, 300)
  }
}

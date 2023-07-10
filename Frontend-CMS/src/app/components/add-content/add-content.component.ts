import { Component, OnInit, Renderer2 } from '@angular/core'
import { Store } from '@ngrx/store'
import { TypeElement } from 'src/app/core/enums/TypeElement.enum'
import { IElement } from 'src/app/core/interfaces/IElement.interface'
import { ElementsActions } from 'src/app/core/store/elements/elements.actions'
import { ElementsFeature } from 'src/app/core/store/elements/elements.feature'

@Component({
  selector: 'add-content',
  templateUrl: './add-content.component.html',
})
export class AddContentComponent implements OnInit {
  TypeElement = TypeElement
  private elements: Array<IElement> = []
  private unlistener: Array<() => void> = [];

  constructor(private store: Store, private render: Renderer2) { }

  ngOnInit(): void {
    this.store
      .select(ElementsFeature.selectElements)
      .subscribe((elements) => (this.elements = elements))
  }

  private onDragOver(event: any): void {
    event.preventDefault()
  }

  private onDrop(id: string, typeElement: string): void {
    this.store.dispatch(
      ElementsActions.addElement({
        name: "element",
        description: "new element",
        elementType: TypeElement[typeElement as keyof typeof TypeElement],
        idParent: id,
      })
    )

    this.removeEventListenersFromElements(this.elements)
  }

  addEventListenersToElements(elements: Array<IElement>, typeElement: string): void {
    elements.forEach(element => {
      if (element.type === TypeElement.Block) {
        const DOMelement = document.getElementById(element.id)
        this.unlistener.push(this.render.listen(DOMelement, "dragover", event => this.onDragOver(event)))
        this.unlistener.push(this.render.listen(DOMelement, "drop", _ => this.onDrop(element.id, typeElement)))
        this.render.addClass(DOMelement, "usite-element-dragover")

        if (element.elementsChilds.length > 0)
          this.addEventListenersToElements(element.elementsChilds, typeElement)
      }
    })
  }

  removeEventListenersFromElements(elements: Array<IElement>): void {
    this.removeClass(elements)
    this.unlistener.forEach(el => el())
  }

  private removeClass(elements: Array<IElement>): void {
    elements.forEach(element => {
      const DOMelement = document.getElementById(element.id)
      this.render.removeClass(DOMelement, "usite-element-dragover")

      if (element.elementsChilds.length > 0) {
        this.removeClass(element.elementsChilds)
      }
    })
  }

  onDragStart(typeElement: string): void {
    this.addEventListenersToElements(this.elements, typeElement)
  }

  onDragEnd(): void {
    this.removeEventListenersFromElements(this.elements)
  }
}

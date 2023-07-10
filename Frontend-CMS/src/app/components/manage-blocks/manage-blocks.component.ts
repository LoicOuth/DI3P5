import { ElementsActions } from './../../core/store/elements/elements.actions'
import { IElement } from './../../core/interfaces/IElement.interface'
import { Component, OnInit } from '@angular/core'
import { MatDialog } from '@angular/material/dialog'
import { Store } from '@ngrx/store'
import { CdkDragDrop } from '@angular/cdk/drag-drop'
import { AddBlockComponent } from '../add-block/add-block.component'
import { selectElementsWithNoMenu } from 'src/app/core/store/elements/element.selectors'

@Component({
  selector: 'manage-blocs',
  templateUrl: './manage-blocks.component.html',
  styleUrls: ['./manage-blocks.component.css']
})
export class ManageBlocksComponent implements OnInit {

  constructor(private store: Store, private dialog: MatDialog) { }

  elements: Array<IElement> = []

  ngOnInit(): void {
    this.store.select(selectElementsWithNoMenu).subscribe(el => this.elements = el)
  }

  openAddBlock() {
    this.dialog.open(AddBlockComponent, {
      width: '50vh'
    })
  }

  onBlockDropped(event: CdkDragDrop<IElement>) {
    if(event.currentIndex == event.previousIndex) return;
    const counter = event.currentIndex - event.previousIndex

    this.store.dispatch(ElementsActions.updateElementPosition({
      elementId: this.elements[event.previousIndex].id,
      positionCounter: counter
    }))
  }
}

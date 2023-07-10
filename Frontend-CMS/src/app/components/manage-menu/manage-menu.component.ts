import { Component, OnInit } from '@angular/core';
import { AddMenuComponent } from '../add-menu/add-menu.component';
import { MatDialog } from '@angular/material/dialog';
import { IElement } from 'src/app/core/interfaces/IElement.interface';
import { Store } from '@ngrx/store';
import { CdkDragDrop } from '@angular/cdk/drag-drop';
import { ElementsActions } from 'src/app/core/store/elements/elements.actions';
import { selectMenuElement } from 'src/app/core/store/elements/element.selectors';

@Component({
  selector: 'app-manage-menu',
  templateUrl: './manage-menu.component.html',
  styleUrls: ['./manage-menu.component.css']
})
export class ManageMenuComponent implements OnInit {

  elements: IElement | undefined;


  constructor(private store: Store, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.store.select(selectMenuElement).subscribe(state => this.elements = state)
  }

  openAddMenu(): void {
    this.dialog.open(AddMenuComponent, {
      width: '50vh'
    })
  }

  onBlockDropped(event: CdkDragDrop<IElement>) {
    if(event.currentIndex == event.previousIndex) return;
    const counter = event.currentIndex - event.previousIndex

    this.store.dispatch(ElementsActions.updateLinkPosition({
      elementId: this.elements?.elementsChilds[event.previousIndex].id!,
      positionCounter: counter
    }))
  }
}

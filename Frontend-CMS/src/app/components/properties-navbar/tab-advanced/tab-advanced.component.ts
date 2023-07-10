import { Component } from '@angular/core'
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component'
import { MatDialog } from '@angular/material/dialog'
import { ConfirmDialogModel } from 'src/app/core/models/ConfirmDialogModel'
import { Store } from '@ngrx/store'
import { ElementsActions } from 'src/app/core/store/elements/elements.actions'
import { ElementsFeature } from 'src/app/core/store/elements/elements.feature'
import { ISelectedElement } from 'src/app/core/interfaces/ISelectedElement'

@Component({
  selector: 'app-tab-advanced',
  templateUrl: './tab-advanced.component.html',
  styleUrls: ['./tab-advanced.component.css']
})
export class TabAdvancedComponent {

  selectElement: ISelectedElement | undefined

  constructor(private dialog: MatDialog, private store: Store) {
    this.store.select(ElementsFeature.selectSelectedElement).subscribe(el => this.selectElement = el!)
  }

  onDeleteElement() {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: "400px",
      data: new ConfirmDialogModel("Do you really want to delete element ? (All childs elements will be deleted)")
    })

    dialogRef.afterClosed().subscribe(dialogResult => {
      if (dialogResult) {
        this.store.dispatch(ElementsActions.deleteElement())
      }
    })
  }

  onSelectParent() {
    this.store.dispatch(ElementsActions.setSelectedElement({
      htmlElement: this.selectElement?.html.parentNode as HTMLElement
    }))
  }
}

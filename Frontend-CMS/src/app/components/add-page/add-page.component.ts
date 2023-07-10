import { Component, OnInit } from '@angular/core'
import { FormControl, FormGroup, Validators } from '@angular/forms'
import { MatDialogRef } from '@angular/material/dialog'
import { Store } from '@ngrx/store'
import { PagesActions } from 'src/app/core/store/pages/pages.actions'
import { PagesFeature } from 'src/app/core/store/pages/pages.feature'

@Component({
  selector: 'add-page',
  templateUrl: './add-page.component.html'
})
export class AddPageComponent {

  private siteId!: string

  public pageFormGroup = new FormGroup({
    name: new FormControl("", Validators.required),
    description: new FormControl("")
  });

  constructor(public dialogRef: MatDialogRef<AddPageComponent>, private store: Store) {
    this.store.select(PagesFeature.selectIdSite).subscribe(el => this.siteId = el ?? "")
  }

  public submitForm(): void {
    this.store.dispatch(PagesActions.addPage({ name: this.pageFormGroup.get("name")!.value!, description: this.pageFormGroup.get("description")!.value! }))
    this.dialogRef.close()
  }
}

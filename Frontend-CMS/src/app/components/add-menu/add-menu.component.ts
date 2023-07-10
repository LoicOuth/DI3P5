import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Store } from '@ngrx/store';
import { IPage } from 'src/app/core/interfaces/IPage.interface';
import { MenusAction } from 'src/app/core/store/menus/menus.action';
import { PagesFeature } from 'src/app/core/store/pages/pages.feature';

@Component({
  selector: 'app-add-menu',
  templateUrl: './add-menu.component.html'
})
export class AddMenuComponent implements OnInit {

  linkFormGroup = new FormGroup({
    name: new FormControl("", Validators.required),
    pageId: new FormControl("", Validators.required)
  })

  pages?: IPage[]

  constructor(private store: Store, public dialogRef: MatDialogRef<AddMenuComponent>) { }

  ngOnInit(): void {
    this.store.select(PagesFeature.selectPages).subscribe(state => this.pages = state)
  }

  submitForm(): void {
    const name = this.linkFormGroup.get("name")!.value!
    const pageId = this.linkFormGroup.get("pageId"!)?.value!    
    
    this.store.dispatch(MenusAction.addMenu({
      linkName: name,
      pageId: pageId
    }))

    this.dialogRef.close()
  }
}

import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms'
import { Store } from '@ngrx/store'
import { PagesActions } from 'src/app/core/store/pages/pages.actions'
import { PagesFeature } from 'src/app/core/store/pages/pages.feature'

@Component({
  selector: 'app-page-detail',
  templateUrl: './page-detail.component.html',
  styleUrls: ['./page-detail.component.css']
})
export class PageDetailComponent implements OnInit {

  pageFormGroup = new FormGroup({
    name: new FormControl("", Validators.required),
    description: new FormControl(""),
    isFirst: new FormControl(false, Validators.required)
  });

  constructor(private store: Store) { }

  ngOnInit(): void {
    this.store.select(PagesFeature.selectSelectedPage).subscribe(el => {
      this.pageFormGroup.setValue({
        name: el!.name,
        description: el!.description,
        isFirst: el!.isFirst
      })
    })
  }

  submitForm(): void {
    this.store.dispatch(PagesActions.updatePage({ 
      name: this.pageFormGroup.get("name")!.value!,  
      description: this.pageFormGroup.get("description")!.value!,
      isFirst: this.pageFormGroup.get("isFirst")!.value!
    }))
  }

}

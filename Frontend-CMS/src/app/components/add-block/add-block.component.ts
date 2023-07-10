import { TypeElement } from '../../core/enums/TypeElement.enum'
import { ElementsActions } from '../../core/store/elements/elements.actions'
import { Component } from '@angular/core'
import { FormControl, FormGroup, Validators } from '@angular/forms'
import { MatDialogRef } from '@angular/material/dialog'
import { Store } from '@ngrx/store'

@Component({
  selector: 'app-add-block',
  templateUrl: './add-block.component.html'
})
export class AddBlockComponent {

  blockFormGroup = new FormGroup({
    name: new FormControl('', Validators.required),
    description: new FormControl('')
  })

  constructor(
    public dialogRef: MatDialogRef<AddBlockComponent>,
    private store: Store,
  ) { }

  submitForm() {
    const name = this.blockFormGroup.get('name')!.value!
    const description = this.blockFormGroup.get('description')!.value

    this.store.dispatch(ElementsActions.addElement({
      description: description!,
      name: name,
      elementType: TypeElement.Block
    }))

    this.dialogRef.close()
  }

}

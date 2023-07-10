import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Store } from '@ngrx/store';
import { ISelectedElement } from 'src/app/core/interfaces/ISelectedElement';
import { ElementsService } from 'src/app/core/services/elements/elements.service';
import { ElementsFeature } from 'src/app/core/store/elements/elements.feature';

@Component({
  selector: 'app-select-file',
  templateUrl: './select-file.component.html',
})
export class SelectFileComponent implements OnInit {
  @ViewChild('fileInput') fileInput!: ElementRef;

  myFile?: File | null;

  constructor(private store: Store, private elementService: ElementsService) {}

  selectedElement?: ISelectedElement | null;

  ngOnInit(): void {
    this.store.select(ElementsFeature.selectSelectedElement).subscribe((el) => {
      this.selectedElement = el;
    });
  }

  selectFile(event: any) {
    this.myFile = event.target.files[0];

    this.elementService
      .updateUrlElement(event.target.files[0], this.selectedElement!.element.id)
      .subscribe((_) => (this.myFile = null));
  }

  onClickFileInputButton(): void {
    this.fileInput.nativeElement.click();
  }
}

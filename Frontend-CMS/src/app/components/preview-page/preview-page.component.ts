import { Store } from '@ngrx/store';
import { HtmlGeneration } from './../../utils/html-generation';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { ElementsFeature } from 'src/app/core/store/elements/elements.feature';

@Component({
  selector: 'app-preview-page',
  templateUrl: './preview-page.component.html',
  providers: [ HtmlGeneration ]
})
export class PreviewPageComponent {

  constructor(private htmlGeneration: HtmlGeneration, private store: Store) { }
  @ViewChild('pageContainer') pageContainer!: ElementRef;

  elements = this.store.select(ElementsFeature.selectElements);

  ngAfterViewInit(): void {
    this.elements.subscribe((elements) => {
      this.pageContainer.nativeElement.innerHTML = '';
      elements.forEach((el) => {
        this.htmlGeneration.generateHtml(
          el,
          this.pageContainer,
          null,
          undefined,
          null,
          true
        );
      });
    });
  }
}

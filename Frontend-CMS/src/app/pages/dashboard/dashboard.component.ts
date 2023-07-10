import { ElementsActions } from './../../core/store/elements/elements.actions';
import { ElementsFeature } from '../../core/store/elements/elements.feature';
import { Component, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { Store } from '@ngrx/store';
import { MenusFeature } from 'src/app/core/store/menus/menus.feature';
import { HtmlGeneration } from 'src/app/utils/html-generation';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  providers: [HtmlGeneration],
})
export class DashboardComponent implements AfterViewInit {
  @ViewChild('pageContainer') pageContainer!: ElementRef;
  @ViewChild('menuContainer') menuContainer!: ElementRef;

  constructor(private store: Store, private htmlGeneration: HtmlGeneration) {}

  elementsState = this.store.select(ElementsFeature.selectElementsState);
  menusState = this.store.select(MenusFeature.selectMenusState);

  ngAfterViewInit(): void {
    this.elementsState.subscribe((elementsState) => {
      this.pageContainer.nativeElement.innerHTML = '';
      this.menuContainer.nativeElement.innerHTML = '';
      elementsState.elements.forEach((el) => {
        this.htmlGeneration.generateHtml(
          el,
          this.pageContainer,
          null,
          elementsState.selectedElement?.element.id,
          this.menuContainer
        );
      });
    });
  }

  clickGobalCallback(event: Event): void {
    const htmlElement = event.target as HTMLElement;
    if (!htmlElement.id) return;

    this.store.dispatch(ElementsActions.setSelectedElement({ htmlElement }));
  }

  clickMenusCallback(event: Event): void {
    const htmlElement = event.target as HTMLElement;
    if (!htmlElement.id) return;

    this.store.dispatch(ElementsActions.setSelectedElement({ htmlElement }));
  }
}

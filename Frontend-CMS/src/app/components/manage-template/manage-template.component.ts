import {
  Component,
  ElementRef,
  OnInit,
  QueryList,
  ViewChildren,
} from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Store } from '@ngrx/store';
import { IElement } from 'src/app/core/interfaces/IElement.interface';
import { IPagination } from 'src/app/core/interfaces/common/IPagination';
import { TemplateService } from 'src/app/core/services/template/template.service';
import { PagesFeature } from 'src/app/core/store/pages/pages.feature';
import { HtmlGeneration } from 'src/app/utils/html-generation';

@Component({
  selector: 'app-manage-template',
  templateUrl: './manage-template.component.html',
  styleUrls: ['./manage-template.component.css'],
  providers: [HtmlGeneration],
})
export class ManageTemplateComponent implements OnInit {
  @ViewChildren('templatesRef') templatesRef!: QueryList<ElementRef>;
  templates?: IPagination<IElement>;
  pageId?: string;

  paginator = {
    pageNumber: 1,
    pageSize: 10,
  };

  isLoading?: string;

  constructor(
    private templateService: TemplateService,
    private htmlGeneration: HtmlGeneration,
    private store: Store,
    public dialogRef: MatDialogRef<ManageTemplateComponent>
  ) {}

  ngOnInit(): void {
    this.loadTemplates();

    this.store
      .select(PagesFeature.selectSelectedPage)
      .subscribe((el) => (this.pageId = el?.id));
  }

  ngAfterViewInit(): void {
    this.templatesRef.changes.subscribe((changes: QueryList<ElementRef>) => {
      changes.toArray().forEach((change) => {
        this.htmlGeneration.generateHtml(
          this.templates?.items.find(
            (el) => el.id === change.nativeElement.id
          )!,
          change
        );
      });
    });
  }

  private loadTemplates(): void {
    this.templateService
      .getAllTemplate(this.paginator.pageNumber, this.paginator.pageSize)
      .subscribe((el) => (this.templates = el));
  }

  onPageChange(event: PageEvent): void {
    this.paginator.pageNumber = event.pageIndex + 1;
    this.paginator.pageSize = event.pageSize;
    this.loadTemplates();
  }

  applyTemplate(templateId: string): void {
    this.isLoading = templateId;
    this.templateService
      .addTemplateToPage(templateId, this.pageId!)
      .subscribe((_) => {
        this.isLoading = undefined;
        this.dialogRef.close();
      });
  }
}

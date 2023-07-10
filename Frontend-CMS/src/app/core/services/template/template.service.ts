import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../../interfaces/common/IPagination';
import { IElement } from '../../interfaces/IElement.interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TemplateService {
  constructor(private httpClient: HttpClient) {}

  getAllTemplate(
    pageNumber: number,
    pageSize: number
  ): Observable<IPagination<IElement>> {
    return this.httpClient.get<IPagination<IElement>>(
      `${environment.api_url}template?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }

  addTemplateToPage(templateId: string, pageId: string): Observable<void> {
    return this.httpClient.post<void>(
      `${environment.api_url}template/${templateId}/page/${pageId}/add`,
      {
        templateId,
        pageId,
      }
    );
  }
}

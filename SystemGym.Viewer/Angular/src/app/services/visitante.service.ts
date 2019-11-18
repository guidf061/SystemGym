import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';

import { Visitante } from '../models/visitante-model';
import { VisitanteSearch } from '../models/visitante-search-model';
import { CustomErrorHandler } from '../core';
import { Paging } from '../models/paging-model';

@Injectable({
  providedIn: 'root'
})
export class VisitanteService {

  visitanteUrl = 'https://localhost:44365/api/Visitante';

  constructor(private http: HttpClient) { }

  search(search: VisitanteSearch): Promise<Paging<Visitante>> {
    let params = new HttpParams();

    if (search.nome !== undefined && search.nome !== null && search.nome !== '') {
      params = params.append('nome', search.nome);
    }

    if (search.cpf !== undefined && search.cpf !== null && search.cpf !== '') {
      params = params.append('cpf', search.cpf);
    }

    if (search.page != undefined && search.page != null) {
      params = params.append('page', search.page.toString());
    }

    if (search.pageSize != undefined && search.pageSize != null) {
      params = params.append('pageSize', search.pageSize.toString());
    }

    if (search.sort !== undefined) {
      params = params.append('sort', search.sort.toString());
      params = params.append('sortDirection', search.sortDirection.toString());
    }

    return this.http.get<Paging<Visitante>>(`${this.visitanteUrl}` + '/Search', { params: params })
      .toPromise()
      .then(data => { return data; })
      .catch(CustomErrorHandler.handleApiError);
  }

  get() {
    return this.http.get<any[]>(`${this.visitanteUrl}`);
  }

  add(visitante: Visitante) {
    let body = visitante
    return this.http.post(`${this.visitanteUrl}`, body)
      .toPromise()
      .then(res => { return res; })
      .catch(CustomErrorHandler.handleApiError)
  }

  update(visitanteId: string, visitante: Visitante) {
    let body = visitante
    return this.http.put(`${this.visitanteUrl}` + '/' + visitanteId, body)
      .toPromise()
      .then(res => { return res; })
      .catch(CustomErrorHandler.handleApiError)
  }

  delete(visitanteId: string) {
    return this.http.delete(`${this.visitanteUrl}` + '/' + visitanteId)
      .toPromise()
      .then(res => { return res; })
      .catch(CustomErrorHandler.handleApiError);
  }
}

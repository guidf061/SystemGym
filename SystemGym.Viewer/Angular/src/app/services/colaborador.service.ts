import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';

import { Colaborador } from '../models/colaborador-model';
import { CustomErrorHandler } from '../core';
import { Paging } from '../models/paging-model';
import { ColaboradorSearch } from '../models/colaborador-search-model';

@Injectable({
  providedIn: 'root'
})
export class ColaboradorService {

  colaboradorUrl = 'https://localhost:44365/api/Colaborador';

  constructor(private http: HttpClient) { }

  search(search: ColaboradorSearch): Promise<Paging<Colaborador>> {
    let params = new HttpParams();

    if (search.nome !== undefined && search.nome !== null && search.nome !== '') {
      params = params.append('nome', search.nome);
    }

    if (search.numeroCtps !== undefined && search.numeroCtps !== null && search.numeroCtps !== '') {
      params = params.append('numeroCtps', search.numeroCtps);
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

    return this.http.get<Paging<Colaborador>>(`${this.colaboradorUrl}` + '/Search', { params: params })
      .toPromise()
      .then(data => { return data; })
      .catch(CustomErrorHandler.handleApiError);
  }

  get() {
    return this.http.get<any[]>(`${this.colaboradorUrl}`);
  }

  add(colaborador: Colaborador) {
    let body = colaborador
    return this.http.post(`${this.colaboradorUrl}`, body)
      .toPromise()
      .then(res => { return res; })
      .catch(CustomErrorHandler.handleApiError)
  }

  update(colaboradorId: string, colaborador: Colaborador) {
    let body = colaborador
    return this.http.put(`${this.colaboradorUrl}` + '/' + colaboradorId, body)
      .toPromise()
      .then(res => { return res; })
      .catch(CustomErrorHandler.handleApiError)
  }

  delete(colaboradorId: string) {
    return this.http.delete(`${this.colaboradorUrl}` + '/' + colaboradorId)
      .toPromise()
      .then(res => { return res; })
      .catch(CustomErrorHandler.handleApiError);
  }
}

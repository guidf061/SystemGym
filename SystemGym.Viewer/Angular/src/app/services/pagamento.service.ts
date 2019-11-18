import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';

import { PagamentoSearch } from '../models/pagamento-search-model';
import { CustomErrorHandler } from '../core';
import { Paging } from '../models/paging-model';
import { Pagamento } from '../models/pagamento-model';

@Injectable({
  providedIn: 'root'
})
export class PagamentoService {

  pagamentoUrl = 'https://localhost:44365/api/Pagamento';

  constructor(private http: HttpClient) { }

  search(search: PagamentoSearch): Promise<Paging<Pagamento>> {
    let params = new HttpParams();

    if (search.alunoId !== undefined && search.alunoId !== null && search.alunoId !== '') {
      params = params.append('alunoId', search.alunoId);
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

    return this.http.get<Paging<Pagamento>>(`${this.pagamentoUrl}` + '/Search', { params: params })
      .toPromise()
      .then(data => { return data; })
      .catch(CustomErrorHandler.handleApiError);
  }

  get() {
    return this.http.get<any[]>(`${this.pagamentoUrl}`);
  }

  add(pagamento: Pagamento) {
    let body = pagamento
    return this.http.post(`${this.pagamentoUrl}`, body)
      .toPromise()
      .then(res => { return res; })
      .catch(CustomErrorHandler.handleApiError)
  }

  update(pagamentoId: string, pagamento: Pagamento) {
    let body = pagamento
    return this.http.put(`${this.pagamentoUrl}` + '/' + pagamentoId, body)
      .toPromise()
      .then(res => { return res; })
      .catch(CustomErrorHandler.handleApiError)
  }

  delete(pagamentoId: string) {
    return this.http.delete(`${this.pagamentoUrl}` + '/' + pagamentoId)
      .toPromise()
      .then(res => { return res; })
      .catch(CustomErrorHandler.handleApiError);
  }
}

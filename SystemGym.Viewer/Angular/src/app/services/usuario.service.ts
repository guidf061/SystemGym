import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';

import { Usuario } from '../models/usuario-model';
import { UsuarioSearch } from '../models/usuario-search-model';
import { CustomErrorHandler } from '../core';
import { Paging } from '../models/paging-model';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  usuarioUrl = 'https://localhost:44365/api/Usuario';

  constructor(private http: HttpClient) { }

  search(search: UsuarioSearch): Promise<Paging<Usuario>> {
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

    return this.http.get<Paging<Usuario>>(`${this.usuarioUrl}` + '/Search', { params: params })
      .toPromise()
      .then(data => { return data; })
      .catch(CustomErrorHandler.handleApiError);
  }

  get() {
    return this.http.get<any[]>(`${this.usuarioUrl}`);
  }

  add(usuario: Usuario) {
    let body = usuario
    return this.http.post(`${this.usuarioUrl}`, body)
      .toPromise()
      .then(res => { return res; })
      .catch(CustomErrorHandler.handleApiError)
  }

  update(usuarioId: string, usuario: Usuario) {
    let body = usuario
    return this.http.put(`${this.usuarioUrl}` + '/' + usuarioId, body)
      .toPromise()
      .then(res => { return res; })
      .catch(CustomErrorHandler.handleApiError)
  }

  delete(usuarioId: string) {
    return this.http.delete(`${this.usuarioUrl}` + '/' + usuarioId)
      .toPromise()
      .then(res => { return res; })
      .catch(CustomErrorHandler.handleApiError);
  }
}

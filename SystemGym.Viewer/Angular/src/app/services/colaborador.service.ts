import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { Promise } from 'q';
import { error } from 'util';
import { Colaborador } from '../models/colaborador-model';

@Injectable({
  providedIn: 'root'
})
export class ColaboradorService {

  url = 'https://localhost:44365/api/Colaborador';

  constructor(private http: HttpClient) { }

  listar() {
    return this.http.get<any[]>(`${this.url}`);
  }

  add(colaborador: Colaborador) {
    let body = colaborador
    return this.http.post(`${this.url}`, body)
      .toPromise()
      .then(res => { return true; })
      .catch(error)
  }

  update(colaboradorId: string, colaborador: Colaborador) {
    let body = colaborador
    return this.http.put(`${this.url}` + '/' + colaboradorId, body)
      .toPromise()
      .then(res => { return true; })
      .catch(error)
  }

  delete(colaboradorId: string) {
    return this.http.delete(`${this.url}` + '/' + colaboradorId)
      .toPromise()
      .then(res => { return true; })
      .catch(error => error);
  }
}

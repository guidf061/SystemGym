import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { Promise } from 'q';
import { error } from 'util';
import { Visitante } from '../models/visitante-model';

@Injectable({
  providedIn: 'root'
})
export class VisitanteService {

  url = 'https://localhost:44365/api/Visitante';

  constructor(private http: HttpClient) { }

  listar() {
    return this.http.get<any[]>(`${this.url}`);
  }

  add(visitante: Visitante) {
    let body = visitante
    return this.http.post(`${this.url}`, body)
      .toPromise()
      .then(res => { return true; })
      .catch(error)
  }

  update(visitanteId: string, visitante: Visitante) {
    let body = visitante
    return this.http.put(`${this.url}` + '/' + visitanteId, body)
      .toPromise()
      .then(res => { return true; })
      .catch(error)
  }

  delete(visitanteId: string) {
    return this.http.delete(`${this.url}` + '/' + visitanteId)
      .toPromise()
      .then(res => { return true; })
      .catch(error => error);
  }
}

import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { Promise } from 'q';
import { error } from 'util';
import { Usuario } from '../models/usuario-model';
import { Aluno } from '../models/aluno-model';

@Injectable({
  providedIn: 'root'
})
export class AlunoService {

  url = 'https://localhost:44365/api/Aluno';

  constructor(private http: HttpClient) { }

  listar() {
    return this.http.get<any[]>(`${this.url}`);
  }

  add(aluno: Aluno) {
    let body = aluno
    return this.http.post(`${this.url}`, body)
      .toPromise()
      .then(res => { return true; })
      .catch(error => error);
  }

  update(alunoId: string, aluno: Aluno) {
    let body = aluno
    return this.http.put(`${this.url}` + '/' + alunoId, body)
      .toPromise()
      .then(res => { return true; })
      .catch(error => error);
  }

  delete(alunoId: string) {
    return this.http.delete(`${this.url}` + '/' + alunoId)
      .toPromise()
      .then(res => { return true; })
      .catch(error => error);
  }
}

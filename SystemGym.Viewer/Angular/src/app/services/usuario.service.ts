import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { Promise } from 'q';
import { error } from 'util';
import { Usuario } from '../models/usuario-model';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  usuarioUrl = 'https://localhost:44365/api/Usuario';

  constructor(private http: HttpClient) { }

  listar() {
    return this.http.get<any[]>(`${this.usuarioUrl}`);
  }

  add(usuario: Usuario) {
    let body = usuario
    return this.http.post(`${this.usuarioUrl}`, body)
      .toPromise()
      .then(res => { return true; })
      .catch(error)
  }

  update(usuarioId: string, usuario: Usuario) {
    let body = usuario
    return this.http.put(`${this.usuarioUrl}` + '/' + usuarioId, body)
      .toPromise()
      .then(res => { return true; })
      .catch(error)
  }

  delete(usuarioId: string) {
    return this.http.delete(`${this.usuarioUrl}` + '/' + usuarioId)
      .toPromise()
      .then(res => { return true; })
      .catch(error => error);
  }
}

import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';

import { AlunoSearch } from '../models/aluno-search-model';
import { CustomErrorHandler } from '../core';
import { Paging } from '../models/paging-model';
import { Aluno } from '../models/aluno-model';
import { MatriculaAluno } from '../models/matricula-aluno-model';

@Injectable({
  providedIn: 'root'
})
export class AlunoService {

  alunoUrl = 'https://localhost:44365/api/Aluno';

  constructor(private http: HttpClient) { }

  search(search: AlunoSearch): Promise<Paging<MatriculaAluno>> {
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

    return this.http.get<Paging<MatriculaAluno>>(`${this.alunoUrl}` + '/Search', { params: params })
      .toPromise()
      .then(data => { return data; })
      .catch(CustomErrorHandler.handleApiError);
  }

  get() {
    return this.http.get<any[]>(`${this.alunoUrl}`);
  }

  add(matriculaAluno: MatriculaAluno) {
    let body = matriculaAluno
    return this.http.post(`${this.alunoUrl}`, body)
      .toPromise()
      .then(res => { return res; })
      .catch(CustomErrorHandler.handleApiError)
  }

  update(matriculaAlunoId: string, matriculaAluno: MatriculaAluno) {
    let body = matriculaAluno
    return this.http.put(`${this.alunoUrl}` + '/' + matriculaAlunoId, body)
      .toPromise()
      .then(res => { return res; })
      .catch(CustomErrorHandler.handleApiError)
  }

  delete(alunoId: string) {
    return this.http.delete(`${this.alunoUrl}` + '/' + alunoId)
      .toPromise()
      .then(res => { return res; })
      .catch(CustomErrorHandler.handleApiError);
  }
}

import { Injectable } from '@angular/core';
import { HttpClient} from "@angular/common/http";

import * as moment from 'moment';
import { CustomErrorHandler } from '../core';
import { Sexo } from '../models/sexo-model';
import { Permissao } from '../models/permissao-model';
import { SituacaoColaborador } from '../models/situacao-colaborador-model';
import { SituacaoMatricula } from '../models/situacao-matricula-model';
import { TipoNotificacao } from '../models/tipo-notificacao-model';
import { Plano } from '../models/plano-model';
import { Funcao } from '../models/funcao-model';
import { Mes } from '../models/mes-model';
import { Ano } from '../models/ano-model';
import { FormaPagamento } from '../models/forma-pagamento-model';

@Injectable()
export class CombosListService {

  private endpointUrl = 'https://localhost:44365/api/CombosList';  // URL to web API

  constructor(private http: HttpClient) {
  }

  getSexo(): Promise<Sexo[]> {
    return this.http.get<Sexo[]>(this.endpointUrl + '/Sexo')
      .toPromise()
      .then(data => { return data; })
      .catch(this.handleError);
  }

  getPermissao(): Promise<Permissao[]> {
    return this.http.get<Permissao[]>(this.endpointUrl + '/Permissao')
      .toPromise()
      .then(data => { return data; })
      .catch(this.handleError);
  }

  getSituacaoColaborador(): Promise<SituacaoColaborador[]> {
    return this.http.get<SituacaoColaborador[]>(this.endpointUrl + '/SituacaoColaborador')
      .toPromise()
      .then(data => { return data; })
      .catch(this.handleError);
  }

  getSituacaoMatricula(): Promise<SituacaoMatricula[]> {
    return this.http.get<SituacaoMatricula[]>(this.endpointUrl + '/SituacaoMatricula')
      .toPromise()
      .then(data => { return data; })
      .catch(this.handleError);
  }

  getTipoNotificacao(): Promise<TipoNotificacao[]> {
    return this.http.get<TipoNotificacao[]>(this.endpointUrl + '/TipoNotificacao')
      .toPromise()
      .then(data => { return data; })
      .catch(this.handleError);
  }

  getPlano(): Promise<Plano[]> {
    return this.http.get<Plano[]>(this.endpointUrl + '/Plano')
      .toPromise()
      .then(data => { return data; })
      .catch(this.handleError);
  }

  getFuncao(): Promise<Funcao[]> {
    return this.http.get<Funcao[]>(this.endpointUrl + '/Funcao')
      .toPromise()
      .then(data => { return data; })
      .catch(this.handleError);
  }

  getMes(): Promise<Mes[]> {
    return this.http.get<Mes[]>(this.endpointUrl + '/Mes')
      .toPromise()
      .then(data => { return data; })
      .catch(this.handleError);
  }

  getAno(): Promise<Ano[]> {
    return this.http.get<Ano[]>(this.endpointUrl + '/Ano')
      .toPromise()
      .then(data => { return data; })
      .catch(this.handleError);
  }

  getFormaPagamento(): Promise<FormaPagamento[]> {
    return this.http.get<FormaPagamento[]>(this.endpointUrl + '/FormaPagamento')
      .toPromise()
      .then(data => { return data; })
      .catch(this.handleError);
  }

  private handleError(error: any) {
    let errMsg = CustomErrorHandler.extractMessage(error);
    return Promise.reject(errMsg);
  }
}

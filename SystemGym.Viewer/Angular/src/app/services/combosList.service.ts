import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';

import * as moment from 'moment';
import { State } from '../models/state-model';
import { City } from '../models/city-model';
import { CitySearch } from '../models/city-search-model';
import { Paging } from '../models/paging-model';
import { CustomErrorHandler } from '../core';
import { Sexo } from '../models/sexo-model';
import { Permissao } from '../models/permissao-model';

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

  

  private handleError(error: any) {
    let errMsg = CustomErrorHandler.extractMessage(error);
    return Promise.reject(errMsg);
  }
}

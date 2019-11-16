import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';

import * as moment from 'moment';
import { State } from '../models/state-model';
import { City } from '../models/city-model';
import { CitySearch } from '../models/city-search-model';
import { Paging } from '../models/paging-model';
import { CustomErrorHandler } from '../core';

@Injectable()
export class AddressService {

  private endpointUrl = 'https://localhost:44365/api/Address';  // URL to web API

  constructor(private http: HttpClient) {
  }

  getState(): Promise<State[]> {
    return this.http.get<State[]>(this.endpointUrl + '/State')
      .toPromise()
      .then(data => { return data; })
      .catch(this.handleError);
  }

  getCity(search: CitySearch): Promise<Paging<City>> {
    let params = new HttpParams();


    if (search.cityId !== undefined && search.cityId !== null) {
      params = params.append('cityId', search.cityId.toString());
    }

    if (search.stateId !== undefined && search.stateId !== null) {
      params = params.append('stateId', search.stateId.toString());
    }

    if (search.name !== undefined && search.name !== null && search.name !== '') {
      params = params.append('name', search.name);
    }
    
    params = params.append('page', search.page.toString());
    params = params.append('pageSize', search.pageSize.toString());

    if (!String.isNullOrEmpty(search.sort)) {
      params = params.append('sort', search.sort.toString());
      params = params.append('sortDirection', search.sortDirection.toString());
    }

    return this.http.get<Paging<City>>(this.endpointUrl + '/City', { params: params })
      .toPromise()
      .then(data => { return data; })
      .catch(this.handleError);
  }

  private handleError(error: any) {
    let errMsg = CustomErrorHandler.extractMessage(error);
    return Promise.reject(errMsg);
  }
}

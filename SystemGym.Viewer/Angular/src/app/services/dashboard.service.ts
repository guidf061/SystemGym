import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';

import { Usuario } from '../models/usuario-model';
import { UsuarioSearch } from '../models/usuario-search-model';
import { CustomErrorHandler } from '../core';
import { Paging } from '../models/paging-model';
import { Dashboard } from '../models/dashboard-model';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  url = 'https://localhost:44365/api/Dashboard';

  constructor(private http: HttpClient) { }

  get(): Promise<Dashboard> {
    return this.http.get<Dashboard>(`${this.url}` + '/Quantity')
      .toPromise()
      .then(data => { return data; })
      .catch(CustomErrorHandler.handleApiError);
  }
}

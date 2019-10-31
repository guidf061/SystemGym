import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from "@angular/common/http";
import { JwtService } from '..';



@Injectable({
  providedIn: 'root'
})
export class LoggedUserService {
  usuarioId: string;
  login: string;
  nome: string;
  roles: string[] = new Array<string>();

  constructor(private http: HttpClient,
    private jwtService: JwtService) {
    this.carregarDadosToken();
  }

  carregarDadosToken() {
    this.readToken();
  }

  private readToken(): void {
    let token = this.jwtService.decodeToken(localStorage.getItem('auth_token'));

    if (token !== undefined && token !== null) {
      this.usuarioId = token.userId;
      this.nome = token.name;

      if (token.roles !== undefined && token.roles !== null) {
        if (token.roles instanceof Array) {
          token.roles.forEach(role => {
            this.roles.push(role);
          });
        } else {
          this.roles.push(token.roles);
        }
      }
    }
  }
}

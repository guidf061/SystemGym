import { Component, OnInit, Pipe } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as moment from 'moment';
import { Usuario } from '../models/usuario-model';
import { UsuarioService } from '../services/usuario.service';
import { UsuarioFormService } from './usuario-form/usuario-form.service';


@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css']
})
export class UsuarioComponent implements OnInit {

  form: FormGroup;

  usuario: Usuario;

  data: Usuario[];
  dataSource = this.data;

  formSubmited: boolean;

  constructor(private usuarioService: UsuarioService,
    private usuarioForService: UsuarioFormService,
    private fb: FormBuilder) { }

  displayedColumns: string[] = ['pessoa.nome', 'pessoa.cpf', 'userName', 'pessoa.email', 'edit' ,'del'];

  ngOnInit() {
    this.listar();

  //  this.createFormGroup();
  }


  listar() {
    this.usuarioService.listar().subscribe(rows => this.data = rows);
  }


  editClick(usuario: Usuario) {
    this.usuarioForService.showDialog(usuario).subscribe();
  }

  deleteClick(usuario: Usuario) {
    this.usuarioService.delete(usuario.usuarioId);
  }

  createClick(): void {
    this.usuarioForService.showDialog(null).subscribe();
  }
}

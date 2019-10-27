import { Component, OnInit, Pipe } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as moment from 'moment';

import { UsuarioFormService } from './usuario-form/usuario-form.service';
import { Usuario } from '../../models/usuario-model';
import { UsuarioService } from '../../services/usuario.service';
import { ConfirmService, LoaderService } from '../../core';
import { MatSnackBar } from '@angular/material';


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
    private confirmService: ConfirmService,
    private snackBar: MatSnackBar,
    private loaderService: LoaderService,
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
    this.confirmService.activate()
      .then(confirmed => {
        if (confirmed) {
          this.loaderService.show();
          this.usuarioService.delete(usuario.usuarioId)
            .then(deleted => {
              this.listar();
              this.snackBar.open('Registro excluído com sucesso!.', 'Fechar', {
                duration: 5000
              });
            },
              error => {
                this.snackBar.open(<string>error, 'Fechar', {
                  duration: 50000
                });
                this.loaderService.hide();
              });
        }
      });
  }

  createClick(): void {
    this.usuarioForService.showDialog(null).subscribe();
  }
}

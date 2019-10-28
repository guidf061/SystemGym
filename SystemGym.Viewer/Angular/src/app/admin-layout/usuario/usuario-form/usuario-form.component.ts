
import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA } from '@angular/material'
import { UsuarioService } from '../../../services/usuario.service';
import { LoaderService } from '../../../services/loader.service';
import { Usuario } from '../../../models/usuario-model';
import { Pessoa } from '../../../models/pessoa-model';


@Component({
  selector: 'app-usuario-form',
  templateUrl: './usuario-form.component.html',
  styleUrls: ['./usuario-form.component.scss']
})
export class UsuarioFormComponent implements OnInit {
  form: FormGroup;
  title: string = 'Cadastrar';
  usuario: Usuario;

  formSubmited: boolean;

  constructor(
    private usuarioService: UsuarioService,
    private loaderService: LoaderService,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<UsuarioFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Usuario,
    private fb: FormBuilder) {
    this.usuario = data;
  }

  ngOnInit() {

    this.createFormGroup();

    if (this.usuario !== undefined && this.usuario !== null) {
      this.title = 'Alterar';

      this.setFormGroup();
    }
    else {
      this.usuario = new Usuario();
    }
  }

  saveClick(): void {
    this.formSubmited = true;

    if (this.form.valid) {
      this.loaderService.show();

      let usuario: Usuario = this.prepareSave();

      if (this.usuario.usuarioId === null || this.usuario.usuarioId === undefined) {
      this.usuarioService.add(usuario);
      }
      else {
      this.usuarioService.update(this.usuario.usuarioId, usuario)
      }

      this.snackBar.open('Dados salvos com sucesso!.', 'Fechar',)

      this.closeDialog(true);


      error => {

        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      }
    }
  }

  closeDialog(update: boolean) {
    this.dialogRef.close(update);
  }

  private setFormGroup(): void {
    this.form.setValue({
      userName: this.usuario.userName,
      password: this.usuario.password,
      nome: this.usuario.pessoa.nome,
      email: this.usuario.pessoa.email,
      cpf: this.usuario.pessoa.cpf,
      telefoneCelular: this.usuario.pessoa.telefoneCelular,
      telefoneCasa: this.usuario.pessoa.telefoneCasa,
      sexoId: this.usuario.pessoa.sexoId,
      tipoId: this.usuario.pessoa.tipoId,
    });
  }

   private createFormGroup(): void {
    this.form = this.fb.group({
      userName: ['', { validators: Validators.required }],
      password: ['', { validators: Validators.required }],
      nome: ['', { validators: Validators.required }],
      email: ['', { validators: Validators.required }],
      cpf: ['', { validators: Validators.required }],
      telefoneCelular: ['', { validators: Validators.required }],
      telefoneCasa: ['', { validators: Validators.required }],
      sexoId: ['', { validators: Validators.required }],
      tipoId: ['', { validators: Validators.required }],
    });
   }

  private prepareSave(): Usuario {
    const formModel = this.form.value;
    let usuario: Usuario = new Usuario();

    usuario.userName = formModel.userName as string;
    usuario.password = formModel.password as string;
    usuario.pessoa = new Pessoa();
    usuario.pessoa.nome = formModel.nome as string;
    usuario.pessoa.email = formModel.email as string;
    usuario.pessoa.cpf = formModel.cpf as string;
    usuario.pessoa.telefoneCasa = formModel.telefoneCasa as string;
    usuario.pessoa.telefoneCelular = formModel.telefoneCelular as string;
    usuario.pessoa.sexoId = formModel.sexoId as number;
    usuario.pessoa.tipoId = formModel.tipoId as number;


    return usuario;
  }
}



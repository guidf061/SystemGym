
import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA, DateAdapter, MAT_DATE_FORMATS } from '@angular/material'
import { UsuarioService } from '../../../services/usuario.service';

import { Usuario } from '../../../models/usuario-model';
import { Pessoa } from '../../../models/pessoa-model';
import { LoaderService } from '../../../core';
import { State } from '../../../models/state-model';
import { AddressService } from '../../../services/address.service';
import { City } from '../../../models/city-model';
import * as moment from 'moment';
import { CustomDateAdapter } from '../../../custom-date-adapter';
import { Sexo } from '../../../models/sexo-model';
import { Permissao } from '../../../models/permissao-model';
import { CombosListService } from '../../../services/combosList.service';

@Component({
  selector: 'app-usuario-form',
  templateUrl: './usuario-form.component.html',
  styleUrls: ['./usuario-form.component.scss'],
  providers: [
    {
      provide: DateAdapter, useClass: CustomDateAdapter
    }
  ]
})
export class UsuarioFormComponent implements OnInit {
  form: FormGroup;
  title: string = 'Cadastrar';
  usuario: Usuario;
  states: State[];
  sexos: Sexo[];
  permissaos: Permissao[];
  stateSelec: boolean = false;

  formSubmited: boolean;

  constructor(
    private usuarioService: UsuarioService,
    private loaderService: LoaderService,
    private combosListService: CombosListService,
    private addressService: AddressService,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<UsuarioFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Usuario,
    private fb: FormBuilder) {
    this.usuario = data;
  }

  selectedCity: City;

  autoCompleteUrl = 'https://localhost:44365/api/Address' + '/City?Name=';
  keyword: string;

  @HostListener('window:keydown', ['$event'])
  keydownHandler(event) {
    let target = event.target || event.srcElement;
    if (event.keyCode === 27) {
      this.closeDialog(false);
    }
  }

  ngOnInit() {

    this.addressService.getState().then(rows => {
      this.states = rows;
      this.loaderService.hide();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });

    this.combosListService.getSexo().then(rows => {
      this.sexos = rows;
      this.loaderService.hide();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });

    this.combosListService.getPermissao().then(rows => {
      this.permissaos = rows;
      this.loaderService.hide();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });

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

      let save: Promise<any>;

      if (this.usuario.usuarioId === null || this.usuario.usuarioId === undefined) {
        save = this.usuarioService.add(usuario);
      } else {
        save = this.usuarioService.update(this.usuario.usuarioId, usuario);
      }

      save.then(reseted => {
        this.loaderService.hide();
        this.snackBar.open('Dados salvos com sucesso!.', 'Fechar', {
          duration: 5000
        });
        this.closeDialog(true);
      },
        error => {
          this.loaderService.hide();
          this.snackBar.open(error, 'Fechar', {
            duration: 10000
          });
        });
    }
  }

  closeDialog(update: boolean) {
    this.dialogRef.close(update);
  }

  citySelected(city: City) {
    if (city !== null) {
      this.selectedCity = city;
      if (!this.stateSelec) {
        this.form.controls['stateId'].setValue(city.stateId);
      }
    }
    return this.selectedCity;
  }

  stateSelected() {
    this.autoCompleteUrl = 'https://localhost:44365/api/Address' + '/City?StateId=' + this.form.controls['stateId'].value + '&Name=';
    this.stateSelec == true;
  }

  private setFormGroup(): void {
    this.form.setValue({
      userName: this.usuario.userName,
      password: this.usuario.password,
      nome: this.usuario.pessoa.nome,
      email: this.usuario.pessoa.email,
      cpf: this.usuario.pessoa.cpf,
      sexoId: this.usuario.pessoa.sexoId,
      endereco: this.usuario.pessoa.endereco,
      permissaoId: this.usuario.pessoa.permissaoId,
      telefoneCelular: this.usuario.pessoa.telefoneCelular,
      telefoneCasa: this.usuario.pessoa.telefoneCasa,
      dataNascimento: this.usuario.pessoa.dataNascimento,
      stateId: (this.usuario.pessoa.city == undefined && this.usuario.pessoa.city == null ? '' : this.usuario.pessoa.city.stateId),

    });

    if (this.usuario.pessoa.city !== undefined && this.usuario.pessoa.city !== null) {
      this.keyword = this.usuario.pessoa.city.name;
      this.selectedCity = this.usuario.pessoa.city;
    }
  }

   private createFormGroup(): void {
    this.form = this.fb.group({
      userName: ['', { validators: Validators.required }],
      password: ['', { validators: Validators.required }],
      nome: ['', { validators: Validators.required }],
      email: ['', { validators: Validators.required }],
      cpf: ['', { validators: Validators.required }],
      sexoId: ['', { validators: Validators.required }],
      endereco: ['', { validators: Validators.required }],
      permissaoId: ['', { validators: Validators.required }],
      telefoneCelular: ['', { validators: Validators.required }],
      telefoneCasa: ['', { validators: Validators.required }],
      dataNascimento: ['', { validators: Validators.required }],
      stateId: '',
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

    usuario.pessoa.permissaoId = formModel.permissaoId as number;

    usuario.pessoa.endereco = formModel.endereco as string;

    usuario.pessoa.stateId = this.form.controls['stateId'].value;

    usuario.pessoa.dataNascimento = moment(formModel.dataNascimento, 'DD/MM/YYYY ').toDate();

    if (this.selectedCity !== undefined && this.selectedCity !== null) {
      usuario.pessoa.cityId = this.selectedCity.cityId;

    }
    
    return usuario;
  }
}



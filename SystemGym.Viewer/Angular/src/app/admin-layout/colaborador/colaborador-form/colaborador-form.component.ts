
import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA } from '@angular/material'
import { ColaboradorService } from '../../../services/colaborador.service';

import { Colaborador } from '../../../models/colaborador-model';
import { Pessoa } from '../../../models/pessoa-model';
import { LoaderService } from '../../../core';
import { State } from '../../../models/state-model';
import { AddressService } from '../../../services/address.service';
import { City } from '../../../models/city-model';
import * as moment from 'moment';
import { CombosListService } from '../../../services/combosList.service';
import { Funcao } from '../../../models/funcao-model';
import { SituacaoColaborador } from '../../../models/situacao-colaborador-model';
import { Sexo } from '../../../models/sexo-model';


@Component({
  selector: 'app-colaborador-form',
  templateUrl: './colaborador-form.component.html',
  styleUrls: ['./colaborador-form.component.scss']
})
export class ColaboradorFormComponent implements OnInit {
  form: FormGroup;
  title: string = 'Cadastrar';
  colaborador: Colaborador;
  funcoes: Funcao[];
  states: State[];
  sexos: Sexo[];
  situacaoColaboradores: SituacaoColaborador[];
  stateSelec: boolean = false;

  formSubmited: boolean;

  constructor(
    private colaboradorService: ColaboradorService,
    private loaderService: LoaderService,
    private addressService: AddressService,
    private combosListService: CombosListService,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<ColaboradorFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Colaborador,
    private fb: FormBuilder) {
    this.colaborador = data;
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

    this.combosListService.getFuncao().then(rows => {
      this.funcoes = rows;
      this.loaderService.hide();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });


    this.combosListService.getSituacaoColaborador().then(rows => {
      this.situacaoColaboradores = rows;
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

    this.createFormGroup();

    if (this.colaborador !== undefined && this.colaborador !== null) {
      this.title = 'Alterar';

      this.setFormGroup();
    }
    else {
      this.colaborador = new Colaborador();
    }
  }

  saveClick(): void {
    this.formSubmited = true;

    if (this.form.valid) {
      this.loaderService.show();

      let colaborador: Colaborador = this.prepareSave();

      if (this.colaborador.colaboradorId === null || this.colaborador.colaboradorId === undefined) {
        this.colaboradorService.add(colaborador);
      }
      else {
        this.colaboradorService.update(this.colaborador.colaboradorId, colaborador)
      }

      this.snackBar.open('Dados salvos com sucesso!.', 'Fechar')

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
      funcaoId: this.colaborador.funcaoId,
      situacaoColaboradorId: this.colaborador.situacaoColaboradorId,
      numeroSerieCtps: this.colaborador.numeroSerieCtps,
      numeroCtps: this.colaborador.numeroCtps,
      numeroPisPasep: this.colaborador.numeroPisPasep,
      docIdentidade: this.colaborador.docIdentidade,
      nome: this.colaborador.pessoa.nome,
      email: this.colaborador.pessoa.email,
      cpf: this.colaborador.pessoa.cpf,
      sexoId: this.colaborador.pessoa.sexoId,
      endereco: this.colaborador.pessoa.endereco,
      telefoneCelular: this.colaborador.pessoa.telefoneCelular,
      telefoneCasa: this.colaborador.pessoa.telefoneCasa,
      dataNascimento: this.colaborador.pessoa.dataNascimento,
      stateId: (this.colaborador.pessoa.city == undefined && this.colaborador.pessoa.city == null ? '' : this.colaborador.pessoa.city.stateId),

    });

    if (this.colaborador.pessoa.city !== undefined && this.colaborador.pessoa.city !== null) {
      this.keyword = this.colaborador.pessoa.city.name;
      this.selectedCity = this.colaborador.pessoa.city;
    }
  }

  private createFormGroup(): void {
    this.form = this.fb.group({
      funcaoId: ['', { validators: Validators.required }],
      situacaoColaboradorId: ['', { validators: Validators.required }],
      numeroSerieCtps: '',
      numeroCtps: '',
      numeroPisPasep: '',
      docIdentidade: ['', { validators: Validators.required }],
      nome: ['', { validators: Validators.required }],
      email: ['', { validators: Validators.required }],
      cpf: ['', { validators: Validators.required }],
      sexoId: ['', { validators: Validators.required }],
      endereco: ['', { validators: Validators.required }],
      telefoneCelular: ['', { validators: Validators.required }],
      telefoneCasa: ['', { validators: Validators.required }],
      dataNascimento: ['', { validators: Validators.required }],
      stateId: '',
    });
  }

  private prepareSave(): Colaborador {
    const formModel = this.form.value;
    let colaborador: Colaborador = new Colaborador();

    colaborador.funcaoId = formModel.funcaoId as number;

    colaborador.situacaoColaboradorId = formModel.situacaoColaboradorId as number;

    colaborador.numeroSerieCtps = formModel.numeroSerieCtps as string;

    colaborador.numeroCtps = formModel.numeroCtps as string;

    colaborador.numeroPisPasep = formModel.numeroPisPasep as string;

    colaborador.docIdentidade = formModel.docIdentidade as string;

    colaborador.pessoa = new Pessoa();

    colaborador.pessoa.nome = formModel.nome as string;

    colaborador.pessoa.email = formModel.email as string;

    colaborador.pessoa.cpf = formModel.cpf as string;

    colaborador.pessoa.telefoneCasa = formModel.telefoneCasa as string;

    colaborador.pessoa.telefoneCelular = formModel.telefoneCelular as string;

    colaborador.pessoa.sexoId = formModel.sexoId as number;

    colaborador.pessoa.permissaoId = formModel.permissaoId as number;

    colaborador.pessoa.endereco = formModel.endereco as string;

    colaborador.pessoa.stateId = this.form.controls['stateId'].value;

    colaborador.pessoa.dataNascimento = moment(formModel.dataNascimento, 'DD/MM/YYYY ').toDate();

    if (this.selectedCity !== undefined && this.selectedCity !== null) {
      colaborador.pessoa.cityId = this.selectedCity.cityId;
    }

    return colaborador;
  }
}



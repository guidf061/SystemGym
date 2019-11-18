
import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA } from '@angular/material'
import { VisitanteService } from '../../../services/visitante.service';

import { Visitante } from '../../../models/visitante-model';
import { Pessoa } from '../../../models/pessoa-model';
import { LoaderService } from '../../../core';
import { State } from '../../../models/state-model';
import { AddressService } from '../../../services/address.service';
import { City } from '../../../models/city-model';
import * as moment from 'moment';
import { visitAll } from '@angular/compiler';


@Component({
  selector: 'app-visitante-form',
  templateUrl: './visitante-form.component.html',
  styleUrls: ['./visitante-form.component.scss']
})
export class VisitanteFormComponent implements OnInit {
  form: FormGroup;
  title: string = 'Cadastrar';
  visitante: Visitante;
  states: State[];
  stateSelec: boolean = false;

  formSubmited: boolean;

  constructor(
    private visitanteService: VisitanteService,
    private loaderService: LoaderService,
    private addressService: AddressService,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<VisitanteFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Visitante,
    private fb: FormBuilder) {
    this.visitante = data;
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

    this.createFormGroup();

    if (this.visitante !== undefined && this.visitante !== null) {
      this.title = 'Alterar';

      this.setFormGroup();
    }
    else {
      this.visitante = new Visitante();
    }
  }

  saveClick(): void {
    this.formSubmited = true;

    if (this.form.valid) {
      this.loaderService.show();

      let visitante: Visitante = this.prepareSave();

      if (this.visitante.visitanteId === null || this.visitante.visitanteId === undefined) {
        this.visitanteService.add(visitante);
      }
      else {
        this.visitanteService.update(this.visitante.visitanteId, visitante)
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
     
      nome: this.visitante.pessoa.nome,
      email: this.visitante.pessoa.email,
      cpf: this.visitante.pessoa.cpf,
      sexoId: this.visitante.pessoa.sexoId,
      endereco: this.visitante.pessoa.endereco,
      telefoneCelular: this.visitante.pessoa.telefoneCelular,
      telefoneCasa: this.visitante.pessoa.telefoneCasa,
      dataNascimento: this.visitante.pessoa.dataNascimento,
      stateId: (this.visitante.pessoa.city == undefined && this.visitante.pessoa.city == null ? '' : this.visitante.pessoa.city.stateId),
      docIdentidade: this.visitante.docIdentidade,
      visitaData: this.visitante.visitaData,
    });

    if (this.visitante.pessoa.city !== undefined && this.visitante.pessoa.city !== null) {
      this.keyword = this.visitante.pessoa.city.name;
      this.selectedCity = this.visitante.pessoa.city;
    }
  }

  private createFormGroup(): void {
    this.form = this.fb.group({
      
      nome: ['', { validators: Validators.required }],
      email: ['', { validators: Validators.required }],
      cpf: ['', { validators: Validators.required }],
      sexoId: ['', { validators: Validators.required }],
      endereco: ['', { validators: Validators.required }],
      telefoneCelular: ['', { validators: Validators.required }],
      telefoneCasa: ['', { validators: Validators.required }],
      dataNascimento: ['', { validators: Validators.required }],
      stateId: ['', { validators: Validators.required }],
      docIdentidade: ['', { validators: Validators.required }],
      visitaData: ['', { validators: Validators.required }],
    });
  }

  private prepareSave(): Visitante {
    const formModel = this.form.value;
    let visitante: Visitante = new Visitante();

    visitante.docIdentidade = formModel.docIdentidade as string;

    visitante.visitaData = moment(formModel.visitaData, 'DD/MM/YYYY ').toDate();

    visitante.pessoa = new Pessoa();

    visitante.pessoa.nome = formModel.nome as string;

    visitante.pessoa.email = formModel.email as string;

    visitante.pessoa.cpf = formModel.cpf as string;

    visitante.pessoa.telefoneCasa = formModel.telefoneCasa as string;

    visitante.pessoa.telefoneCelular = formModel.telefoneCelular as string;

    visitante.pessoa.sexoId = formModel.sexoId as number;

    visitante.pessoa.endereco = formModel.endereco as string;

    visitante.pessoa.stateId = this.form.controls['stateId'].value;

    visitante.pessoa.dataNascimento = moment(formModel.dataNascimento, 'DD/MM/YYYY ').toDate();

    if (this.selectedCity !== undefined && this.selectedCity !== null) {
      visitante.pessoa.cityId = this.selectedCity.cityId;
    }

    return visitante;
  }
}



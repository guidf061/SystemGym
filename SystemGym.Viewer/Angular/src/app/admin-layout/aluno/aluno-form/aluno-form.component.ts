
import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA } from '@angular/material'
import { AlunoService } from '../../../services/aluno.service';
import { Pessoa } from '../../../models/pessoa-model';
import { LoaderService } from '../../../core';
import { State } from '../../../models/state-model';
import { AddressService } from '../../../services/address.service';
import { City } from '../../../models/city-model';
import * as moment from 'moment';
import { Aluno } from '../../../models/aluno-model';


@Component({
  selector: 'app-aluno-form',
  templateUrl: './aluno-form.component.html',
  styleUrls: ['./aluno-form.component.scss']
})
export class AlunoFormComponent implements OnInit {
  form: FormGroup;
  title: string = 'Matricular Aluno';
  aluno: Aluno;
  states: State[];
  stateSelec: boolean = false;

  formSubmited: boolean;

  constructor(
    private alunoService: AlunoService,
    private loaderService: LoaderService,
    private addressService: AddressService,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<AlunoFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Aluno,
    private fb: FormBuilder) {
    this.aluno = data;
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

    if (this.aluno !== undefined && this.aluno !== null) {
      this.title = 'Alterar Matrícula';

      this.setFormGroup();
    }
    else {
      this.aluno = new Aluno();
    }
  }

  saveClick(): void {
    this.formSubmited = true;

    if (this.form.valid) {
      this.loaderService.show();

      let aluno: Aluno = this.prepareSave();

      if (this.aluno.alunoId === null || this.aluno.alunoId === undefined) {
        this.alunoService.add(aluno);
      }
      else {
        this.alunoService.update(this.aluno.alunoId, aluno)
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
      nome: this.aluno.pessoa.nome,
      email: this.aluno.pessoa.email,
      cpf: this.aluno.pessoa.cpf,
      sexoId: this.aluno.pessoa.sexoId,
      endereco: this.aluno.pessoa.endereco,
      tipoId: this.aluno.pessoa.tipoId,
      telefoneCelular: this.aluno.pessoa.telefoneCelular,
      telefoneCasa: this.aluno.pessoa.telefoneCasa,
      dataNascimento: this.aluno.pessoa.dataNascimento,
      stateId: (this.aluno.pessoa.city == undefined && this.aluno.pessoa.city == null ? '' : this.aluno.pessoa.city.stateId),
      numeroWhatsapp: this.aluno.numeroWhatsapp,
    });

    if (this.aluno.pessoa.city !== undefined && this.aluno.pessoa.city !== null) {
      this.keyword = this.aluno.pessoa.city.name;
      this.selectedCity = this.aluno.pessoa.city;
    }
  }

  private createFormGroup(): void {
    this.form = this.fb.group({
     
      nome: ['', { validators: Validators.required }],
      email: ['', { validators: Validators.required }],
      numeroCartao: ['', { validators: Validators.required }],
      cpf: ['', { validators: Validators.required }],
      sexoId: ['', { validators: Validators.required }],
      endereco: ['', { validators: Validators.required }],
      tipoId: ['', { validators: Validators.required }],
      telefoneCelular: ['', { validators: Validators.required }],
      telefoneCasa: ['', { validators: Validators.required }],
      dataNascimento: ['', { validators: Validators.required }],
      stateId: '',
      situacaoMatriculaId: ['', { validators: Validators.required }],
      ativo: false,
      cancelamentoDate: '',
      numeroWhatsapp:'',
    });
  }

  private prepareSave(): Aluno {
    const formModel = this.form.value;
    let aluno: Aluno = new Aluno();

    aluno.ativo = formModel.ativo as boolean;
    aluno.situacaoMatriculaId = formModel.situacaoMatriculaId as number;
    aluno.cancelamentoDate = moment(formModel.dataNascimento, 'DD/MM/YYYY ').toDate();
    aluno.numeroCartao = formModel.numeroCartao as string;
    aluno.numeroWhatsapp = formModel.numeroWhatsapp as string;
    aluno.pessoa = new Pessoa();
    aluno.pessoa.nome = formModel.nome as string;
    aluno.pessoa.email = formModel.email as string;
    aluno.pessoa.cpf = formModel.cpf as string;
    aluno.pessoa.telefoneCasa = formModel.telefoneCasa as string;
    aluno.pessoa.telefoneCelular = formModel.telefoneCelular as string;
    aluno.pessoa.sexoId = formModel.sexoId as number;
    aluno.pessoa.tipoId = formModel.tipoId as number;
    aluno.pessoa.endereco = formModel.endereco as string;
    aluno.pessoa.stateId = this.form.controls['stateId'].value;
    aluno.pessoa.dataNascimento = moment(formModel.dataNascimento, 'DD/MM/YYYY ').toDate();
    if (this.selectedCity !== undefined && this.selectedCity !== null) {
      aluno.pessoa.cityId = this.selectedCity.cityId;
    }

    return aluno;
  }
}



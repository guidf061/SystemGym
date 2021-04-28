
import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA, DateAdapter } from '@angular/material'
import { AlunoService } from '../../../services/aluno.service';
import { Pessoa } from '../../../models/pessoa-model';
import { LoaderService } from '../../../core';
import { State } from '../../../models/state-model';
import { AddressService } from '../../../services/address.service';
import { City } from '../../../models/city-model';
import * as moment from 'moment';
import { Aluno } from '../../../models/aluno-model';
import { CustomDateAdapter } from '../../../custom-date-adapter';
import { Sexo } from '../../../models/sexo-model';
import { CombosListService } from '../../../services/combosList.service';
import { SituacaoMatricula } from '../../../models/situacao-matricula-model';
import { Plano } from '../../../models/plano-model';
import { MatriculaAluno } from '../../../models/matricula-aluno-model';


@Component({
  selector: 'app-matricula-form',
  templateUrl: './matricula-form.component.html',
  styleUrls: ['./matricula-form.component.scss'],
  providers: [
    {
      provide: DateAdapter, useClass: CustomDateAdapter
    }
  ]
})
export class MatriculaFormComponent implements OnInit {
  form: FormGroup;
  title: string = 'Matricular Aluno';
  matriculaAluno: MatriculaAluno;
  states: State[];
  sexos: Sexo[];
  planos: Plano[];
  situacaoMatriculas: SituacaoMatricula[];
  stateSelec: boolean = false;

  formSubmited: boolean;

  constructor(
    private alunoService: AlunoService,
    private loaderService: LoaderService,
    private addressService: AddressService,
    private snackBar: MatSnackBar,
    private combosListService: CombosListService,
    private dialogRef: MatDialogRef<MatriculaFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: MatriculaAluno,
    private fb: FormBuilder) {
    this.matriculaAluno = data;
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

    this.combosListService.getSituacaoMatricula().then(rows => {
      this.situacaoMatriculas = rows;
      this.loaderService.hide();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });

    this.createFormGroup();

    if (this.matriculaAluno !== undefined && this.matriculaAluno !== null) {
      this.title = 'Alterar Matrícula';

      this.setFormGroup();
    }
    else {
      this.matriculaAluno = new MatriculaAluno();
    }
  }

  saveClick(): void {
    this.formSubmited = true;

    if (this.form.valid) {
      this.loaderService.show();

      let matriculaAluno: MatriculaAluno = this.prepareSave();

      let save: Promise<any>;

      if (this.matriculaAluno.matriculaAlunoId === null || this.matriculaAluno.matriculaAlunoId === undefined) {
        save = this.alunoService.add(matriculaAluno);
      } else {
        save = this.alunoService.update(this.matriculaAluno.matriculaAlunoId, matriculaAluno);
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
      // aluno
      numeroCartao: this.matriculaAluno.aluno.numeroCartao,
      numeroWhatsapp: this.matriculaAluno.aluno.numeroWhatsapp,

      //matricula
      situacaoMatriculaId: this.matriculaAluno.situacaoMatriculaId,
      ativo: this.matriculaAluno.ativo,
      
      //pessoa
      nome: this.matriculaAluno.aluno.pessoa.nome,
      email: this.matriculaAluno.aluno.pessoa.email,
      cpf: this.matriculaAluno.aluno.pessoa.cpf,
      sexoId: this.matriculaAluno.aluno.pessoa.sexoId,
      endereco: this.matriculaAluno.aluno.pessoa.endereco,
      telefoneCelular: this.matriculaAluno.aluno.pessoa.telefoneCelular,
      telefoneCasa: this.matriculaAluno.aluno.pessoa.telefoneCasa,
      dataNascimento: this.matriculaAluno.aluno.pessoa.dataNascimento,
      stateId: (this.matriculaAluno.aluno.pessoa.city == undefined && this.matriculaAluno.aluno.pessoa.city == null ? '' : this.matriculaAluno.aluno.pessoa.city.stateId),
    });

    if (this.matriculaAluno.aluno.pessoa.city !== undefined && this.matriculaAluno.aluno.pessoa.city !== null) {
      this.keyword = this.matriculaAluno.aluno.pessoa.city.name;
      this.selectedCity = this.matriculaAluno.aluno.pessoa.city;
    }
  }

  private createFormGroup(): void {
    this.form = this.fb.group({
      // aluno
      numeroWhatsapp: ['', { validators: Validators.required }],
      numeroCartao: ['', { validators: Validators.required }],

      //matricula
      situacaoMatriculaId: '',
      ativo: false,

      //pessoa
      nome: ['', { validators: Validators.required }],
      email: ['', { validators: Validators.required }],
      cpf: ['', { validators: Validators.required }],
      sexoId: ['', { validators: Validators.required }],
      endereco: ['', { validators: Validators.required }],
      telefoneCelular: ['', { validators: Validators.required }],
      telefoneCasa: ['', { validators: Validators.required }],
      dataNascimento: ['', { validators: Validators.required }],
      stateId: ['', { validators: Validators.required }],
     
    });
  }

  private prepareSave(): MatriculaAluno {
    const formModel = this.form.value;
    let matriculaAluno: MatriculaAluno = new MatriculaAluno();

    //matricula

    matriculaAluno.ativo = formModel.ativo as boolean,

    matriculaAluno.cancelamentoDate = moment(formModel.cancelamentoDate, 'DD/MM/YYYY ').toDate();

    matriculaAluno.situacaoMatriculaId = formModel.situacaoMatriculaId as number;

    if (this.matriculaAluno !== undefined && this.matriculaAluno !== null){
      matriculaAluno.alunoId = this.matriculaAluno.alunoId;
    }

    //aluno

    matriculaAluno.aluno = new Aluno();

    if (this.matriculaAluno.aluno !== undefined && this.matriculaAluno.aluno !== null) {
      matriculaAluno.aluno.pessoaId = this.matriculaAluno.aluno.pessoaId;
    }

    matriculaAluno.aluno.numeroCartao = formModel.numeroCartao as string;

    matriculaAluno.aluno.numeroWhatsapp = formModel.numeroWhatsapp as string;


    //pessoa

    matriculaAluno.aluno.pessoa = new Pessoa();

    matriculaAluno.aluno.pessoa.nome = formModel.nome as string;

    matriculaAluno.aluno.pessoa.email = formModel.email as string;

    matriculaAluno.aluno.pessoa.cpf = formModel.cpf as string;

    matriculaAluno.aluno.pessoa.telefoneCasa = formModel.telefoneCasa as string;

    matriculaAluno.aluno.pessoa.telefoneCelular = formModel.telefoneCelular as string;

    matriculaAluno.aluno.pessoa.sexoId = formModel.sexoId as number;

    matriculaAluno.aluno.pessoa.permissaoId = formModel.permissaoId as number;

    matriculaAluno.aluno.pessoa.endereco = formModel.endereco as string;

    matriculaAluno.aluno.pessoa.stateId = this.form.controls['stateId'].value;

    matriculaAluno.aluno.pessoa.countryId = 1 as number;


    matriculaAluno.aluno.pessoa.dataNascimento = moment(formModel.dataNascimento, 'DD/MM/YYYY ').toDate();

    if (this.selectedCity !== undefined && this.selectedCity !== null) {
      matriculaAluno.aluno.pessoa.cityId = this.selectedCity.cityId;

    }

    return matriculaAluno;
  }
}



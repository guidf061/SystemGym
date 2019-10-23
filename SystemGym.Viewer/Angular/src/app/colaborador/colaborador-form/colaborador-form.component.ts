
import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA } from '@angular/material'
import { Pessoa } from '../../models/pessoa-model';
import { LoaderService } from '../../services/loader.service';
import { Visitante } from '../../models/visitante-model';
import { VisitanteService } from '../../services/visitante.service';
import { Colaborador } from '../../models/colaborador-model';
import { ColaboradorService } from '../../services/colaborador.service';

@Component({
  selector: 'app-colaborador-form',
  templateUrl: './colaborador-form.component.html',
  styleUrls: ['./colaborador-form.component.css']
})
export class ColaboradorFormComponent implements OnInit {
  form: FormGroup;
  title: string = 'Cadastrar';
  colaborador: Colaborador;

  formSubmited: boolean;

  constructor(
    private colaboradorService: ColaboradorService,
    private loaderService: LoaderService,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<ColaboradorFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Colaborador,
    private fb: FormBuilder) {
    this.colaborador = data;
  }

  ngOnInit() {

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
      duration: 5000

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
      nome: this.colaborador.pessoa.nome,
      email: this.colaborador.pessoa.email,
      cpf: this.colaborador.pessoa.cpf,
      telefoneCelular: this.colaborador.pessoa.telefoneCelular,
      telefoneCasa: this.colaborador.pessoa.telefoneCasa,
      sexoId: this.colaborador.pessoa.sexoId,
      tipoId: this.colaborador.pessoa.tipoId,
      funcaoId: this.colaborador.funcaoId,
      situacaoColaboradorId: this.colaborador.situacaoColaboradorId,
    });
  }

   private createFormGroup(): void {
    this.form = this.fb.group({
      nome:'',
      email: '',
      cpf: '',
      telefoneCelular: '',
      telefoneCasa: '',
      sexoId: '',
      tipoId: '',
      funcaoId: '',
      situacaoColaboradorId:'',

    });
   }

  private prepareSave(): Colaborador {
    const formModel = this.form.value;
    let colaborador: Colaborador = new Colaborador();

    colaborador.funcaoId = formModel.funcaoId as number;
    colaborador.situacaoColaboradorId = formModel.situacaoColaboradorId as number;
    colaborador.pessoa = new Pessoa();
    colaborador.pessoa.nome = formModel.nome as string;
    colaborador.pessoa.email = formModel.email as string;
    colaborador.pessoa.cpf = formModel.cpf as string;
    colaborador.pessoa.telefoneCasa = formModel.telefoneCasa as string;
    colaborador.pessoa.telefoneCelular = formModel.telefoneCelular as string;
    colaborador.pessoa.sexoId = formModel.sexoId as number;
    colaborador.pessoa.tipoId = formModel.tipoId as number;


    return colaborador;
  }
}



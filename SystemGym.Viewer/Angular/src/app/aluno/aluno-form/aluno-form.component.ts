
import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA } from '@angular/material'
import { UsuarioService } from '../../services/usuario.service';
import { Usuario } from '../../models/usuario-model';
import { Pessoa } from '../../models/pessoa-model';
import { LoaderService } from '../../services/loader.service';
import { Aluno } from '../../models/aluno-model';
import { AlunoService } from '../../services/aluno.service';

@Component({
  selector: 'app-aluno-form',
  templateUrl: './aluno-form.component.html',
  styleUrls: ['./aluno-form.component.css']
})
export class AlunoFormComponent implements OnInit {
  form: FormGroup;
  title: string = 'Cadastrar';
  aluno: Aluno;

  formSubmited: boolean;

  constructor(
    private alunoService: AlunoService,
    private loaderService: LoaderService,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<AlunoFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Aluno,
    private fb: FormBuilder) {
    this.aluno = data;
  }

  ngOnInit() {

    this.createFormGroup();

    if (this.aluno !== undefined && this.aluno !== null) {
      this.title = 'Alterar';

      this.setFormGroup();
    }
    else {
      this.aluno = new Aluno();
    }
  }

  saveClick(): void {
    this.formSubmited = true;

    if (this.form.valid) {

      let aluno: Aluno = this.prepareSave();

      if (this.aluno.alunoId === null || this.aluno.alunoId === undefined) {
        this.alunoService.add(aluno);
      }
      else {
        this.alunoService.update(this.aluno.alunoId, aluno)
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
      numeroCartao: this.aluno.numeroCartao,
      situacaoAlunoId: this.aluno.situacaoAlunoId,
      nome: this.aluno.pessoa.nome,
      email: this.aluno.pessoa.email,
      cpf: this.aluno.pessoa.cpf,
      telefoneCelular: this.aluno.pessoa.telefoneCelular,
      telefoneCasa: this.aluno.pessoa.telefoneCasa,
      sexoId: this.aluno.pessoa.sexoId,
      tipoId: this.aluno.pessoa.tipoId,
    });
  }

   private createFormGroup(): void {
    this.form = this.fb.group({
      numeroCartao: '',
      situacaoAlunoId:'',
      nome:'',
      email: '',
      cpf: '',
      telefoneCelular: '',
      telefoneCasa: '',
      sexoId: '',
      tipoId: '',
    });
   }

  private prepareSave(): Aluno {
    const formModel = this.form.value;
    let aluno: Aluno = new Aluno();

    aluno.numeroCartao = formModel.numeroCartao as string;
    aluno.situacaoAlunoId = formModel.situacaoAlunoId as number;
    aluno.pessoa = new Pessoa();
    aluno.pessoa.nome = formModel.nome as string;
    aluno.pessoa.email = formModel.email as string;
    aluno.pessoa.cpf = formModel.cpf as string;
    aluno.pessoa.telefoneCasa = formModel.telefoneCasa as string;
    aluno.pessoa.telefoneCelular = formModel.telefoneCelular as string;
    aluno.pessoa.sexoId = formModel.sexoId as number;
    aluno.pessoa.tipoId = formModel.tipoId as number;


    return aluno;
  }
}



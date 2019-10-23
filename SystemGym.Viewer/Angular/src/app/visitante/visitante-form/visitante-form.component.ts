
import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA } from '@angular/material'
import { Pessoa } from '../../models/pessoa-model';
import { LoaderService } from '../../services/loader.service';
import { Visitante } from '../../models/visitante-model';
import { VisitanteService } from '../../services/visitante.service';

@Component({
  selector: 'app-visitante-form',
  templateUrl: './visitante-form.component.html',
  styleUrls: ['./visitante-form.component.css']
})
export class VisitanteFormComponent implements OnInit {
  form: FormGroup;
  title: string = 'Cadastrar';
  visitante: Visitante;

  formSubmited: boolean;

  constructor(
    private visitanteService: VisitanteService,
    private loaderService: LoaderService,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<VisitanteFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Visitante,
    private fb: FormBuilder) {
    this.visitante = data;
  }

  ngOnInit() {

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
      docIdentidade: this.visitante.docIdentidade,
      nome: this.visitante.pessoa.nome,
      email: this.visitante.pessoa.email,
      cpf: this.visitante.pessoa.cpf,
      telefoneCelular: this.visitante.pessoa.telefoneCelular,
      telefoneCasa: this.visitante.pessoa.telefoneCasa,
      sexoId: this.visitante.pessoa.sexoId,
      tipoId: this.visitante.pessoa.tipoId,

    });
  }

   private createFormGroup(): void {
     this.form = this.fb.group({
      docIdentidade: '',
      nome:'',
      email: '',
      cpf: '',
      telefoneCelular: '',
      telefoneCasa: '',
      sexoId: '',
      tipoId: '',
    });
   }

  private prepareSave(): Visitante {
    const formModel = this.form.value;
    let visitante: Visitante = new Visitante();

    visitante.docIdentidade = formModel.docIdentidade as string;
    visitante.pessoa = new Pessoa();
    visitante.pessoa.nome = formModel.nome as string;
    visitante.pessoa.email = formModel.email as string;
    visitante.pessoa.cpf = formModel.cpf as string;
    visitante.pessoa.telefoneCasa = formModel.telefoneCasa as string;
    visitante.pessoa.telefoneCelular = formModel.telefoneCelular as string;
    visitante.pessoa.sexoId = formModel.sexoId as number;
    visitante.pessoa.tipoId = formModel.tipoId as number;


    return visitante;
  }
}



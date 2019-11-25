import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { PagamentoService } from '../../../../services/pagamento.service';
import { Pagamento } from '../../../../models/pagamento-model';
import { LoaderService, CustomMask } from '../../../../core';
import { CombosListService } from '../../../../services/combosList.service';
import { Plano } from '../../../../models/plano-model';
import { Mes } from '../../../../models/mes-model';
import { Ano } from '../../../../models/ano-model';
import { FormaPagamento } from '../../../../models/forma-pagamento-model';
import { ColaboradorService } from '../../../../services/colaborador.service';
import { Colaborador } from '../../../../models/colaborador-model';
import { ColaboradorSearch } from '../../../../models/colaborador-search-model';


@Component({
  selector: 'app-pagamento-form',
  templateUrl: './pagamento-form.component.html',
  styleUrls: ['./pagamento-form.component.scss']
})
export class PagamentoFormComponent implements OnInit {
  private alunoId: string;
  form: FormGroup;
  title: string = 'Registrar';
  pagamento: Pagamento;
  touch: boolean;
  planos: Plano[];
  anos: Ano[];
  meses: Mes[];
  colaboradores: Colaborador[];
  formaPagamentos: FormaPagamento[];

  formSubmited: boolean;

  get cnpjMask(): any {
    return CustomMask.cnpjMask();
  }

  get phoneMask() {
    return CustomMask.phoneMask();
  }

  get cepMask() {
    return CustomMask.cepMask();
  }

  @HostListener('window:keydown', ['$event'])
  keydownHandler(event) {
    let target = event.target || event.srcElement;
    if (event.keyCode === 27) {
      this.closeDialog(false);
    }
  }

  constructor(private pagamentoService: PagamentoService,
    private loaderService: LoaderService,
    private combosListService: CombosListService,
    private colaboradorService: ColaboradorService,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<PagamentoFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder) {
    this.alunoId = data.alunoId;
    this.pagamento = data.pagamento;
  }

  ngOnInit() {

    this.combosListService.getFormaPagamento().then(rows => {
      this.formaPagamentos = rows;
      this.loaderService.hide();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });

    this.combosListService.getAno().then(rows => {
      this.anos = rows;
      this.loaderService.hide();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });

    this.combosListService.getMes().then(rows => {
      this.meses = rows;
      this.loaderService.hide();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });

    this.combosListService.getPlano().then(rows => {
      this.planos = rows;
      this.loaderService.hide();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });

    let search: ColaboradorSearch = new ColaboradorSearch();
    search.page = 1;
    search.pageSize = 1000;

    this.colaboradorService.search(search).then(rows => {
      this.colaboradores = rows.items;
      this.loaderService.hide();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });



    this.touch = document.documentElement.clientWidth < 960;

    this.createFormGroup();

    if (this.pagamento !== undefined && this.pagamento !== null) {
      this.title = 'Alterar';
      this.setFormGroup();
    }
    else {
      this.pagamento = new Pagamento();
    }
  }


  saveClick(): void {
    this.formSubmited = true;

    if (this.form.valid) {
      this.loaderService.show();

      let pagamento: Pagamento = this.prepareSave();

      let save: Promise<any>;

      if (this.pagamento.pagamentoId === undefined || this.pagamento.pagamentoId === null) {
        save = this.pagamentoService.add(pagamento);
      } else {
        save = this.pagamentoService.update(this.pagamento.pagamentoId, pagamento);
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

  planoSelected() {
    if (this.form.controls['planoId'].value == 1) {
      this.form.controls['valorMensalidade'].setValue('100,00 R$');
    } else {
      this.form.controls['valorMensalidade'].setValue('90,00 R$');
    };
    this.form.controls['valorMensalidade'].disable();
  }

  private createFormGroup(): void {
    this.form = this.fb.group({
      colaboradorId: '',
      planoId: ['', { validators: Validators.required }],
      valorMensalidade: ['', { validators: Validators.required }],
      mesId: ['', { validators: Validators.required }],
      anoId: ['', { validators: Validators.required }],
      formaPagamentoId: ['', { validators: Validators.required }],
      
    });
  }

  private setFormGroup(): void {
    this.form.setValue({
      alunoId: this.pagamento.alunoId,
      colaboradorId: this.pagamento.colaboradorId,
      planoId: this.pagamento.planoId,
      valorMensalidade: this.pagamento.valorMensalidade,
      mesId: this.pagamento.mesId,
      anoId: this.pagamento.anoId,
      formaPagamentoId: this.pagamento.formaPagamentoId,

    });
  }

  private prepareSave(): Pagamento {
    const formModel = this.form.value;
    let pagamento: Pagamento = new Pagamento();

    pagamento.alunoId = this.alunoId;

    pagamento.planoId = formModel.planoId as number;

    if (formModel.colaboradorId !== undefined && formModel.colaboradorId !== "") {
      pagamento.colaboradorId = formModel.colaboradorId as string;
    }
  
    pagamento.valorMensalidade = this.form.controls['valorMensalidade'].value as string;

    pagamento.mesId = formModel.mesId as number;

    pagamento.anoId = formModel.anoId as number;

    pagamento.formaPagamentoId = formModel.formaPagamentoId as number;

    return pagamento;
  }
}

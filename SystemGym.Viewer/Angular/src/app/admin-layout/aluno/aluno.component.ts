import { Component, OnInit, Pipe, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as moment from 'moment';

import { AlunoFormService } from './aluno-form/aluno-form.service';
import { Aluno } from '../../models/aluno-model';
import { AlunoService } from '../../services/aluno.service';
import { ConfirmService, LoaderService } from '../../core';
import { MatSnackBar, MatPaginator, MatSort } from '@angular/material';
import { AlunoSearch } from '../../models/aluno-search-model';
import { merge } from 'rxjs';
import { PagamentoListService } from './pagamento/pagamento.service';

@Component({
  selector: 'app-aluno',
  templateUrl: './aluno.component.html',
  styleUrls: ['./aluno.component.scss']
})
export class AlunoComponent implements OnInit {

  form: FormGroup;
  aluno: Aluno;
  data: Aluno[];
  dataSource = this.data;

  noDataFound: boolean = false;
  formSubmited: boolean;
  length: number;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(private alunoService: AlunoService,
    private alunoForService: AlunoFormService,
    private pagamentoListService: PagamentoListService,
    private confirmService: ConfirmService,
    private snackBar: MatSnackBar,
    private loaderService: LoaderService,
    private fb: FormBuilder) { }

  displayedColumns: string[] = ['pessoa.nome', 'pessoa.cpf', 'pessoa.email', 'pagamento' ,'edit', 'del'];

  ngOnInit() {

    this.createFormGroup();

    this.loadData();

    this.paginator.pageIndex = 0;
    this.paginator.pageSize = document.documentElement.clientWidth < 600 ? 10 : 20;
    this.sort.active = 'pessoa.nome';
    this.sort.direction = 'asc';

    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    merge(this.sort.sortChange, this.paginator.page)
      .subscribe(() => {
        this.loadData();
        this.scrollTop();
      });

  }


  loadData() {

    this.loaderService.show();

    let alunoSearch: AlunoSearch = this.prepareSearch();

    this.alunoService.search(alunoSearch).then(rows => {
      this.length = rows.totalItems;
      this.data = rows.items;
      this.noDataFound = (this.data === null || this.data.length === 0);
      this.loaderService.hide();
    },
      error => {
        this.loaderService.hide();
        this.snackBar.open(error, 'Fechar', {
          duration: 10000
        });
      });
  }


  editClick(aluno: Aluno) {
    this.alunoForService.showDialog(aluno).subscribe();
  }

  deleteClick(aluno: Aluno) {
    this.confirmService.activate()
      .then(confirmed => {
        if (confirmed) {
          this.loaderService.show();
          this.alunoService.delete(aluno.alunoId)
            .then(deleted => {
              this.loadData();
              this.snackBar.open('Registro excluído com sucesso!.', 'Fechar', {
                duration: 5000
              });
            },
              error => {
                this.snackBar.open(<string>error, 'Fechar', {
                  duration: 50000
                });
                this.loaderService.hide();
              });
        }
      });
  }

  pagamentoClick(aluno: Aluno) {
    this.pagamentoListService.showDialog(aluno).subscribe(update => {
      if (update) {
        this.loadData();
      }
    });
  }

  clearForm() {
    this.paginator.pageIndex = 0;
    this.paginator.pageSize = document.documentElement.clientWidth < 600 ? 10 : 20;
    this.form.reset();
    this.loadData();
  }

  createClick(): void {
    this.alunoForService.showDialog(null).subscribe();
  }

  private createFormGroup(): void {
    this.form = this.fb.group({
      nome: '',
      cpf: '',
    });
  }

  private prepareSearch(): AlunoSearch {
    let search: AlunoSearch = new AlunoSearch();
    const formModel = this.form.value;


    if (formModel.nome !== null && formModel.nome !== '' && formModel.nome !== undefined) {
      search.nome = formModel.nome as string;
    }

    if (formModel.cpf !== null && formModel.cpf !== '' && formModel.cpf !== undefined) {
      search.cpf = formModel.cpf as string;
    }

    search.page = this.paginator.pageIndex + 1;
    search.pageSize = this.paginator.pageSize;
    search.sort = this.sort.active;
    search.sortDirection = this.sort.direction;

    return search;
  }

  private scrollTop(): void {
    if (document.getElementsByClassName('mat-drawer-content').length > 0) {
      document.getElementsByClassName('mat-drawer-content')[0].scrollTop = 0;
    }
  }
}


import { Component, OnInit, Pipe, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as moment from 'moment';

import { ColaboradorFormService } from './colaborador-form/colaborador-form.service';
import { Colaborador } from '../../models/colaborador-model';
import { ColaboradorService } from '../../services/colaborador.service';
import { ConfirmService, LoaderService } from '../../core';
import { MatSnackBar, MatPaginator, MatSort } from '@angular/material';
import { ColaboradorSearch } from '../../models/colaborador-search-model';
import { merge } from 'rxjs';
import { AddressService } from '../../services/address.service';
import { State } from '../../models/state-model';


@Component({
  selector: 'app-colaborador',
  templateUrl: './colaborador.component.html',
  styleUrls: ['./colaborador.component.scss']
})
export class ColaboradorComponent implements OnInit {

  form: FormGroup;
  colaborador: Colaborador;
  data: Colaborador[];
  dataSource = this.data;

  noDataFound: boolean = false;
  formSubmited: boolean;
  length: number;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(private colaboradorService: ColaboradorService,
    private colaboradorForService: ColaboradorFormService,
    private confirmService: ConfirmService,
    private snackBar: MatSnackBar,
    private loaderService: LoaderService,
    private fb: FormBuilder) { }

  displayedColumns: string[] = ['pessoa.nome', 'pessoa.cpf', 'pessoa.email', 'docIdentidade','criacaoData','edit', 'del'];

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

    let colaboradorSearch: ColaboradorSearch = this.prepareSearch();

    this.colaboradorService.search(colaboradorSearch).then(rows => {
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

  editClick(colaborador: Colaborador) {
    this.colaboradorForService.showDialog(colaborador).subscribe(update => {
      if (update) {
        this.loadData();
      }
    });
  }

  deleteClick(colaborador: Colaborador) {
    this.confirmService.activate()
      .then(confirmed => {
        if (confirmed) {
          this.loaderService.show();
          this.colaboradorService.delete(colaborador.colaboradorId)
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

  clearForm() {
    this.paginator.pageIndex = 0;
    this.paginator.pageSize = document.documentElement.clientWidth < 600 ? 10 : 20;
    this.form.reset();
    this.loadData();
  }

  createClick(): void {
    this.colaboradorForService.showDialog(null).subscribe(update => {
      if (update) {
        this.loadData();
      }
    });
  }

  private createFormGroup(): void {
    this.form = this.fb.group({
      nome: '',
      numeroCtps: '',
    });
  }

  private prepareSearch(): ColaboradorSearch {
    let search: ColaboradorSearch = new ColaboradorSearch();
    const formModel = this.form.value;


    if (formModel.nome !== null && formModel.nome !== '' && formModel.nome !== undefined) {
      search.nome = formModel.nome as string;
    }

    if (formModel.numeroCtps !== null && formModel.numeroCtps !== '' && formModel.numeroCtps !== undefined) {
      search.numeroCtps = formModel.numeroCtps as string;
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


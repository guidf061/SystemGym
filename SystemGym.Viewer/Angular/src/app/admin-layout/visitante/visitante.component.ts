import { Component, OnInit, Pipe, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as moment from 'moment';

import { VisitanteFormService } from './visitante-form/visitante-form.service';
import { Visitante } from '../../models/visitante-model';
import { VisitanteService } from '../../services/visitante.service';
import { ConfirmService, LoaderService } from '../../core';
import { MatSnackBar, MatPaginator, MatSort } from '@angular/material';
import { VisitanteSearch } from '../../models/visitante-search-model';
import { merge } from 'rxjs';
import { AddressService } from '../../services/address.service';
import { State } from '../../models/state-model';


@Component({
  selector: 'app-visitante',
  templateUrl: './visitante.component.html',
  styleUrls: ['./visitante.component.scss']
})
export class VisitanteComponent implements OnInit {

  form: FormGroup;
  visitante: Visitante;
  data: Visitante[];
  dataSource = this.data;

  noDataFound: boolean = false;
  formSubmited: boolean;
  length: number;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(private visitanteService: VisitanteService,
    private visitanteForService: VisitanteFormService,
    private confirmService: ConfirmService,
    private snackBar: MatSnackBar,
    private loaderService: LoaderService,
    private fb: FormBuilder) { }

  displayedColumns: string[] = ['pessoa.nome', 'pessoa.cpf', 'docIdentidade', 'pessoa.email', 'visitaData', 'horaVisita' ,'edit', 'del'];

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

    let visitanteSearch: VisitanteSearch = this.prepareSearch();

    this.visitanteService.search(visitanteSearch).then(rows => {
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

  editClick(visitante: Visitante) {
    this.visitanteForService.showDialog(visitante).subscribe(update => {
      if (update) {
        this.loadData();
      }
    });
  }

  deleteClick(visitante: Visitante) {
    this.confirmService.activate()
      .then(confirmed => {
        if (confirmed) {
          this.loaderService.show();
          this.visitanteService.delete(visitante.visitanteId)
            .then(deleted => {
              this.loadData();
              this.snackBar.open('Registro exclu??do com sucesso!.', 'Fechar', {
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
    this.visitanteForService.showDialog(null).subscribe(update => {
      if (update) {
        this.loadData();
      }
    });
  }

  private createFormGroup(): void {
    this.form = this.fb.group({
      nome: '',
      docIdentidade: '',
    });
  }

  private prepareSearch(): VisitanteSearch {
    let search: VisitanteSearch = new VisitanteSearch();
    const formModel = this.form.value;


    if (formModel.nome !== null && formModel.nome !== '' && formModel.nome !== undefined) {
      search.nome = formModel.nome as string;
    }

    if (formModel.docIdentidade !== null && formModel.docIdentidade !== '' && formModel.docIdentidade !== undefined) {
      search.docIdentidade = formModel.docIdentidade as string;
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


import { Component, OnInit, Pipe, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as moment from 'moment';

import { UsuarioFormService } from './usuario-form/usuario-form.service';
import { Usuario } from '../../models/usuario-model';
import { UsuarioService } from '../../services/usuario.service';
import { ConfirmService, LoaderService } from '../../core';
import { MatSnackBar, MatPaginator, MatSort } from '@angular/material';
import { UsuarioSearch } from '../../models/usuario-search-model';
import { merge } from 'rxjs';
import { AddressService } from '../../services/address.service';
import { State } from '../../models/state-model';


@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.scss']
})
export class UsuarioComponent implements OnInit {

  form: FormGroup;
  usuario: Usuario;
  data: Usuario[];
  dataSource = this.data;
 
  noDataFound: boolean = false;
  formSubmited: boolean;
  length: number;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(private usuarioService: UsuarioService,
    private usuarioForService: UsuarioFormService,
    private confirmService: ConfirmService,
    private snackBar: MatSnackBar,
    private loaderService: LoaderService,
    private fb: FormBuilder) { }

  displayedColumns: string[] = ['pessoa.nome', 'pessoa.cpf', 'userName', 'pessoa.email', 'edit' ,'del'];

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

    let usuarioSearch: UsuarioSearch = this.prepareSearch();

    this.usuarioService.search(usuarioSearch).then(rows => {
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


  editClick(usuario: Usuario) {
    this.usuarioForService.showDialog(usuario).subscribe();
  }

  deleteClick(usuario: Usuario) {
    this.confirmService.activate()
      .then(confirmed => {
        if (confirmed) {
          this.loaderService.show();
          this.usuarioService.delete(usuario.usuarioId)
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
    this.usuarioForService.showDialog(null).subscribe();
  }

  private createFormGroup(): void {
    this.form = this.fb.group({
      nome: '',
      cpf: '',
    });
  }

  private prepareSearch(): UsuarioSearch {
    let search: UsuarioSearch = new UsuarioSearch();
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


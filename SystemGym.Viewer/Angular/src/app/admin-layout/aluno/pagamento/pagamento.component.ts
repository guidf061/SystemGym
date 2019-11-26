import { Component, OnInit, OnDestroy, ViewChild, Inject, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators, CheckboxControlValueAccessor } from '@angular/forms';
import { PageEvent, MatPaginator } from '@angular/material/paginator';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';




import { Aluno } from '../../../models/aluno-model';
import { Pagamento } from '../../../models/pagamento-model';
import { PagamentoService } from '../../../services/pagamento.service';
import { PagamentoSearch } from '../../../models/pagamento-search-model';
import { AuthService } from '../../../core/tools/auth.service';
import { LoggedUserService } from '../../../core/tools/logged-user.service';
import { ConfirmService, LoaderService } from '../../../core';
import { PagamentoFormService } from './form/pagamento-form.service';
import { MatSort } from '@angular/material';
import { CombosListService } from '../../../services/combosList.service';
import { Mes } from '../../../models/mes-model';

@Component({
  selector: 'app-pagamento',
  templateUrl: './pagamento.component.html',
  styleUrls: ['./pagamento.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class PagamentoComponent implements OnInit, OnDestroy {

  private aluno: Aluno;
  companyId: string;
  form: FormGroup;
  data: Pagamento[];
  meses: Mes[];
  noDataFound: boolean = false;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  length: number;

  constructor(private authService: AuthService,
    private combosListService: CombosListService,
    private pagamentoService: PagamentoService,
    private pagamentoFormService: PagamentoFormService,
    private snackBar: MatSnackBar,
    private loggedUserService: LoggedUserService,
    private confirmService: ConfirmService,
    private loaderService: LoaderService,
    private dialogRef: MatDialogRef<PagamentoComponent>,
    @Inject(MAT_DIALOG_DATA) public params: Aluno,
    private fb: FormBuilder) {
    this.aluno = params;
  }

  displayedColumns: string[] = ['pagamentoDate', 'edit', 'del'];

  ngOnInit() {

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

    this.createFormGroup();

    this.loadData();
  }

  loadData() {
    this.loaderService.show();

    let pagamentoSearch: PagamentoSearch = this.prepareSearch();

    this.pagamentoService.search(pagamentoSearch).then(rows => {
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

  createClick(): void {
    this.pagamentoFormService.showDialog(this.aluno.alunoId, null).subscribe(update => {
      if (update) {
        this.loadData();
      }
    });
  }

  editClick(pagamento: Pagamento) {
    this.pagamentoFormService.showDialog(this.aluno.alunoId, pagamento).subscribe(update => {
      if (update) {
        this.loadData();
      }
    });
  }

  getColor(value: number): string {
    let dataNow = new Date();
    let getMonth = dataNow.getMonth() + 1;
    let objetoMonth = this.data.map(x => x.mesId);
  
    let filterMonth = objetoMonth.findIndex(x => x === value);

    if (filterMonth === -1 && filterMonth < getMonth) {
      return "red";
    }
    else if (filterMonth => 0 && filterMonth <= getMonth) {
      return "green";
    }
    else if (filterMonth > getMonth){
      return "SlateGray";
    }

  }

  getImg(value: number) : string {

   let objeto = this.data.map(x => x.mesId);
   let filter = objeto.findIndex(x => x === value);

   if (filter === -1) {
      return "warning";
    } else {
      return "check_circle";
    }
  } 

  getInfo(value: number): string {

    let objeto = this.data.map(x => x.mesId);
    let filter = objeto.findIndex(x => x === value);

    if (filter === -1) {
      return "Pagamento ainda nao efetuado ";
    } else if( filter !== -1) {
      return "Pagamento efetuado ";
    }
  }



  deleteClick(pagamento: Pagamento) {
    this.confirmService.activate()
      .then(confirmed => {
        if (confirmed) {
          this.loaderService.show();
          this.pagamentoService.delete(pagamento.pagamentoId)
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

  ngOnDestroy(): void {
  }

  clearForm() {
    this.paginator.pageIndex = 0;
    this.paginator.pageSize = document.documentElement.clientWidth < 600 ? 10 : 20;
    this.form.reset();
    this.loadData();
  }

  closeDialog(update: boolean) {
    this.dialogRef.close(update);
  }

  private createFormGroup(): void {
    this.form = this.fb.group({
    });
  }

  private prepareSearch(): PagamentoSearch {
    let search: PagamentoSearch = new PagamentoSearch();

    const formModel = this.form.value;

    search.alunoId = this.aluno.alunoId;

    return search;
  }

  private scrollTop(): void {
    if (document.getElementsByClassName('mat-drawer-content').length > 0) {
      document.getElementsByClassName('mat-drawer-content')[0].scrollTop = 0;
    }
  }
}

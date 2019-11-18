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
  noDataFound: boolean = false;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  length: number;

  constructor(private authService: AuthService,
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

  ngOnInit() {
    this.paginator.pageIndex = 0;
    this.paginator.pageSize = document.documentElement.clientWidth < 600 ? 10 : 20;

    this.createFormGroup();

    this.loadData();

    this.paginator.page
      .subscribe(() => {
        this.loadData();
        this.scrollTop();
      });
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

  private prepareSearch():PagamentoSearch {
    let search: PagamentoSearch = new PagamentoSearch();

    const formModel = this.form.value;

    search.alunoId = this.aluno.alunoId;
    
    search.page = this.paginator.pageIndex + 1;
    search.pageSize = this.paginator.pageSize;

    return search;
  }

  private scrollTop(): void {
    if (document.getElementsByClassName('mat-drawer-content').length > 0) {
      document.getElementsByClassName('mat-drawer-content')[0].scrollTop = 0;
    }
  }
}

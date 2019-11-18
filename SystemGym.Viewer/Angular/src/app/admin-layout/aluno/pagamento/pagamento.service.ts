import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

import { Observable } from 'rxjs';
import { Aluno } from '../../../models/aluno-model';
import { PagamentoComponent } from './pagamento.component';


@Injectable()
export class PagamentoListService {

  constructor(private dialog: MatDialog) { }

  showDialog(aluno: Aluno): Observable<boolean> {
    let dialogRef: MatDialogRef<PagamentoComponent>;

    dialogRef = this.dialog.open(PagamentoComponent, {
      disableClose: true,
      width: '750px',
      height: '500px',
      panelClass: 'modal-wrapper',
      data: aluno
    });

    dialogRef.afterClosed().subscribe(result => {
      dialogRef = null;
    });

    return dialogRef.afterClosed();
  }
}

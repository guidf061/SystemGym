import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

import { Observable } from 'rxjs';

import { Pagamento } from '../../../../models/pagamento-model';
import { PagamentoFormComponent } from './pagamento-form.component';



@Injectable()
export class PagamentoFormService {

  constructor(private dialog: MatDialog) { }

  showDialog(alunoId: string, pagamento: Pagamento): Observable<boolean> {
    let dialogRef: MatDialogRef<PagamentoFormComponent>;

    dialogRef = this.dialog.open(PagamentoFormComponent, {
      disableClose: true,
      width: '500px',
      height: '200px',
      panelClass: 'modal-wrapper',
      data: {
        pagamento,
        alunoId
      },
    });

    dialogRef.afterClosed().subscribe(result => {
      dialogRef = null;
    });

    return dialogRef.afterClosed();
  }
}

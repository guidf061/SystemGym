import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material'

import { Observable } from 'rxjs';

import { VisitanteFormComponent } from './visitante-form.component';
import { Visitante } from '../../models/visitante-model';

@Injectable()
export class VisitanteFormService {

  constructor(private dialog: MatDialog) { }

  showDialog(visitante: Visitante): Observable<boolean> {
    let dialogRef: MatDialogRef<VisitanteFormComponent>;

    dialogRef = this.dialog.open(VisitanteFormComponent, {
      disableClose: true,
      width: '450px',
      height: '700px',
      panelClass: 'modal-wrapper',
      data: visitante
    });

    dialogRef.afterClosed().subscribe(result => {
      dialogRef = null;
    });

    return dialogRef.afterClosed();
  }
}

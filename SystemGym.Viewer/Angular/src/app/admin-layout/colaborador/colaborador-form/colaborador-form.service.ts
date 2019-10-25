import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material'

import { Observable } from 'rxjs';

import { ColaboradorFormComponent } from './colaborador-form.component';
import { Colaborador } from '../../../models/colaborador-model';

@Injectable()
export class ColaboradorFormService {

  constructor(private dialog: MatDialog) { }

  showDialog(colaborador: Colaborador): Observable<boolean> {
    let dialogRef: MatDialogRef<ColaboradorFormComponent>;

    dialogRef = this.dialog.open(ColaboradorFormComponent, {
      disableClose: true,
      width: '450px',
      height: '700px',
      panelClass: 'modal-wrapper',
      data: colaborador
    });

    dialogRef.afterClosed().subscribe(result => {
      dialogRef = null;
    });

    return dialogRef.afterClosed();
  }
}

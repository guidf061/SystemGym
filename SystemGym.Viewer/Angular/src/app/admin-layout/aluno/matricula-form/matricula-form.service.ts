import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material'

import { Observable } from 'rxjs';

import { MatriculaFormComponent } from './matricula-form.component';
import { MatriculaAluno } from '../../../models/matricula-aluno-model';

@Injectable()
export class MatriculaFormService {

  constructor(private dialog: MatDialog) { }

  showDialog(matriculaAluno: MatriculaAluno): Observable<boolean> {
    let dialogRef: MatDialogRef<MatriculaFormComponent>;

    dialogRef = this.dialog.open(MatriculaFormComponent, {
      disableClose: true,
      width: '450px',
      height: '530px',
      panelClass: 'modal-wrapper',
      data: matriculaAluno
    });

    dialogRef.afterClosed().subscribe(result => {
      dialogRef = null;
    });

    return dialogRef.afterClosed();
  }
}

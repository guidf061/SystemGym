import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material'

import { Observable } from 'rxjs';

import { AlunoFormComponent } from './aluno-form.component';
import { Aluno } from '../../../models/aluno-model';

@Injectable()
export class AlunoFormService {

  constructor(private dialog: MatDialog) { }

  showDialog(aluno: Aluno): Observable<boolean> {
    let dialogRef: MatDialogRef<AlunoFormComponent>;

    dialogRef = this.dialog.open(AlunoFormComponent, {
      disableClose: true,
      width: '450px',
      height: '530px',
      panelClass: 'modal-wrapper',
      data: aluno
    });

    dialogRef.afterClosed().subscribe(result => {
      dialogRef = null;
    });

    return dialogRef.afterClosed();
  }
}

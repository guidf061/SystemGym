import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material'

import { Observable } from 'rxjs';
import { UsuarioFormComponent } from './usuario-form.component';
import { Usuario } from '../../../models/usuario-model';

@Injectable({
  providedIn: 'root'
})
export class UsuarioFormService {

  constructor(private dialog: MatDialog) { }

  showDialog(usuario: Usuario): Observable<boolean> {
    let dialogRef: MatDialogRef<UsuarioFormComponent>;

    dialogRef = this.dialog.open(UsuarioFormComponent, {
      disableClose: true,
      width: '450px',
      height: '550px',
      panelClass: 'modal-wrapper',
      data: usuario
    });

    dialogRef.afterClosed().subscribe(result => {
      dialogRef = null;
    });

    return dialogRef.afterClosed();
  }
}

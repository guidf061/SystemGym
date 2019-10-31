import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';

import { LoginComponent } from '../components/login/login.component';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private dialog: MatDialog) { }

  showDialog(): Observable<boolean> {
    let dialogRef: MatDialogRef<LoginComponent>;

    dialogRef = this.dialog.open(LoginComponent, {
      disableClose: true,
      width: '350px',
      height: '300px',
      //panelClass: 'modal-wrapper',
    });

    dialogRef.afterClosed().subscribe(result => {
      dialogRef = null;
    });

    return dialogRef.afterClosed();
  }
}

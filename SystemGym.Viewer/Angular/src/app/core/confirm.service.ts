import { Injectable, ViewContainerRef } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import { ConfirmComponent } from '../components/confirm/confirm.component';

@Injectable()
export class ConfirmService {

  viewContainerRef: ViewContainerRef;

  constructor(private dialog: MatDialog) { }

  activate(message?: string, title?: string): Promise<boolean> {
    let dialogRef: MatDialogRef<ConfirmComponent>;

    dialogRef = this.dialog.open(ConfirmComponent, {
      role: 'dialog',
      disableClose: true,
      width: '55%',
      position: {
        top: '5%'
      },
      panelClass: 'alert-modal-wrapper',
      viewContainerRef: this.viewContainerRef,
      data: { message, title }
    });

    dialogRef.afterClosed().subscribe(result => {
      dialogRef = null;
    });

    return dialogRef.afterClosed().toPromise();
  }
}

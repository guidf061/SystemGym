import { Injectable, ViewContainerRef } from '@angular/core';
import { Observable } from 'rxjs';
import { MatDialog, MatDialogRef, MatDialogConfig } from '@angular/material';


import { LoaderService } from './loader.service';
import { AlertDialogComponent } from '../components/alert-dialog/alert-dialog.component';

export declare type AlertDialogType = 'alert' | 'success' | 'error' | 'validation';

@Injectable()
export class AlertDialogService {
  viewContainerRef: ViewContainerRef;
  dialogRef: MatDialogRef<AlertDialogComponent>;

  constructor(private dialog: MatDialog,
    private loaderService: LoaderService) { }

  showDialog(alertType: string, title: string, message: string): Observable<boolean> {
    this.loaderService.hide();
    if (message !== undefined && message !== null && message !== '') {

      if (this.dialogRef !== undefined && this.dialogRef !== null) {
        this.dialogRef.close();
      }

      this.dialogRef = this.dialog.open(AlertDialogComponent, {
        role: 'dialog',
        disableClose: false,
        width: '55%',
        position: {
          top: '5%'
        },
        panelClass: 'alert-modal-wrapper',
        viewContainerRef: this.viewContainerRef,
        data: { alertType, title, message }
      });

      this.dialogRef.afterClosed().subscribe(result => {
        //this.dialogRef = null;
      });

      return this.dialogRef.afterClosed();
    }
  }
}

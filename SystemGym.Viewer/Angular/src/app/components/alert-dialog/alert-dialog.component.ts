import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-alert-dialog',
  templateUrl: './alert-dialog.component.html',
  styleUrls: ['./alert-dialog.component.css']
})
export class AlertDialogComponent implements OnInit {

  alertType: string;
  title: string;
  message: string;
  buttonLabel: string = 'Fechar';

  constructor(private dialogRef: MatDialogRef<AlertDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    this.setLabels(data.alertType, data.title, data.message);
  }

  ngOnInit(): void {
  }

  closeDialog(): void {
    this.dialogRef.close();
  }

  private setLabels(alertType: string, title: string, message: string) {
    this.alertType = alertType;
    this.title = title;
    this.message = message;

    if (this.message.indexOf('Http failure response') > -1) {
      this.message = 'N\xE3o foi possivel carregar os dados. Verifique sua conex\xE3o com a internet e tente novamente.';
      this.buttonLabel = "Tentar";
    }
    else if (this.title === 'Seja bem-vindo!!!') {
      this.buttonLabel = "Continuar";
    }
  }
}

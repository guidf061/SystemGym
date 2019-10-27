import { Component, OnInit, HostListener, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

const KEY_ESC = 27;

@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.css']
})
export class ConfirmComponent implements OnInit {
  cancelText: string;
  message: string;
  okText: string;
  title: string;

  @HostListener('window:keydown', ['$event'])
  keydownHandler(event) {
    let target = event.target || event.srcElement;
    if (event.keyCode === 27) {
      this.closeDialog(false);
    }
  }

  private defaults = {
    title: 'Aten\xE7\xE3o!',
    message: 'Essa opera\xE7\xE3o n\xE3o poder\xE1 ser desfeita, tem certeza de que deseja continuar?',
    cancelText: 'Cancelar',
    okText: 'Confirmar'
  };

  constructor(private dialogRef: MatDialogRef<ConfirmComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    this.setLabels(data.message, data.title);
  }

  ngOnInit(): any {
  }

  closeDialog(update: boolean) {
    this.dialogRef.close(update);
  }

  private setLabels(message = this.defaults.message, title = this.defaults.title) {
    this.title = title;
    this.message = message;
    this.okText = this.defaults.okText;
    this.cancelText = this.defaults.cancelText;
  }
}

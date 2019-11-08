import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AuthService } from '../../core/tools/auth.service';
import { Usuario } from '../../models/usuario-model';
import { HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;

  url = 'https://localhost:44365/api/Usuario';
  private endpointUrl = this.url;  // URL to web API

  constructor(private authService: AuthService,
    private http: HttpClient,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private dialogRef: MatDialogRef<LoginComponent>,
    private fb: FormBuilder) {
  }

  ngOnInit() {
    this.createForm();
  }

  login() {
    if (this.form.valid) {
      let userName = this.form.controls['userName'].value;
      let password = this.form.controls['password'].value;

      let obj = { userName: userName, password: password };

      return this.http.post<Usuario>(this.endpointUrl + '/Login', obj)
        .toPromise()
        .then(res => {
          this.closeDialog(true);
        })
        .catch(message => {
          this.snackBar.open(message, 'Fechar', {
            duration: 5000,
          });
        });
    }
  }

  private createForm() {
    this.form = this.fb.group({
      userName: ['', { validators: Validators.required }],
      password: ['', { validators: Validators.required }]
    });
  }

  closeDialog(logado: boolean) {
    this.dialogRef.close(logado);
  }
}

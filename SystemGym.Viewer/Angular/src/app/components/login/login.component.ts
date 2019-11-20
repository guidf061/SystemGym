import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AuthService } from '../../core/tools/auth.service';
import { Usuario } from '../../models/usuario-model';
import { HttpClient} from "@angular/common/http";
import { MatTabGroup } from '@angular/material';
import { LoaderService, CustomErrorHandler } from '../../core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  email: string;
  invalidLogin: string;
  formForgot: FormGroup;
  formSubmited: boolean = false;
  formForgotSubmited: boolean = false;
  title = 'Login';

  @ViewChild('panelView', { static: true })
  panelView: MatTabGroup;

  url = 'https://localhost:44365/api/Usuario';
  private endpointUrl = this.url;  // URL to web API

  constructor(private authService: AuthService,
    private http: HttpClient,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private loaderService: LoaderService,
    private dialogRef: MatDialogRef<LoginComponent>,
    private fb: FormBuilder) {
  }

  ngOnInit() {
    this.createForm();
    this.loaderService.hide();
  }

  login() {

    if (this.form.valid) {
      this.loaderService.show();
      this.invalidLogin = '';

      const formModel = this.form.value;

      let userName: string = formModel.userName as string;
      let password: string = formModel.password as string;

      let obj = { userName: userName, password: password };

      return this.http.post<Usuario>(this.endpointUrl + '/Login', obj)
        .toPromise()
        .then(res => {
          this.closeDialog(true);
        })
        .catch(message => {
          this.handleError(message)
        });
    }
  }


  private handleError(err: any) {
    this.invalidLogin = CustomErrorHandler.extractMessage(err);
    this.loaderService.hide();
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

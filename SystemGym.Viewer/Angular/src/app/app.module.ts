import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';

import { AppComponent } from './app.component';


import { SharedModule } from './shared/shared.module';
import { AdminLayoutComponent } from './admin-layout/admin-layout.component';
import { CoreModule } from '@angular/flex-layout';
import { ConfirmService, LoaderService, JwtService } from './core';
import { AlertDialogComponent } from './components/alert-dialog/alert-dialog.component';
import { ConfirmComponent } from './components/confirm/confirm.component';
import { LoaderComponent } from './components/loader/loader.component';
import { LoginComponent } from './components/login/login.component';
import { LoginService } from './core/login.service';
import { HomeComponent } from './home/home.component';
import { MatDatepickerModule, MatNativeDateModule, MAT_DATE_FORMATS } from '@angular/material';

export const DD_MM_YYYY_Format = {
  parse: {
    dateInput: 'LL',
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@NgModule({
  imports: [
    BrowserAnimationsModule,
    HttpClientModule,
    ComponentsModule,
    CoreModule,
    RouterModule,
    AppRoutingModule,
    NgbModule,
    SharedModule,
    ToastrModule.forRoot()
  ],
  declarations: [
    AppComponent,
    ConfirmComponent,
    AlertDialogComponent,
    LoaderComponent,
    LoginComponent,
    HomeComponent,
  ],
  exports: [
    MatDatepickerModule,
    MatNativeDateModule,
  ],

  entryComponents: [
    AlertDialogComponent,
    ConfirmComponent,
    LoginComponent,
  ],
  providers: [
    ConfirmService,
    LoaderService,
    LoginService,
    JwtService,
    MatDatepickerModule,
     { provide: MAT_DATE_FORMATS, useValue: DD_MM_YYYY_Format },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

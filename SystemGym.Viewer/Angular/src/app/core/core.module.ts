import { NgModule, Optional, SkipSelf, ErrorHandler, LOCALE_ID } from '@angular/core';
import './tools/extensions';
import { registerLocaleData } from '@angular/common';
import lcoalePT from '@angular/common/locales/pt';

import { AlertDialogService } from './alert-dialog.service';
import { ConfirmService } from './confirm.service';
import { CustomErrorHandler } from './tools/custom-error-handler';
import { JwtService } from './tools/jtw.service';
import { LoaderService } from './loader.service';
import { MobileGuard } from './tools/mobile-guard.service';
import { NgIdleModule } from '@ng-idle/core';


registerLocaleData(lcoalePT);

import * as moment from 'moment';
import 'moment/locale/pt-br';
import { AuthService } from './tools/auth.service';
import { LoggedUserService } from './tools/logged-user.service';
import { LoginService } from './login.service';

@NgModule({
  imports: [
    NgIdleModule.forRoot()
  ],
  providers: [
    { provide: ErrorHandler, useClass: CustomErrorHandler },
    { provide: LOCALE_ID, useValue: "pt" },
    AuthService,
    AlertDialogService,
    ConfirmService,
    LoggedUserService,
    JwtService,
    LoaderService,
    MobileGuard,
    LoginService
  ],
  exports: [
  ],
})
export class CoreModule {
  constructor( @Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error('CoreModule is already loaded. Import it in the AppModule only');
    }
  }
}

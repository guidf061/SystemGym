import { ErrorHandler, Injectable, Injector } from '@angular/core';

import { environment } from '../../../environments/environment';
import { AlertDialogService } from '../alert-dialog.service';

@Injectable()
export class CustomErrorHandler extends ErrorHandler {

  constructor(private injector: Injector) {
    super();
  }

  handleError(error) {
    super.handleError(error);

    let alertDialogService = <AlertDialogService>this.injector.get(AlertDialogService);
    let errorMessage: string = CustomErrorHandler.extractMessage(error);

    if (errorMessage.indexOf('Unauthorized') === -1) {
      if (!environment.production) {
        alertDialogService.showDialog('validation', 'Acesso negado', 'No momento, voc\xEA n\xE3o tem permiss\xE3o para acessar esta pasta.');
      }
    } 
  }

  static extractMessage(err: any): string {
    let message: string;

    /*
    if (err.error && !String.isNullOrEmpty(err.error.message)) {
      return err.error.message;
    }
    */

    if (err.error) {
      if (typeof err.error !== 'object' && err.error.indexOf('{"message":') === 0) {
        let error = JSON.parse(err.error);
        if (error && !String.isNullOrEmpty(error.message)) {
          return error.message;
        }
      }
      else {
        if (err.error.message != undefined) {
          return err.error.message;
        }
        else if (err.error.error !== undefined && err.error.error.message != undefined) {
          return err.error.error.message;
        }
      }
    }

    if (typeof err === 'string' && err.indexOf('{"message":') === 0) {
      let error = JSON.parse(err);
      if (error && !String.isNullOrEmpty(error.message)) {
        return error.message;
      }
    }

    if (!String.isNullOrEmpty(err.message)) {
      return err.message;
    }

    if (err._body && (typeof err._body === 'string' || err._body instanceof String)) {
      message = JSON.parse(err._body).message;
    }

    if (message !== null && message !== undefined) {
      return message;
    } else if (err instanceof Error) {
      return err.message;
    } else if (err instanceof XMLHttpRequest) {
      return err.statusText;
    } else if (err.statusText) {
      return err.statusText;
    } else {
      return err.toString();
    }
  }

  static readBlob(blob: Blob): Promise<string> {
    return new Promise(function (resolve, reject) {
      let reader = new FileReader();

      reader.onload = function () {
        resolve(reader.result.toString());
      }

      reader.readAsText(blob);
    });
  }

}

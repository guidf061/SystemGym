import { FormGroup, FormControl, AbstractControl, ValidatorFn, ValidationErrors } from '@angular/forms';
import * as moment from 'moment';
import 'moment/locale/pt-br';

function isEmptyInputValue(value: any): boolean {
  // we don't check for string here so it also works with arrays
  return value == null || value.length === 0;
}

export class CustomValidators {

  // FormControl Validators

  static emailIsValid(control: FormControl): ValidationErrors | null {
    if (isEmptyInputValue(control.value)) {
      return null;
    }

    let regexp = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/i;
    return regexp.test(control.value) ? null : {
      'invalidEmail': true
    };
  }

  static dateIsValid(control: FormControl): ValidationErrors | null {
    if (isEmptyInputValue(control.value)) {
      return null;
    }

    let dateFormat: string = (control.value.length <= 10) ? 'DD/MM/YYYY' : 'DD/MM/YYYY HH:mm';

    //moment.locale('pt-BR');

    return moment(control.value, dateFormat, true).isValid() ? null : {
      'invalidDate': { valid: false }
    };
  }

  static dateIsoIsValid(control: FormControl): { [key: string]: any } {
    if (isEmptyInputValue(control.value)) {
      return null;
    }

    return moment(control.value).isValid() ? null : {
      'invalidDate': { valid: false }
    };
  }

  static cpfIsValid(control: FormControl): ValidationErrors | null {
    if (isEmptyInputValue(control.value)) {
      return null;
    }

    let a: number;
    let c: string = control.value.replace(/\D+/g, "")
    let e: string = c.substr(0, 9);
    let f: string = c.substr(9, 2);
    let b: number = 0;
    let g: boolean = !0;
    let d: boolean = !1;

    if (11 === c.length) {
      for (a = 0; a < c.length - 1; a++) {
        if (c.charAt(a) != c.charAt(a + 1)) {
          g = !1;
          break;
        }
      }
      if (!g) {
        for (a = 0; 9 > a; a++) {
          b += parseInt(e.charAt(a), 10) * (10 - a);
        }
        b = 11 - b % 11;
        9 < b && (b = 0);
        if (parseInt(f.charAt(0), 10) != b) {
          d = !1;
        } else {
          b *= 2;
          for (a = 0; 9 > a; a++) {
            b += parseInt(e.charAt(a), 10) * (11 - a);
          }
          b = 11 - b % 11;
          9 < b && (b = 0);
          d = parseInt(f.charAt(1), 10) != b ? !1 : !0;
        }
      }
    }

    return d ? null : {
      'invalidCpf': { valid: false }
    };
  }

  static cnpjIsValid(control: FormControl): ValidationErrors | null {
    if (isEmptyInputValue(control.value)) {
      return null;
    }

    let isValid: boolean = false;
    let c: string = control.value.replace(/\D+/g, '');

    if (c.length === 14) {
      var b = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
      if (/0{14}/.test(c)) {
        isValid = false;
      } else {
        for (var i = 0, n = 0; i < 12; n += parseInt(c[i]) * b[++i]);
        if (parseInt(c[12]) != (((n %= 11) < 2) ? 0 : 11 - n)) {
          isValid = false;
        } else {
          for (var i = 0, n = 0; i <= 12; n += parseInt(c[i]) * b[i++]);
          if (parseInt(c[13]) != (((n %= 11) < 2) ? 0 : 11 - n)) {
            isValid = false;
          } else {
            isValid = true;
          }
        }
      }
    }

    return isValid ? null : {
      'invalidCnpj': { valid: false }
    };
  }

  static min(min: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (isEmptyInputValue(control.value) || isEmptyInputValue(min)) {
        return null;
      }
      const value = control.value.parseFloatLocale('pt-BR');

      return !isNaN(value) && value < min ? { 'lowerThan': { 'min': min, 'actual': control.value } } : null;
    };
  }

  static max(max: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (isEmptyInputValue(control.value) || isEmptyInputValue(max)) {
        return null;
      }
      const value = control.value.parseFloatLocale('pt-BR');

      return !isNaN(value) && value > max ? { 'greaterThan': { 'max': max, 'actual': control.value } } : null;
    };
  }

  // FormGroup Validators

  static endDateIsEqualOrGreater(startDate: string, endDate: string): ValidatorFn {
    return (group: FormGroup): ValidationErrors | null => {
      let startDateCtrl = group.controls[startDate];
      let endDateCtrl = group.controls[endDate];

      // Mark group as touched so we can add invalid class easily
      group.markAsTouched();

      //moment.locale('pt-BR');

      if (moment(endDateCtrl.value, 'DD/MM/YYYY', true).toDate() < moment(startDateCtrl.value, 'DD/MM/YYYY', true).toDate()) {
        endDateCtrl.setErrors({ 'minorDate': true });
        return { isValid: false };
      }
    };
  }

  static dateIsGreaterThat(dateCtrlName: string, endDate: Date): ValidatorFn {
    return (group: FormGroup): ValidationErrors | null => {
      let startDateCtrl = group.controls[dateCtrlName];

      // Mark group as touched so we can add invalid class easily
      group.markAsTouched();

      //moment.locale('pt-BR');

      if (moment(startDateCtrl.value, 'DD/MM/YYYY', true).toDate() > moment(endDate, 'DD/MM/YYYY', true).toDate()) {
        startDateCtrl.setErrors({ 'greaterDate': true });
        return { isValid: false };
      }
    };
  }

  static matchPassword(newPassword: string, confirmPassword: string): ValidatorFn {
    return (group: FormGroup): ValidationErrors | null => {
      let password = group.controls[newPassword];
      let confirm = group.controls[confirmPassword];

      // Don't kick in until user touches both fields   
      if (password.pristine || confirm.pristine) {
        return null;
      }

      // Mark group as touched so we can add invalid class easily
      group.markAsTouched();

      if (password.value !== confirm.value) {
        confirm.setErrors({ 'notEquivalent': true });
        return { isValid: false };
      }
    };
  }

  static maxValueIsEqualOrGreater(minValue: string, maxValue: string): ValidatorFn {
    return (group: FormGroup): ValidationErrors | null => {
      let minValueCtrl = group.controls[minValue];
      let maxValueCtrl = group.controls[maxValue];

      if (isEmptyInputValue(minValueCtrl.value) || isEmptyInputValue(maxValueCtrl.value)) {
        return { isValid: true };
      }

      const min = minValueCtrl.value.parseFloatLocale('pt-BR');
      const max = maxValueCtrl.value.parseFloatLocale('pt-BR');

      // Mark group as touched so we can add invalid class easily
      group.markAsTouched();

      if (max < min) {
        minValueCtrl.markAsTouched();
        maxValueCtrl.markAsTouched();

        minValueCtrl.setErrors({ 'minMaxError': true });
        maxValueCtrl.setErrors({ 'minMaxError': true });
        return { isValid: false };
      }
      else {
        if (minValueCtrl.hasError('minMaxError')) {
          minValueCtrl.updateValueAndValidity();
        }

        if (maxValueCtrl.hasError('minMaxError')) {
          maxValueCtrl.updateValueAndValidity();
        }
      }
    };
  }
}

import { MomentDateAdapter } from '@angular/material-moment-adapter';

export const CUSTOM_FORMATS = {
  parse: {
    dateInput: 'DD/MM/YYYY',
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'DD/MM/YYYY',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

export class CustomDateAdapter extends MomentDateAdapter {
  getFirstDayOfWeek(): number {
    return 7;
  }
}

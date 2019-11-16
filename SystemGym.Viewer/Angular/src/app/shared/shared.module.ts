import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from '../material/material.module';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MAT_DATE_FORMATS } from '@angular/material';
import { AutoCompleteComponent } from './auto-complete/auto-complete.component';

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
  declarations: [
    AutoCompleteComponent,
  ],

  providers: [
    { provide: MAT_DATE_FORMATS, useValue: DD_MM_YYYY_Format },
  ],

  imports: [
    CommonModule,
    MaterialModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
  ],
  exports: [
    AutoCompleteComponent,
    CommonModule,
    MaterialModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
  ]
})
export class SharedModule { }

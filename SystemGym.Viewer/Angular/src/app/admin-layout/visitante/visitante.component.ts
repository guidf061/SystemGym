import { Component, OnInit, Pipe } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as moment from 'moment';

import { VisitanteFormService } from './visitante-form/visitante-form.service';
import { Visitante } from '../../models/visitante-model';
import { VisitanteService } from '../../services/visitante.service';


@Component({
  selector: 'app-visitante',
  templateUrl: './visitante.component.html',
  styleUrls: ['./visitante.component.scss']
})
export class VisitanteComponent implements OnInit {

  form: FormGroup;

  visitante: Visitante;

  data: Visitante[];
  dataSource = this.data;

  formSubmited: boolean;

  constructor(private visitanteService: VisitanteService,
    private visitanteFormService: VisitanteFormService,
    private fb: FormBuilder) { }

  displayedColumns: string[] = ['pessoa.nome', 'pessoa.cpf', 'pessoa.email', 'docIdentidade','visitaData', 'edit' ,'del'];

  ngOnInit() {
    this.listar();

  //  this.createFormGroup();
  }

  listar() {
    this.visitanteService.listar().subscribe(rows => this.data = rows);
  }

  editClick(visitante: Visitante) {
    this.visitanteFormService.showDialog(visitante).subscribe();
  }

  deleteClick(visitante: Visitante) {
    this.visitanteService.delete(visitante.visitanteId);
  }

  createClick(): void {
    this.visitanteFormService.showDialog(null).subscribe();
  }
}

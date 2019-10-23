import { Component, OnInit, Pipe } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as moment from 'moment';
import { Colaborador } from '../models/colaborador-model';
import { ColaboradorService } from '../services/colaborador.service';
import { ColaboradorFormService } from './colaborador-form/colaborador-form.service';

@Component({
  selector: 'app-colaborador',
  templateUrl: './colaborador.component.html',
  styleUrls: ['./colaborador.component.css']
})
export class ColaboradorComponent implements OnInit {

  form: FormGroup;

  colaborador: Colaborador;

  data: Colaborador[];
  dataSource = this.data;

  noDataFound: boolean = false;

  formSubmited: boolean;

  constructor(private colaboradorService: ColaboradorService,
    private colaboradorFormService: ColaboradorFormService,
    private fb: FormBuilder) { }

  displayedColumns: string[] = ['pessoa.nome', 'pessoa.cpf', 'pessoa.email', 'pessoa.endereco', 'edit' ,'del'];

  ngOnInit() {
    this.listar();

  //  this.createFormGroup();
  }

  listar() {
    this.colaboradorService.listar().subscribe(rows => this.data = rows);
  }

  editClick(colaborador: Colaborador) {
    this.colaboradorFormService.showDialog(colaborador).subscribe();
  }

  deleteClick(colaborador: Colaborador) {
    this.colaboradorService.delete(colaborador.colaboradorId);
  }

  createClick(): void {
    this.colaboradorFormService.showDialog(null).subscribe();
  }
}

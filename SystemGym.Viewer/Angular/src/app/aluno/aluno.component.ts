import { Component, OnInit, Pipe } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as moment from 'moment';
import { Usuario } from '../models/usuario-model';
import { UsuarioService } from '../services/usuario.service';
import { Aluno } from '../models/aluno-model';
import { AlunoService } from '../services/aluno.service';
import { AlunoFormService } from './aluno-form/aluno-form.service';


@Component({
  selector: 'app-aluno',
  templateUrl: './aluno.component.html',
  styleUrls: ['./aluno.component.css']
})
export class AlunoComponent implements OnInit {

  form: FormGroup;

  aluno: Aluno;

  data: Aluno[];
  dataSource = this.data;

  formSubmited: boolean;

  constructor(private alunoService: AlunoService,
    private alunoFormService: AlunoFormService,
    private fb: FormBuilder) { }

  displayedColumns: string[] = ['pessoa.nome', 'pessoa.cpf', 'pessoa.email', 'numeroCartao', 'edit' ,'del'];

  ngOnInit() {
    this.listar();

  //  this.createFormGroup();
  }

  listar() {
    this.alunoService.listar().subscribe(rows => this.data = rows);
  }

  editClick(aluno: Aluno) {
    this.alunoFormService.showDialog(aluno).subscribe();
  }

  deleteClick(aluno: Aluno) {
    this.alunoService.delete(aluno.alunoId);
  }

  createClick(): void {
    this.alunoFormService.showDialog(null).subscribe();
  }
}

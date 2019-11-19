import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


import { ChartsModule } from 'ng2-charts';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';


import { MatTableModule, MatIconModule, MatDatepickerModule, MatNativeDateModule } from '@angular/material';

import { VisitanteService } from '../services/visitante.service';
import { AlunoService } from '../services/aluno.service';
import { ColaboradorService } from '../services/colaborador.service';

import { MaterialModule } from '../material/material.module';
import { UsuarioService } from '../services/usuario.service';

import { AlunoComponent } from './aluno/aluno.component';
import { UsuarioComponent } from './usuario/usuario.component';
import { UsuarioFormService } from './usuario/usuario-form/usuario-form.service';
import { UsuarioFormComponent } from './usuario/usuario-form/usuario-form.component';
import { AlunoFormComponent } from './aluno/aluno-form/aluno-form.component';
import { ColaboradorFormComponent } from './colaborador/colaborador-form/colaborador-form.component';
import { ColaboradorComponent } from './colaborador/colaborador.component';
import { VisitanteFormComponent } from './visitante/visitante-form/visitante-form.component';
import { VisitanteComponent } from './visitante/visitante.component';
import { ColaboradorFormService } from './colaborador/colaborador-form/colaborador-form.service';
import { VisitanteFormService } from './visitante/visitante-form/visitante-form.service';
import { AlunoFormService } from './aluno/aluno-form/aluno-form.service';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SharedModule } from '../shared/shared.module';
import { AdminLayoutComponent } from './admin-layout.component';
import { AdminLayoutRouting } from './admin-layout.routing';
import { ComponentsModule } from '../components/components.module';
import { AddressService } from '../services/address.service';
import { PagamentoService } from '../services/pagamento.service';
import { PagamentoListService } from './aluno/pagamento/pagamento.service';
import { PagamentoFormService } from './aluno/pagamento/form/pagamento-form.service';
import { PagamentoComponent } from './aluno/pagamento/pagamento.component';
import { PagamentoFormComponent } from './aluno/pagamento/form/pagamento-form.component';
import { CombosListService } from '../services/combosList.service';

@NgModule({
  imports: [
    CommonModule,
    ComponentsModule,
    AdminLayoutRouting,
    FormsModule,
    ChartsModule,
    NgbModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    SharedModule,
    MatIconModule,
    ToastrModule.forRoot()
  ],
  declarations: [
    DashboardComponent,
    UsuarioFormComponent,
    UsuarioComponent,
    AlunoFormComponent,
    AlunoComponent,
    VisitanteComponent,
    VisitanteFormComponent,
    ColaboradorComponent,
    ColaboradorFormComponent,
    AdminLayoutComponent,
    PagamentoComponent,
    PagamentoFormComponent
  ],

  entryComponents: [
    UsuarioComponent,
    UsuarioFormComponent,
    AlunoComponent,
    AlunoFormComponent,
    VisitanteComponent,
    VisitanteFormComponent,
    ColaboradorComponent,
    ColaboradorFormComponent,
    PagamentoComponent,
    PagamentoFormComponent

  ],

  providers: [
    UsuarioFormService,
    UsuarioService,
    ColaboradorService,
    AlunoService,
    VisitanteService,
    AlunoFormService,
    VisitanteFormService,
    ColaboradorFormService,
    AddressService,
    PagamentoService,
    PagamentoListService,
    PagamentoFormService,
    CombosListService
  ],
})

export class AdminLayoutModule {}

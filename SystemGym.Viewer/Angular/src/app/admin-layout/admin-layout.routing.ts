import { Routes, RouterModule } from '@angular/router';

import { UsuarioComponent } from './usuario/usuario.component';
import { VisitanteComponent } from './visitante/visitante.component';
import { ColaboradorComponent } from './colaborador/colaborador.component';
import { AlunoComponent } from './aluno/aluno.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HomeComponent } from '../home/home.component';
import { NgModule } from '@angular/core';
import { AdminLayoutComponent } from './admin-layout.component';

const routes: Routes = [
  {
    path: '',
    component: AdminLayoutComponent,
    children: [
      {
        path: '',
        children: [
          {
            path: 'dashboard',
            component: DashboardComponent
          },
          {
            path: 'aluno',
            component: AlunoComponent
          },
          {
            path: 'colaborador',
            component: ColaboradorComponent
          },
          {
            path: 'visitante',
            component: VisitanteComponent
          },
          {
            path: 'usuario',
            component: UsuarioComponent
          },
        ]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminLayoutRouting { }


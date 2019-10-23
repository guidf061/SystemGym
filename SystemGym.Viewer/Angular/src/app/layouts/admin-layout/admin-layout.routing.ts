import { Routes } from '@angular/router';

import { DashboardComponent } from '../../dashboard/dashboard.component';

import { AlunoComponent } from '../../aluno/aluno.component';
import { UsuarioComponent } from '../../usuario/usuario.component';
import { VisitanteComponent } from '../../visitante/visitante.component';
import { ColaboradorComponent } from '../../colaborador/colaborador.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard',      component: DashboardComponent },
    { path: 'aluno',          component: AlunoComponent },
    { path: 'usuario',          component: UsuarioComponent },
    { path: 'visitante', component: VisitanteComponent },
    { path: 'colaborador', component: ColaboradorComponent },
];

import { Routes } from '@angular/router';

import { UsuarioComponent } from './usuario/usuario.component';
import { VisitanteComponent } from './visitante/visitante.component';
import { ColaboradorComponent } from './colaborador/colaborador.component';
import { AlunoComponent } from './aluno/aluno.component';
import { DashboardComponent } from './dashboard/dashboard.component';


export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard',      component: DashboardComponent },
    { path: 'aluno',          component: AlunoComponent },
    { path: 'usuario',          component: UsuarioComponent },
    { path: 'visitante', component: VisitanteComponent },
    { path: 'colaborador', component: ColaboradorComponent },
];

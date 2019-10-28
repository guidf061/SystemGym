import { Component, OnInit } from '@angular/core';

declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}
export const ROUTES: RouteInfo[] = [
  { path: '/dashboard', title: 'Dashboard', icon: 'business_chart-bar-32', class: '' },
  { path: '/usuario', title: 'Usuario', icon: 'users_single-02', class: '' },
  { path: '/aluno', title: 'Aluno', icon: 'sport_user-run', class: '' },
  { path: '/visitante', title: 'Visitante', icon: 'emoticons_satisfied', class: '' },
  { path: '/colaborador', title: 'Colaborador', icon: 'business_badge', class: '' },

];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  menuItems: any[];

  constructor() { }

  ngOnInit() {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
  }
  isMobileMenu() {
      if ( window.innerWidth > 991) {
          return false;
      }
      return true;
  };
}

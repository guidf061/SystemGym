import { Component, OnInit, OnDestroy, HostListener, ViewEncapsulation } from '@angular/core';
import { Router } from "@angular/router";
import { LoginService } from '../core/login.service';
import { AuthService } from '../core/tools/auth.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {

  constructor(private loginService: LoginService,
    private authService: AuthService,
    private router: Router) {
  }

  ngOnInit() {
  }

  openLogin() {
    if (!this.authService.loggedIn()) {
      this.loginService.showDialog().subscribe(logado => {
        if (logado) {
          this.router.navigate(['/admin-layout/dashboard/dashboard.component']);
        }
      });
    }
    else {
      this.router.navigate(['/admin/produto']);
    }
  }
}

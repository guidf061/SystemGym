/// <reference path="../alert-dialog.service.ts" />
import { Injectable } from '@angular/core';
import {
  CanActivate, Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  CanActivateChild,
  NavigationExtras,
  CanLoad, Route
} from '@angular/router';
import { Platform } from '@angular/cdk/platform';

@Injectable()
export class MobileGuard implements CanActivate, CanActivateChild, CanLoad {

  constructor(private router: Router,
    private platform: Platform) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let url: string = state.url;

    return this.checkLogin(url, route);
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return this.canActivate(route, state);
  }

  canLoad(route: Route): boolean {
    let url = `/${route.path}`;

    return this.checkLogin(url, null);
  }

  private checkLogin(url: string, route: ActivatedRouteSnapshot): boolean {

    let app: boolean = document.URL.indexOf('http://') === -1 && document.URL.indexOf('https://') === -1;

    if (app) {
      if (url === '/home') {
        if (!String.isNullOrEmpty(localStorage.getItem('auth_token'))) {
          this.router.navigate(['/process/search']);
        }
        return false;
      }
    }

    return true;
  }
}

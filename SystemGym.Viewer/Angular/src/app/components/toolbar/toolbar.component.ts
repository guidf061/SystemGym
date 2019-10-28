import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { Subscription } from 'rxjs';
import { OverlayContainer } from '@angular/cdk/overlay';

import {
  AuthService,
  LoggedUserService,
  NavbarService,
  SidenavMode,
  ToolbarService
} from '@app/core';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit, OnDestroy {
  private subscriptionLogIn: Subscription;
  private subscriptionLogOut: Subscription;
  private titleSubscription: Subscription;
  private colorSubscription: Subscription;
  private showSearchSubscription: Subscription;

  @Input() title: string = '';
  @Input('dark') dark: boolean = false;
  @Output() darkChange: EventEmitter<boolean> = new EventEmitter<boolean>();

  toolbarColor: string = 'primary';
  showSearch: boolean = false;
  showUserInfo: boolean = false;
  logo: string = './assets/img/logo-top.png';

  constructor(private navbarService: NavbarService,
    private authService: AuthService,
    private toolbarService: ToolbarService,
    private loggedUserService: LoggedUserService,
    private overlayContainer: OverlayContainer) {
  }

  openNavbar(): void {
    this.navbarService.toggleSidenav();
  }

  ngOnInit(): void {
    this.title = '';

    this.toggleLogo();

    this.titleSubscription = this.toolbarService.changeTitle$.subscribe(
      title => {
        this.title = title;
      });

    this.showSearchSubscription = this.toolbarService.showSearch$.subscribe(
      show => {
        this.showSearch = show;
      });

    this.colorSubscription = this.toolbarService.changeColor$.subscribe(
      color => {
        this.toolbarColor = color;
        this.toggleLogo();
      });

    this.subscriptionLogOut = this.authService.logOut$.subscribe(() => {
      this.showUserInfo = false;
    });

    this.subscriptionLogIn = this.authService.logIn$.subscribe(() => {
      this.showUserInfo = true;
    });

    if (this.loggedUserService.loggerUser !== undefined && this.loggedUserService.loggerUser != null && !String.isNullOrEmpty(this.loggedUserService.loggerUser.userId)) {
      this.showUserInfo = true;
    }
  }

  toggleTheme(): void {
    this.dark = !this.dark
    this.darkChange.emit(this.dark);

    if (this.dark) {
      this.overlayContainer.getContainerElement().classList.add('dark-theme');
    }
    else {
      this.overlayContainer.getContainerElement().classList.remove('dark-theme');
    }

    this.toggleLogo();

    localStorage.setItem('dark-theme', this.dark ? 'Y' : 'N');
  }

  toggleLogo(): void {
    this.logo = (this.toolbarColor === 'primary' || this.dark) ? './assets/img/logo-top-white.png' : './assets/img/logo-top.png';
  }

  ngOnDestroy(): void {
    if (this.titleSubscription != undefined) {
      this.titleSubscription.unsubscribe();
    }

    if (this.colorSubscription != undefined) {
      this.colorSubscription.unsubscribe();
    }

    if (this.subscriptionLogIn !== undefined) {
      this.subscriptionLogIn.unsubscribe();
    }

    if (this.subscriptionLogOut !== undefined) {
      this.subscriptionLogOut.unsubscribe();
    }

    if (this.showSearchSubscription !== undefined) {
      this.showSearchSubscription.unsubscribe();
    }
  }
}

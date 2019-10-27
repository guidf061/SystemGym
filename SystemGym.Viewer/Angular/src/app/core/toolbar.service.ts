import { Injectable } from '@angular/core';
import { Subject,  Observable } from 'rxjs';

@Injectable()
export class ToolbarService {
  private changeTitleSource = new Subject<string>();
  public changeTitle$ = this.changeTitleSource.asObservable();

  private changeColorSource = new Subject<string>();
  public changeColor$ = this.changeColorSource.asObservable();

  private showSearchSource = new Subject<boolean>();
  public showSearch$ = this.showSearchSource.asObservable();

  changeTitle(title: string) {
    this.changeTitleSource.next(title);
  }

  changeColor(color: string) {
    this.changeColorSource.next(color);
  }

  showSearch(show: boolean) {
    this.showSearchSource.next(show);
  }
}

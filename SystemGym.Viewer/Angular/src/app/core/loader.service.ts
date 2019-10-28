import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable()
export class LoaderService {

  private showSource = new Subject<boolean>();
  public show$ = this.showSource.asObservable();

  show(instantly: boolean = false) {
    this.showSource.next(instantly);
  }

  private hideSource = new Subject<boolean>();
  public hide$ = this.hideSource.asObservable();

  hide() {
    this.hideSource.next(true);
  }
}

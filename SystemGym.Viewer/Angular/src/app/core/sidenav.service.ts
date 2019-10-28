import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SidenavService {
  private toggleSource = new Subject();
  public toggle$ = this.toggleSource.asObservable();

  private modeSource = new Subject<'over' | 'push' | 'side'>();
  public mode$ = this.modeSource.asObservable();

  toggle() {
    this.toggleSource.next();
  }

  changeMode(mode: 'over' | 'push' | 'side') {
    this.modeSource.next(mode);
  }
}

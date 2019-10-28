import { Component, OnInit, Input, Output, ChangeDetectorRef } from '@angular/core';
import { LoaderService } from '../../core';


@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.scss']
})
export class LoaderComponent implements OnInit {
  visible: boolean;
  showTimeout: any;
  spinnerDiameter: number = 100;
  spinnerStrokeWidth: number = 10;

  constructor(private loaderService: LoaderService, private changeDetectorRef: ChangeDetectorRef) {
    loaderService.show = this.show.bind(this);
    loaderService.hide = this.hide.bind(this);
  }

  show(instantly: boolean): void {
    this.spinnerDiameter = 100;
    this.spinnerStrokeWidth = 10;

    if (document.documentElement.clientWidth < 600) {
      this.spinnerDiameter = 50;
      this.spinnerStrokeWidth = 5;
    }

    if (!instantly) {
      if (!this.visible && this.showTimeout === undefined) {
        this.showTimeout = setTimeout(() => {
          this.visible = true;
        }, 700);
      }
    }
    else {
      this.visible = true;
    }
  }

  hide(): void {
    if (this.showTimeout !== undefined && this.showTimeout !== null) {
      clearTimeout(this.showTimeout);
      this.showTimeout = undefined;
    }

    if (this.visible) {
      setTimeout(() => {
        this.visible = false;
        //this.changeDetectorRef.detectChanges();
      }, 400);
    }
  }

  ngOnInit(): void {
    this.visible = false;
  }
}

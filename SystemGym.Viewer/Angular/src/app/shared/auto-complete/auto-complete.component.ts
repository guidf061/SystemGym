import {
  Component,
  ElementRef,
  Renderer,
  Input,
  Output,
  OnInit,
  ViewEncapsulation,
  EventEmitter,
  ViewChild
} from '@angular/core';

import { HttpClient, HttpHeaders } from "@angular/common/http";
import { CustomErrorHandler } from '../../core';


export const OPTION_HEIGHT = 48;
export const PANEL_HEIGHT = 240;

@Component({
  selector: 'app-auto-complete',
  templateUrl: './auto-complete.component.html',
  styleUrls: ['./auto-complete.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AutoCompleteComponent implements OnInit {

  @Input('displayPropertyName') displayPropertyName: string = 'value';
  @Input('keyword') keyword: string = '';
  @Input('listFormatter') listFormatter: (arg: any) => string;
  @Input('maxNumList') maxNumList: number;
  @Input('minChars') minChars: number = 0;
  @Input('placeHolder') placeholder: string = '';
  @Input('removeIfInvalid') removeIfInvalid: boolean = true;
  @Input('required') required: boolean = false;
  @Input('serviceUrl') serviceUrl: string;
  @Input('showDelay') showDelay: number = 0;
  @Input('source') source: any;
  @Input('tabindex') tabIndex: number;
  @Input('valuePropertyName') valuePropertyName;
  @Input('width') width: string;
  @Input('disabled') disabled: boolean = false;
  @Input('noBorder') noBorder: boolean;
  @Output() keywordChange: EventEmitter<string> = new EventEmitter<string>();

  @Output() valueSelected = new EventEmitter();

  dropdownVisible: boolean = false;

  filteredList: any[] = [];
  itemIndex: number = 0;
  selectedData: any = null;

  private movedToBody: boolean = false;
  @ViewChild('autoCompleteDiv', { static: true }) autoCompleteDiv: ElementRef;
  @ViewChild('autoCompleteOptionsDiv', { static: false }) autoCompleteOptionsDiv: ElementRef;

  constructor(private http: HttpClient, private renderer: Renderer) {
  }

  ngOnInit(): void {
  }

  showDropdownList(): void {
    setTimeout(() => {
      this.reloadList().then(() => {
        if (this.keyword !== undefined && this.keyword !== null && this.keyword !== '') {
          if (this.filteredList.length > 0) {
            this.selectOne(this.filteredList[0]);
          }
        }
      });
    }, this.showDelay);
  }

  hideDropdownList(): void {
    this.dropdownVisible = false;
    this.movedToBody = false;

    if (String.isNullOrEmpty(this.keyword)) {
      this.filteredList = [];
      this.selectedData = null;
    }
    else if ((this.selectedData.name !== undefined && this.selectedData.name !== null && this.selectedData.name !== '') || this.keyword !== this.selectedData[this.displayPropertyName]) {
      if (this.removeIfInvalid) {
        this.keyword = '';
      }

      this.selectedData = null;
      this.valueSelected.emit(null);
    }
  }

  reloadListInDelay(): void {
    let delayMs = 350;

    // executing after user stopped typing
    this.delay(() => this.reloadList(), delayMs);

    this.keywordChange.emit(this.keyword);
  }

  reloadList(): Promise<boolean> {
    return new Promise<boolean>((resolve, reject) => {
      if (this.keyword !== undefined && this.keyword !== null && this.keyword.length >= (this.minChars || 0)) {
        if (this.serviceUrl !== undefined && this.serviceUrl !== null) {
          console.log(this.keyword);
          let url: string = this.serviceUrl;
          url += this.serviceUrl.indexOf('?') === -1 ? '' + this.keyword : this.keyword;
          this.http.get<any>(url)
            .toPromise()
            .then(res => {

              this.source = res.items ? res.items : res;

              this.filteredList = this.source; //this.filter(this.source, this.keyword);
              if (this.maxNumList) {
                this.filteredList = this.filteredList.slice(0, this.maxNumList);
              }

              this.dropdownVisible = true;
              this.setOptionsPositionTimeOut();
              return resolve(true);
            })
            .catch(error => {
              //this.handleError(error);
              return resolve(false);
            });
        }
        else if (this.source !== undefined) {
          // local source
          this.filteredList = this.filter(this.source, this.keyword);
          if (this.maxNumList) {
            this.filteredList = this.filteredList.slice(0, this.maxNumList);
          }

          this.dropdownVisible = true;
          this.setOptionsPositionTimeOut();
          return resolve(true);
        }
      } else if (this.minChars === 0) {
        this.filteredList = this.source || [];
        this.dropdownVisible = true;
        this.setOptionsPositionTimeOut();
        return resolve(true);
      }
      else {
        this.dropdownVisible = true;
        this.setOptionsPositionTimeOut();
      }
    });
  }

  selectOne(data: any) {
    if (data !== undefined) {
      this.selectedData = data;
      this.keyword = data[this.displayPropertyName];

      if (this.valuePropertyName !== undefined && this.valuePropertyName !== null && this.valuePropertyName !== '') {
        this.valueSelected.emit(data[this.valuePropertyName]);
      }
      else {
        this.valueSelected.emit(data);
      }
    }

    this.hideDropdownList();
  };

  inputElKeyHandler(evt: any) {
    let totalNumItem = this.filteredList.length;
    switch (evt.keyCode) {
      case 27: // ESC, hide auto complete
        this.hideDropdownList();
        break;

      case 38: // UP, select the previous li el
        this.itemIndex = (totalNumItem + this.itemIndex - 1) % totalNumItem;
        this.scrollToOption();
        break;

      case 40: // DOWN, select the next li el or the first one
        this.dropdownVisible = true;
        this.itemIndex = (totalNumItem + this.itemIndex + 1) % totalNumItem;
        this.scrollToOption();
        break;

      case 13: // ENTER, choose it!!
        if (this.dropdownVisible) {
          if (this.filteredList.length > 0) {
            this.selectOne(this.filteredList[this.itemIndex]);
          }
          evt.preventDefault();
        }
        break;
      case 9: // TAB, choose it!!
        if (this.filteredList.length > 0 && !String.isNullOrEmpty(this.keyword)) {
          this.selectOne(this.filteredList[this.itemIndex]);
        }
        break;
    }
  };

  getFormattedList(data: any): string {
    let formatter = this.listFormatter || this.defaultListFormatter;
    return formatter.apply(this, [data]);
  }

  private setOptionsPositionTimeOut() {
    if (!this.movedToBody) {
      setTimeout(() => {
        this.setOptionsPosition();
      }, 0);
    }
    else {
      this.setOptionsPosition();
    }
  }

  private setOptionsPosition() {
    if (this.autoCompleteDiv != undefined && this.autoCompleteOptionsDiv != undefined) {
      let parentTop = (this.autoCompleteDiv.nativeElement as HTMLDivElement).getBoundingClientRect().top;
      let parentLeft = (this.autoCompleteDiv.nativeElement as HTMLDivElement).getBoundingClientRect().left;
      let parentWidth = (this.autoCompleteDiv.nativeElement as HTMLDivElement).clientWidth;

      let optionsTop: number = parentTop;
      let optionsHeight: number = (this.filteredList.length * OPTION_HEIGHT) > PANEL_HEIGHT ? PANEL_HEIGHT : (this.filteredList.length * OPTION_HEIGHT);
      let windowsHeight: number = document.documentElement.clientHeight;

      if ((parentTop + 300) > windowsHeight) {
        optionsTop -= optionsHeight;
      }
      else {
        optionsTop += 29;
      }

      (this.autoCompleteOptionsDiv.nativeElement as HTMLDivElement).style.top = optionsTop + 'px';
      (this.autoCompleteOptionsDiv.nativeElement as HTMLDivElement).style.left = parentLeft + 'px';
      (this.autoCompleteOptionsDiv.nativeElement as HTMLDivElement).style.width = (!this.width ? parentWidth + 'px' : this.width);

      if (!this.movedToBody) {
        let overlayContainer = document.getElementsByClassName('cdk-overlay-container');
        let autoCompleteOptions = document.getElementsByClassName('auto-complete-options');

        if (overlayContainer.length > 0 && autoCompleteOptions.length > 0) {
          overlayContainer[0].childNodes.forEach(x => {
            if ((<HTMLDivElement>x).classList.contains('mat-dialog-container')) {
              overlayContainer[0].removeChild(x);
            }
          });

          overlayContainer[0].appendChild(autoCompleteOptions[0]);
          this.movedToBody = true;
        }

        //$(".auto-complete-options").appendTo(".cdk-overlay-container");
        //this.movedToBody = true;
      }
    }
  }

  private scrollToOption(): void {
    const optionOffset = this.itemIndex * OPTION_HEIGHT;
    const panelTop = this.getScrollTop();

    if (optionOffset < panelTop) {
      this.setScrollTop(optionOffset);
    } else if (optionOffset + OPTION_HEIGHT > panelTop + PANEL_HEIGHT) {
      const newScrollTop = Math.max(0, optionOffset - PANEL_HEIGHT + OPTION_HEIGHT);
      this.setScrollTop(newScrollTop);
    }
  }

  private setScrollTop(scrollTop: number): void {
    if (this.autoCompleteOptionsDiv) {
      this.autoCompleteOptionsDiv.nativeElement.scrollTop = scrollTop;
    }
  }

  private getScrollTop(): number {
    return this.autoCompleteOptionsDiv ? this.autoCompleteOptionsDiv.nativeElement.scrollTop : 0;
  }

  private defaultListFormatter(data: any): string {
    let html: string = '';
    html += data[this.displayPropertyName] ? `<span>${data[this.displayPropertyName]}</span>` : data;
    return html;
  }

  private filter(list: any[], keyword: string) {
    keyword = keyword.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, '\\$&');
    return list.filter(x => x[this.displayPropertyName].match(new RegExp(`${keyword}`, 'gi')));
  }

  private delay = (function () {
    let timer = 0;
    return function (callback: any, ms: number) {
      clearTimeout(timer);
      timer = setTimeout(callback, ms);
    };
  })();

  private handleError(error: any) {
    let errMsg = CustomErrorHandler.extractMessage(error);
    return Promise.reject(errMsg);
  }
}

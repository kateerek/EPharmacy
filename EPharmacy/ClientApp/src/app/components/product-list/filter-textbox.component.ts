import {Component, OnInit, Input, Output, EventEmitter} from '@angular/core'

@Component({
  selector: 'filter-textbox',
  template: `
          Wyszukaj: <input type="text" [(ngModel)]="filter" />
  `

})

export class FilterTextboxComponent implements OnInit{

  private _filter: string;

  @Input() set filter(val: string){
    this._filter = val;
    this.changed.emit(this.filter);
  }

  get filter(){
    return this._filter;
  }



  @Output() changed: EventEmitter<string> = new EventEmitter<string>();

  constructor(){}
  ngOnInit() {
  }
}

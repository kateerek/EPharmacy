import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: []
})
export class OrderComponent implements OnInit {
  @Input() order;
  readonly nullDate = new Date("0000-12-31T22:36:00.000Z");
  constructor() {
   }

  ngOnInit() {
  }

  formatDate(date: Date) {
    var month: any = date.getUTCMonth() + 1; //months from 1-12
    var day: any = date.getUTCDate();
    var year = date.getUTCFullYear();
    if (day < 10) {
      day = '0' + day;
    }
    if (month < 10) {
      month = '0' + month;
    }
    return day + "." + month + "." + year;
  }
}

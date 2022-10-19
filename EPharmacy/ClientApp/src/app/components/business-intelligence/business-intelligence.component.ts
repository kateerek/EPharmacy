import { Component, OnInit } from '@angular/core';
import { BusinessIntelligenceClient,
  AttributeResponseModel,
  ProductShortModel} from '../../api/epharmacy';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-business-intelligence',
  templateUrl: './business-intelligence.component.html',
  styleUrls: ['./business-intelligence.component.css']
})
export class BusinessIntelligenceComponent implements OnInit {
  bestProducts: ProductShortModel[] = [];
  bestAttribute: AttributeResponseModel[] = [];
  types: string[] = [];
  selectedValue: string;
  date = new Date(Date.now());
  constructor( private bi: BusinessIntelligenceClient) { }
  barChart: Chart;
  labels: string[] = [];
  data: number[] = [];
  title: string;

  ngOnInit() {
    this.types = ['Najlepiej sprzedające się produkty', 'Najlepiej sprzedające się kategorie',
                  'Najlepiej sprzedające się promocje', 'Najlepiej sprzedające się refundacje',
                  'Najwięcej sprzedające lokalizacje aptek'   ];
   }
  createChart() {
    if ( this.barChart !== undefined ) {
      this.barChart.destroy();
    }
    this.barChart = new Chart('barChart', {
      type: 'bar',
    data: {
     labels: this.labels,
     datasets: [{
         data: this.data,
         backgroundColor: [
             'rgba(255, 99, 132, 0.2)',
             'rgba(54, 162, 235, 0.2)',
             'rgba(255, 206, 86, 0.2)',
             'rgba(75, 192, 192, 0.2)',
             'rgba(153, 102, 255, 0.2)',
         ],
         borderColor: [
             'rgba(255,99,132,1)',
             'rgba(54, 162, 235, 1)',
             'rgba(255, 206, 86, 1)',
             'rgba(75, 192, 192, 1)',
             'rgba(153, 102, 255, 1)',
         ],
         borderWidth: 1
     }]
    },
    options: {
      legend: {
        display: false
      },
     title: {
         text: this.title,
         display: true
     },
     scales: {
         yAxes: [{
             ticks: {
              beginAtZero: true
             }
         }],
         xAxes: [{
          ticks: {
            autoSkip: false
           }
         }]
     }
    }
    });
  }

getData(from: Date, to: Date) {
  if (from < to) {
    this.labels = [];
    this.data = [];
    if (this.selectedValue === this.types[0]) {
     this.getBestSellingProducts(from, to);
    }
    if (this.selectedValue === this.types[1]) {
      this.getBestSellingAttributes(from, to);
    }
    if (this.selectedValue === this.types[2]) {
      this.getBestSellingOfferDiscounts(from, to);
    }
    if (this.selectedValue === this.types[3]) {
      this.getBestSellingPrescriptionDiscounts(from, to);
    }
    if (this.selectedValue === this.types[4]) {
      this.getBestSellingByPharmacyLocation(from, to);
    }
  }
}
getBestSellingProducts(from: Date, to: Date) {
  this.bi.getBestSellingProducts(from, to, 5).subscribe(
    data => {
      data.values.forEach(element => {
       this.data.push(element.count);
       this.labels.push('ID:' + element.model.id.toString() + ', ' + element.model.name.slice(0, 10) + '...');
      });
      this.title = this.selectedValue;
      this.createChart();
    }
  );
}
getBestSellingAttributes(from: Date, to: Date) {
  this.bi.getBestSellingAttributes(from, to, 5).subscribe(
    data => {
      data.values.forEach(element => {
       this.data.push(element.count);
       this.labels.push('ID:' + element.model.id.toString() + ', ' + element.model.name.slice(0, 10) + '...');
      });
      this.title = this.selectedValue;
      this.createChart();
    }
  );
}
getBestSellingByPharmacyLocation(from: Date, to: Date) {
  this.bi.getBestSellingByPharmacyLocation(from, to, 5).subscribe(
    data => {
      data.values.forEach(element => {
       this.data.push(element.count);
       this.labels.push('ID:' + element.model.id.toString() + ', ' + element.model.name.slice(0, 10) + '...');
      });
      this.title = this.selectedValue;
      this.createChart();
    }
  );
}
getBestSellingOfferDiscounts(from: Date, to: Date) {
  this.bi.getBestSellingOfferDiscounts(from, to, 5).subscribe(
    data => {
      data.values.forEach(element => {
       this.data.push(element.count);
       this.labels.push('ID:' + element.model.id.toString() + ', ' + element.model.name.slice(0, 10) + '...');
      });
      this.title = this.selectedValue;
      this.createChart();
    }
  );
}
getBestSellingPrescriptionDiscounts(from: Date, to: Date) {
  this.bi.getBestSellingPrescriptionDiscounts(from, to, 5).subscribe(
    data => {
      data.values.forEach(element => {
       this.data.push(element.count);
       this.labels.push('ID:' + element.model.id.toString() + ', ' + element.model.name.slice(0, 10) + '...');
      });
      this.title = this.selectedValue;
      this.createChart();
    }
  );
}
getDay(): String {
  const date = this.date.toLocaleDateString().split('.', 3);
  return date[2] + '-' + date[1] + '-' + date[0];
}
onClick(value) {
  this.getData( new Date(value.datepickerFrom), new Date(value.datepickerTo));
}

}

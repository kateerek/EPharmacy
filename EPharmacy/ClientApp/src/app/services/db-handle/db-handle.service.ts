import { Injectable } from '@angular/core';
import { of } from 'rxjs/observable/of';

@Injectable()
export class DbHandleService {

  productsJson = [
      {
        'id' : '1',
        'name' : 'Doctor Life, Special C 1000mg, Kwercetyna, 50 tabletek',
        'price' : 59.89,
        'picture' : 'https://www.aptekagemini.pl/product/image/219499/55505.jpg',
      },
      {
        'id' : '2',
        'name' : 'Ceviforte C 1000, 30 kapsułek',
        'price' : 7.99,
        'picture' : 'https://www.aptekagemini.pl/product/image/292589/9026842_9.jpg',
      },
      {
        'id' : '3',
        'name' : 'Cholinex 150mg, bez cukru, 16 pastylek do ssania',
        'price' : 8.89,
        'picture' : 'https://www.aptekagemini.pl/product/image/232126/2110.jpg',
      }
      ,
      {
        'id' : '4',
        'name' : 'Rutinoscorbin 25mg+100mg, 90 tabletek',
        'price' : 7.24,
        'picture' : 'https://www.aptekagemini.pl/product/image/87421/1526281106apteka_internetowa_gemini_rutinoscorbin_90_tabletek.jpg',
      }
      ,
      {
        'id' : '5' ,
        'name' : 'Apap Noc 500mg+25mg, 6 tabletek',
        'price' : 3.99,
        'picture' : 'https://www.aptekagemini.pl/product/image/292895/1524846648apteka_internetowa_gemini_apap_noc_6_tabletek.jpg',
      }
    ];
  typesJson = [
      {
        'id' : 1,
        'name' : 'witaminy',
      },
      {
        'id' : 2,
        'name' : 'ból gardła',
      },
      {
        'id' : 3,
        'name' : 'gorączka',
      },
      {
        'id' : 4,
        'name' : 'kaszel',
      }
    ];
  constructor() { }

  getTypes() {
    return of(this.typesJson);
  }
  getProductsPage(offset, typeKey, startKey?) {
    return of(this.productsJson);
   }
   getProduct(id) {
    return of(this.productsJson.find(e => e.id === id));
  }
}

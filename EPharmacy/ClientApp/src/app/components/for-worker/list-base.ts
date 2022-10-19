import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Observable';
import { OnDestroy } from '@angular/core';

export class ListBase<T> implements OnDestroy {
  protected values: T[] = [];
  valuesOnPage: T[] = [];
  pageList: number[];
  offset = 10;
  page = 1;
  subscription: Subscription;

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  getPageNumber() {
    return this.values.length / this.offset;
  }

  isPaginationVisible() {
    return this.getPageNumber() > 1;
  }

  setPageArray() {
    this.pageList = [];
    for (let i = 0; i < this.getPageNumber(); i++) {
      this.pageList.push(i + 1);
    }
  }

  getLast(value: number) {
    return (value + this.offset > this.values.length - 1) ? this.values.length : (value + this.offset);
  }

  setPage(value: number) {
    this.page = value;
    this.valuesOnPage = this.values.slice((this.page - 1) * this.offset, this.getLast((this.page - 1) * this.offset));
  }

  nextPage() {
    if (this.page < this.getPageNumber()) {
      this.setPage(this.page + 1);
    }
  }

  prevPage() {
    if (this.page - 1 !== 0) {
      this.setPage(this.page - 1);
    }
  }

  getValues(observable: Observable<T[]>) {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
    this.subscription =
      observable.subscribe(values => {
        if (values !== null) {
          this.values = values;
          this.setPage(1);
        }
        else {
          this.values = [];
          this.valuesOnPage = [];
        }
        this.setPageArray();
        this.page = 1;
      });
  }
}

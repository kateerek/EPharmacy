import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductTypesListComponent } from './product-types-list.component';

describe('ProductTypesListComponent', () => {
  let component: ProductTypesListComponent;
  let fixture: ComponentFixture<ProductTypesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductTypesListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductTypesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

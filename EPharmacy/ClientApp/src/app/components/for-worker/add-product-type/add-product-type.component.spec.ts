import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddProductTypeComponent } from './add-product-type.component';

describe('AddProductTypeComponent', () => {
  let component: AddProductTypeComponent;
  let fixture: ComponentFixture<AddProductTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddProductTypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddProductTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

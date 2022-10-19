import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RefundationInfoComponent } from './refundation-info.component';

describe('RefundationInfoComponent', () => {
  let component: RefundationInfoComponent;
  let fixture: ComponentFixture<RefundationInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RefundationInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RefundationInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

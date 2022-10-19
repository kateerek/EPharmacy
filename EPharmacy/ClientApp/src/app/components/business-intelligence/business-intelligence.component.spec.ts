import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BusinessIntelligenceComponent } from './business-intelligence.component';

describe('BusinessIntelligenceComponent', () => {
  let component: BusinessIntelligenceComponent;
  let fixture: ComponentFixture<BusinessIntelligenceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BusinessIntelligenceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BusinessIntelligenceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

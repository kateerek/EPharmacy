import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ValidationErrorsDisplayComponent } from './validation-errors-display.component';

describe('ValidationErrorsDisplayComponent', () => {
  let component: ValidationErrorsDisplayComponent;
  let fixture: ComponentFixture<ValidationErrorsDisplayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ValidationErrorsDisplayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ValidationErrorsDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormInputElementComponent } from './form-input-element.component';

describe('FormInputElementComponent', () => {
  let component: FormInputElementComponent;
  let fixture: ComponentFixture<FormInputElementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormInputElementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormInputElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

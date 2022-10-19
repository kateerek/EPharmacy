import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditProducerComponent } from './edit-producer.component';

describe('EditProducerComponent', () => {
  let component: EditProducerComponent;
  let fixture: ComponentFixture<EditProducerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditProducerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditProducerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

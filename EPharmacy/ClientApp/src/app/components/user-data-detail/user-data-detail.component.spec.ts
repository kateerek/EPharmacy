import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserDataDetailComponent } from './user-data-detail.component';

describe('UserDataDetailComponent', () => {
  let component: UserDataDetailComponent;
  let fixture: ComponentFixture<UserDataDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserDataDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserDataDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

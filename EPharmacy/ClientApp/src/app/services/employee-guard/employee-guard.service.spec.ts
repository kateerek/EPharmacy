import { TestBed, inject } from '@angular/core/testing';

import { EmployeeGuardService } from './employee-guard.service';

describe('EmployeeGuardService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EmployeeGuardService]
    });
  });

  it('should be created', inject([EmployeeGuardService], (service: EmployeeGuardService) => {
    expect(service).toBeTruthy();
  }));
});

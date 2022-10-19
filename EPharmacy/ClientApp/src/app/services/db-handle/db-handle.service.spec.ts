import { TestBed, inject } from '@angular/core/testing';

import { DbHandleService } from './db-handle.service';

describe('DbHandleService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DbHandleService]
    });
  });

  it('should be created', inject([DbHandleService], (service: DbHandleService) => {
    expect(service).toBeTruthy();
  }));
});

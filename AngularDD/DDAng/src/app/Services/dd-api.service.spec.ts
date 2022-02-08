import { TestBed } from '@angular/core/testing';

import { DdApiService } from './dd-api.service';

describe('DdApiService', () => {
  let service: DdApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DdApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

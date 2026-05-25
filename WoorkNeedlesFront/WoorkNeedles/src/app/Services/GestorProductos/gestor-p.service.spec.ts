import { TestBed } from '@angular/core/testing';

import { GestorPService } from './gestor-p.service';

describe('GestorPService', () => {
  let service: GestorPService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GestorPService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { CategoriaInsumoService } from './categoria-insumo.service';

describe('CategoriaInsumoService', () => {
  let service: CategoriaInsumoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CategoriaInsumoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

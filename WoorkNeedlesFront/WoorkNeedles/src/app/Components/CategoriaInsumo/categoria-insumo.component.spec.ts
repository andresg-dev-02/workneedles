import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriaInsumoComponent } from './categoria-insumo.component';

describe('CategoriaInsumoComponent', () => {
  let component: CategoriaInsumoComponent;
  let fixture: ComponentFixture<CategoriaInsumoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CategoriaInsumoComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CategoriaInsumoComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

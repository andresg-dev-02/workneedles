import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GestorPComponent } from './insumo-producto.component';

describe('GestorPComponent', () => {
  let component: GestorPComponent;
  let fixture: ComponentFixture<GestorPComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GestorPComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(GestorPComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

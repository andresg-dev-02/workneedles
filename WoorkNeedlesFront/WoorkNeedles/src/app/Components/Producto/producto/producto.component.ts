import { Component, OnInit } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { ProductoService } from '../../../Services/services/producto.service';
import { ProductoModel } from '../../../Models/Producto/producto.model';

@Component({
  selector: 'app-producto',
  standalone: true,
  imports: [CommonModule, CurrencyPipe],
  template: `
    <!-- Estado de carga -->
    <div *ngIf="cargando" class="flex justify-center p-8">
      <span class="loading loading-spinner loading-lg"></span>
    </div>

    <!-- Error -->
    <div *ngIf="error" class="alert alert-error">
      {{ error }}
    </div>

    <!-- Lista de productos -->
    <div class="grid grid-cols-3 gap-4" *ngIf="!cargando">
      <div class="card shadow-md" *ngFor="let producto of productos">
        
        <figure *ngIf="producto.urlimagen">
          <img [src]="producto.urlimagen" [alt]="producto.nombre" />
        </figure>

        <div class="card-body">
          <h2 class="card-title">{{ producto.nombre }}</h2>
          <p>{{ producto.descripcion }}</p>
          <p *ngIf="producto.material">Material: {{ producto.material }}</p>
          <p *ngIf="producto.genero">Género: {{ producto.genero }}</p>
          <span class="badge">{{ producto.categoria }}</span>
          <p class="font-bold">{{ producto.preciobase | currency:'COP' }}</p>
        </div>

      </div>
    </div>
  `
})

export class ProductoComponent implements OnInit {
  productos: ProductoModel[] = [];
  cargando = false;
  error = '';

  constructor(private productoService: ProductoService) {}

  ngOnInit() {
    this.cargarProductos();
  }

  cargarProductos() {
    this.cargando = true;
    this.productoService.getProductos().subscribe({
      next: (data) => {
        this.productos = data;
        this.cargando = false;
      },
      error: (err) => {
        this.error = 'No se pudieron cargar los productos.';
        this.cargando = false;
      }
    });
  }
}

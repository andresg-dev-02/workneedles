import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductoService, FiltroProductoDto } from '../../../Services/Producto/producto.service';
import { ProductoModel } from '../../../Models/Producto/producto.model';

@Component({
  selector: 'app-producto',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './producto.component.html',
})
export class ProductoComponent implements OnInit {
  productos: ProductoModel[] = [];
  cargando = false;
  error = '';

  filtro: FiltroProductoDto = {};
  readonly generos = ['Masculino', 'Femenino', 'Unisex'];

  constructor(private productoService: ProductoService) {}

  ngOnInit() {
    this.cargarProductos();
  }

  cargarProductos() {
    this.cargando = true;
    this.error = '';
    this.productoService.getProductos().subscribe({
      next: data => { this.productos = data; this.cargando = false; },
      error: () => { this.error = 'No se pudieron cargar los productos.'; this.cargando = false; }
    });
  }

  aplicarFiltro() {
    this.cargando = true;
    this.error = '';
    this.productoService.buscarProductos(this.filtro).subscribe({
      next: data => { this.productos = data; this.cargando = false; },
      error: () => { this.error = 'Error al filtrar productos.'; this.cargando = false; }
    });
  }

  limpiarFiltro() {
    this.filtro = {};
    this.cargarProductos();
  }
}
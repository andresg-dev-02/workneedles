import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductoService } from '../../Services/Producto/producto.service';
import { ProductoModel } from '../../Models/Producto/producto.model';
import { InsumosProductoDto } from '../../Services/GestorProductos/gestor-p.service';

@Component({
  selector: 'app-insumo-producto',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './insumo-producto.component.html',
})
export class InsumoProductoComponent implements OnInit {
  productos: ProductoModel[] = [];
  productoSeleccionado: ProductoModel | null = null;
  insumos: InsumosProductoDto[] = [];

  cargandoProductos = false;
  cargandoInsumos = false;
  errorProductos: string | null = null;
  errorInsumos: string | null = null;

  constructor(private productoService: ProductoService) {}

  ngOnInit(): void {
    this.cargarProductos();
  }

  cargarProductos(): void {
    this.cargandoProductos = true;
    this.errorProductos = null;

    this.productoService.getProductos().subscribe({
      next: (data: ProductoModel[]) => {
        this.productos = data;
        this.cargandoProductos = false;
      },
      error: () => {
        this.errorProductos = 'No se pudieron cargar los productos.';
        this.cargandoProductos = false;
      },
    });
  }

  seleccionarProducto(producto: ProductoModel): void {
    if (this.productoSeleccionado?.id === producto.id) {
      this.productoSeleccionado = null;
      this.insumos = [];
      return;
    }

    this.productoSeleccionado = producto;
    this.insumos = [];
    this.cargandoInsumos = true;
    this.errorInsumos = null;

    this.productoService.getInsumos(producto.id).subscribe({
      next: (data: InsumosProductoDto[]) => {
        this.insumos = data;
        this.cargandoInsumos = false;
      },
      error: () => {
        this.errorInsumos = 'No se pudieron cargar los insumos.';
        this.cargandoInsumos = false;
      },
    });
  }

  estaSeleccionado(producto: ProductoModel): boolean {
    return this.productoSeleccionado?.id === producto.id;
  }
}
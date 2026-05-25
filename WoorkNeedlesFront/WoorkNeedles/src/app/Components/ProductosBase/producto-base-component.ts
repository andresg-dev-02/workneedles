import { Component, OnInit } from '@angular/core';
import {ProductoService} from '../../Services/Producto/producto.service';
import {ProductoModel} from '../../Models/Producto/producto.model';
import {CommonModule} from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-producto-base-component',
  imports: [CommonModule, FormsModule],
  templateUrl: './producto-base-component.html',
  styleUrl: './producto-base-component.css',
})
export class ProductoBaseComponent implements OnInit {
  productos: ProductoModel[] = [];
  errorProductos: string | null = null;

  constructor(private productoService: ProductoService) {}

  ngOnInit(): void {
    this.cargarProductos();
  }

  private cargarProductos(): void {
    this.productoService.getProductos().subscribe({
      next: (productos: ProductoModel[]) => {
        this.productos = productos;
        console.log('Productos cargados:', this.productos);
      },
      error: (error: any) => {
        console.error('Error al cargar productos:', error);
        this.errorProductos = 'Error al cargar productos';
      }
    });
  }
}

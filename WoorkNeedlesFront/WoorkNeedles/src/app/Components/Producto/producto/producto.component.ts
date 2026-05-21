import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductoService } from '../../../Services/Producto/producto.service';
import { ProductoModel } from '../../../Models/Producto/producto.model';

@Component({
  selector: 'app-producto',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './producto.component.html',
})

export class ProductoComponent implements OnInit {
  productos: ProductoModel[] = [];
  cargando = false;
  error = '';

  constructor(private productoService: ProductoService, private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    this.cargarProductos();
  }

  cargarProductos() {
  this.cargando = true;
  this.productoService.getProductos().subscribe({
    next: (data) => {
      console.log('Datos recibidos:', data); 
      this.productos = data;
      this.cargando = false;
      console.log(this.cargando); 
      this.cdr.detectChanges(); 
    },
    error: (err) => {
      console.log('Error:', err);
      this.error = 'No se pudieron cargar los productos.';
      this.cargando = false;
      this.cdr.detectChanges(); 
    }
  });
}
}

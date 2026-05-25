import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductoService, CreateProductoDto } from '../../Services/Producto/producto.service';
import { ProductoModel } from '../../Models/Producto/producto.model';

@Component({
  selector: 'app-producto-base-component',
  imports: [CommonModule, FormsModule],
  templateUrl: './producto-base-component.html',
  styleUrl: './producto-base-component.css',
})
export class ProductoBaseComponent implements OnInit {
  productos: ProductoModel[] = [];
  errorProductos: string | null = null;

  // Modales
  modalCrear = false;
  modalEditar = false;
  modalEliminar = false;

  // Loading por acción
  loadingCrear = false;
  loadingEditar = false;
  loadingEliminar = false;

  // Errores por acción
  errorCrear: string | null = null;
  errorEditar: string | null = null;

  // Producto en foco para editar/eliminar
  productoEditar: ProductoModel | null = null;
  productoEliminar: ProductoModel | null = null;

  // Form crear
  nuevoProducto: CreateProductoDto = this.formVacio();

  constructor(private productoService: ProductoService) {}

  ngOnInit(): void {
    this.cargarProductos();
  }

  private formVacio(): CreateProductoDto {
    return {
      nombre: '',
      descripcion: '',
      material: '',
      preciobase: 0,
      urlimagen: '',
      idCategoria: 0,
      genero: '',
    };
  }

  cargarProductos(): void {
    this.productoService.getProductos().subscribe({
      next: (productos) => (this.productos = productos),
      error: () => (this.errorProductos = 'Error al cargar productos'),
    });
  }

  // ── Crear ──────────────────────────────────────────────────
  abrirCrear(): void {
    this.nuevoProducto = this.formVacio();
    this.errorCrear = null;
    this.modalCrear = true;
  }

  crearProducto(): void {
    if (!this.nuevoProducto.nombre || !this.nuevoProducto.descripcion) return;
    this.loadingCrear = true;
    this.errorCrear = null;

    this.productoService.createProducto(this.nuevoProducto).subscribe({
      next: () => {
        this.modalCrear = false;
        this.loadingCrear = false;
        this.cargarProductos();
      },
      error: () => {
        this.errorCrear = 'Error al crear el producto.';
        this.loadingCrear = false;
      },
    });
  }

  // ── Editar ─────────────────────────────────────────────────
  abrirEditar(producto: ProductoModel): void {
    this.productoEditar = { ...producto };
    this.errorEditar = null;
    this.modalEditar = true;
  }

  guardarEdicion(): void {
    if (!this.productoEditar) return;
    this.loadingEditar = true;
    this.errorEditar = null;

    const dto: CreateProductoDto = {
      nombre: this.productoEditar.nombre,
      descripcion: this.productoEditar.descripcion,
      material: this.productoEditar.material,
      preciobase: this.productoEditar.preciobase,
      urlimagen: this.productoEditar.urlimagen,
      idCategoria: this.productoEditar.idCategoria,
      genero: this.productoEditar.genero,
    };

    this.productoService.updateProducto(this.productoEditar.id, dto).subscribe({
      next: () => {
        this.modalEditar = false;
        this.loadingEditar = false;
        this.cargarProductos();
      },
      error: () => {
        this.errorEditar = 'Error al actualizar el producto.';
        this.loadingEditar = false;
      },
    });
  }

  // ── Eliminar ───────────────────────────────────────────────
  abrirEliminar(producto: ProductoModel): void {
    this.productoEliminar = producto;
    this.modalEliminar = true;
  }

  confirmarEliminar(): void {
    if (!this.productoEliminar) return;
    this.loadingEliminar = true;

    this.productoService.deleteProducto(this.productoEliminar.id).subscribe({
      next: () => {
        this.modalEliminar = false;
        this.loadingEliminar = false;
        this.cargarProductos();
      },
      error: () => {
        this.loadingEliminar = false;
      },
    });
  }
}
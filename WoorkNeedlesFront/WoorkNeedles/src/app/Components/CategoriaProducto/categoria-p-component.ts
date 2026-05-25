import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  CategoriaProductoService,
  CategoriaProductoDto,
  CreateCategoriaProductoDto,
} from '../../Services/CategoriasProducto/categoria-producto.service';

@Component({
  selector: 'app-categoria-p-component',
  imports: [CommonModule, FormsModule],
  templateUrl: './categoria-p-component.html',
  styleUrl: './categoria-p-component.css',
})
export class CategoriaPComponent implements OnInit {

  // ── Estado general ──────────────────────────────────────────────────
  categorias: CategoriaProductoDto[] = [];
  error = '';

  // ── Modal Crear ─────────────────────────────────────────────────────
  modalCrear = false;
  nuevaCategoria: CreateCategoriaProductoDto = { nombre: '', descripcion: '' };
  errorCrear = '';
  loadingCrear = false;

  // ── Modal Editar ─────────────────────────────────────────────────────
  modalEditar = false;
  categoriaEditar: CategoriaProductoDto | null = null;
  loadingEditar = false;

  // ── Modal Eliminar ───────────────────────────────────────────────────
  modalEliminar = false;
  categoriaEliminar: CategoriaProductoDto | null = null;
  loadingEliminar = false;

  constructor(private categoriaService: CategoriaProductoService) {}

  // ── Ciclo de vida ────────────────────────────────────────────────────
  ngOnInit(): void {
    this.cargarCategorias();
  }

  // ── Listar ───────────────────────────────────────────────────────────
    cargarCategorias(): void {
    this.error = '';

    this.categoriaService.getCategoriasProducto().subscribe({
      next: (data) => {
        this.categorias = data;

        console.log(this.categorias);
      },

      error: () => (
        this.error = 'Error al cargar las categorías.'
      ),
    });
  }
  // ── Crear ────────────────────────────────────────────────────────────
  crearCategoria(): void {
    if (!this.nuevaCategoria.nombre.trim()) {
      this.errorCrear = 'El nombre es obligatorio.';
      return;
    }
    this.loadingCrear = true;
    this.errorCrear = '';

    this.categoriaService.createCategoriaProducto(this.nuevaCategoria).subscribe({
      next: () => {
        this.loadingCrear = false;
        this.modalCrear = false;
        this.nuevaCategoria = { nombre: '', descripcion: '' };
        this.cargarCategorias();
      },
      error: () => {
        this.loadingCrear = false;
        this.errorCrear = 'Error al crear la categoría.';
      },
    });
  }

  // ── Editar ───────────────────────────────────────────────────────────
  abrirEditar(categoria: CategoriaProductoDto): void {
    this.categoriaEditar = { ...categoria };
    this.modalEditar = true;
  }

  guardarEdicion(): void {
    if (!this.categoriaEditar) return;
    this.loadingEditar = true;

    this.categoriaService
      .updateCategoriaProducto(this.categoriaEditar.id, this.categoriaEditar)
      .subscribe({
        next: () => {
          this.loadingEditar = false;
          this.modalEditar = false;
          this.categoriaEditar = null;
          this.cargarCategorias();
        },
        error: (err) => {
          this.loadingEditar = false;
          console.log('Status:', err.status);
        console.log('Error completo:', err.error);
        },
      });
  }

  // ── Eliminar ─────────────────────────────────────────────────────────
  abrirEliminar(categoria: CategoriaProductoDto): void {
    this.categoriaEliminar = categoria;
    this.modalEliminar = true;
  }

  confirmarEliminar(): void {
    if (!this.categoriaEliminar) return;
    this.loadingEliminar = true;

    this.categoriaService.deleteCategoriaProducto(this.categoriaEliminar.id).subscribe({
      next: () => {
        this.loadingEliminar = false;
        this.modalEliminar = false;
        this.categoriaEliminar = null;
        this.cargarCategorias();
      },
      error: () => {
        this.loadingEliminar = false;
      },
    });
  }
}
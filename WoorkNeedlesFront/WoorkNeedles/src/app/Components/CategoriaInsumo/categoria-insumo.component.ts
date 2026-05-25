import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  CategoriaInsumoService,
  CategoriaInsumoDto,
  CreateCategoriaInsumoDto,
  UpdateCategoriaInsumoDto,
} from '../../Services/CategoriaInsumo/categoria-insumo.service';

@Component({
  selector: 'app-categoria-insumo',
  imports: [CommonModule, FormsModule],
  templateUrl: './categoria-insumo.component.html',

})
export class CategoriaInsumoComponent implements OnInit {

  // ── Estado general ──────────────────────────────────────────────────
  categorias: CategoriaInsumoDto[] = [];
  error = '';

  // ── Modal Crear ──────────────────────────────────────────────────────
  modalCrear = false;
  nuevaCategoria: CreateCategoriaInsumoDto = { nombre: '', descripcion: '' };
  errorCrear = '';
  loadingCrear = false;

  // ── Modal Editar ──────────────────────────────────────────────────────
  modalEditar = false;
  categoriaEditar: CategoriaInsumoDto | null = null;
  loadingEditar = false;
  errorEditar = '';

  // ── Modal Eliminar ────────────────────────────────────────────────────
  modalEliminar = false;
  categoriaEliminar: CategoriaInsumoDto | null = null;
  loadingEliminar = false;

  constructor(private categoriaInsumoService: CategoriaInsumoService) {}

  // ── Ciclo de vida ────────────────────────────────────────────────────
  ngOnInit(): void {
    this.cargarCategorias();
  }

  // ── Listar ───────────────────────────────────────────────────────────
  cargarCategorias(): void {
    this.error = '';
    this.categoriaInsumoService.getCategoriasInsumo().subscribe({
      next: (data) => (this.categorias = data),
      error: () => (this.error = 'Error al cargar las categorías de insumo.'),
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

    this.categoriaInsumoService.createCategoriaInsumo(this.nuevaCategoria).subscribe({
      next: () => {
        this.loadingCrear = false;
        this.modalCrear = false;
        this.nuevaCategoria = { nombre: '', descripcion: '' };
        this.cargarCategorias();
      },
      error: (err) => {
        this.loadingCrear = false;
        this.errorCrear = err.error ?? 'Error al crear la categoría.';
      },
    });
  }

  // ── Editar ───────────────────────────────────────────────────────────
  abrirEditar(categoria: CategoriaInsumoDto): void {
    this.categoriaEditar = { ...categoria };
    this.errorEditar = '';
    this.modalEditar = true;
  }

  guardarEdicion(): void {
    if (!this.categoriaEditar) return;
    this.loadingEditar = true;
    this.errorEditar = '';

    const dto: UpdateCategoriaInsumoDto = {
      nombre: this.categoriaEditar.nombre,
      descripcion: this.categoriaEditar.descripcion,
    };

    this.categoriaInsumoService
      .updateCategoriaInsumo(this.categoriaEditar.id, dto)
      .subscribe({
        next: () => {
          this.loadingEditar = false;
          this.modalEditar = false;
          this.categoriaEditar = null;
          this.cargarCategorias();
        },
        error: (err) => {
          this.loadingEditar = false;
          this.errorEditar = err.error ?? 'Error al actualizar la categoría.';
        },
      });
  }

  // ── Eliminar ──────────────────────────────────────────────────────────
  abrirEliminar(categoria: CategoriaInsumoDto): void {
    this.categoriaEliminar = categoria;
    this.modalEliminar = true;
  }

  confirmarEliminar(): void {
    if (!this.categoriaEliminar) return;
    this.loadingEliminar = true;

    this.categoriaInsumoService.deleteCategoriaInsumo(this.categoriaEliminar.id).subscribe({
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
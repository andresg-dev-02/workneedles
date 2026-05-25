import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InsumosService, InsumoDto, CreateInsumoDto } from '../../Services/Insumo/insumos.service';
import { CategoriaInsumoService, CategoriaInsumoDto } from '../../Services/CategoriaInsumo/categoria-insumo.service';

export interface UpdateInsumoDto {
  idcategoria: number;
  nombre: string;
  descripcion: string;
  unidadmedida: string;
  stockactual: number;
  stockalerta: number;
  precio: number;
  proveedor?: string | null;
}

@Component({
  selector: 'app-insumo-component',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './insumo-component.html',
})
export class InsumosComponent implements OnInit {

  insumos: InsumoDto[] = [];
  categorias: CategoriaInsumoDto[] = [];
  error = '';

  // Filtro
  filtroActivo: boolean | null = null;
  filtroAlerta = false;

  // Modal crear
  modalCrear = false;
  loadingCrear = false;
  errorCrear = '';
  nuevoInsumo: CreateInsumoDto = {
    idcategoria: 0, nombre: '', descripcion: '',
    unidadmedida: '', stockactual: 0, stockalerta: 0,
    precio: 0, proveedor: null
  };

  // Modal editar
  modalEditar = false;
  loadingEditar = false;
  insumoEditar: UpdateInsumoDto | null = null;
  insumoEditarId = 0;

  // Modal eliminar
  modalEliminar = false;
  loadingEliminar = false;
  insumoEliminar: InsumoDto | null = null;

  readonly unidades = ['metros', 'kg', 'unidades', 'litros', 'rollos'];

  constructor(
    private insumosService: InsumosService,
    private categoriaInsumoService: CategoriaInsumoService
  ) {}

  ngOnInit() {
    this.getInsumos();
    this.categoriaInsumoService.getCategoriasInsumo().subscribe({
      next: c => this.categorias = c,
      error: () => {}
    });
  }

  getInsumos() {
    this.insumosService.getInsumos().subscribe({
      next: r => this.insumos = r,
      error: () => this.error = 'No se pudieron cargar los insumos.'
    });
  }

  get insumosFiltrados(): InsumoDto[] {
    return this.insumos.filter(i => {
      if (this.filtroActivo !== null && i.activo !== this.filtroActivo) return false;
      if (this.filtroAlerta && i.stockactual > i.stockalerta) return false;
      return true;
    });
  }

  get totalAlerta(): number {
    return this.insumos.filter(i => i.stockactual <= i.stockalerta).length;
  }

  // ── Crear ──
  crearInsumo() {
    if (!this.nuevoInsumo.nombre || !this.nuevoInsumo.idcategoria) return;
    this.loadingCrear = true;
    this.errorCrear = '';
    this.insumosService.createInsumo(this.nuevoInsumo).subscribe({
      next: () => {
        this.modalCrear = false;
        this.nuevoInsumo = {
          idcategoria: 0, nombre: '', descripcion: '',
          unidadmedida: '', stockactual: 0, stockalerta: 0,
          precio: 0, proveedor: null
        };
        this.loadingCrear = false;
        this.getInsumos();
      },
      error: () => {
        this.errorCrear = 'No se pudo crear el insumo.';
        this.loadingCrear = false;
      }
    });
  }

  // ── Editar ──
  abrirEditar(insumo: InsumoDto) {
    // Buscar el id de la categoría por nombre
    const cat = this.categorias.find(c => c.nombre === insumo.categoria);
    this.insumoEditarId = insumo.id;
    this.insumoEditar = {
      idcategoria: cat?.id ?? 0,
      nombre: insumo.nombre,
      descripcion: insumo.descripcion,
      unidadmedida: insumo.unidadmedida,
      stockactual: insumo.stockactual,
      stockalerta: insumo.stockalerta,
      precio: insumo.precio,
      proveedor: insumo.proveedor,
    };
    this.modalEditar = true;
  }

  guardarEdicion() {
    if (!this.insumoEditar || !this.insumoEditarId) return;
    this.loadingEditar = true;

    const dto: UpdateInsumoDto = {
      ...this.insumoEditar,
      idcategoria: Number(this.insumoEditar.idcategoria), // evita FK error por string
    };

    this.insumosService.updateInsumo(this.insumoEditarId, dto).subscribe({
      next: () => {
        this.modalEditar = false;
        this.insumoEditar = null;
        this.insumoEditarId = 0;
        this.loadingEditar = false;
        this.getInsumos();
      },
      error: () => { this.loadingEditar = false; }
    });
  }

  // ── Eliminar ──
  abrirEliminar(insumo: InsumoDto) {
    this.insumoEliminar = insumo;
    this.modalEliminar = true;
  }

  confirmarEliminar() {
    if (!this.insumoEliminar) return;
    this.loadingEliminar = true;
    this.insumosService.deleteInsumo(this.insumoEliminar.id).subscribe({
      next: () => {
        this.insumos = this.insumos.filter(i => i.id !== this.insumoEliminar!.id);
        this.modalEliminar = false;
        this.loadingEliminar = false;
      },
      error: () => { this.loadingEliminar = false; }
    });
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('es-CO', {
      style: 'currency', currency: 'COP', maximumFractionDigits: 0
    }).format(value);
  }
}
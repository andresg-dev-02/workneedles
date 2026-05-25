import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { forkJoin, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { CategoriaProductoService, CategoriaProductoDto } from '../../Services/CategoriasProducto/categoria-producto.service';
import { ProductoService, InsumosProductoDto } from '../../Services/Producto/producto.service';
import { ProductoModel } from '../../Models/Producto/producto.model';
import { InventarioService, InventarioDto } from '../../Services/Inventario/inventarios.service';
import { ColoresService, ColorDto } from '../../Services/Color/colores.service';
import { TallasService, TallaDto } from '../../Services/Talla/tallas.service';
import { InsumosService, InsumoDto } from '../../Services/Insumo/insumos.service';

@Component({
  selector: 'app-inventario',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './inventario-component.html',
})
export class InsumoProductoComponent implements OnInit {
  // Categorías
  categorias: CategoriaProductoDto[] = [];
  categoriaSeleccionada: CategoriaProductoDto | null = null;

  // Productos
  productos: ProductoModel[] = [];
  cargandoProductos = false;
  errorProductos: string | null = null;

  // Producto seleccionado
  productoSeleccionado: ProductoModel | null = null;
  inventario: InventarioDto[] = [];
  insumos: InsumosProductoDto[] = [];
  cargandoDetalle = false;
  errorDetalle: string | null = null;

  // Catálogos
  colores: ColorDto[] = [];
  tallas: TallaDto[] = [];
  insumosDisponibles: InsumoDto[] = [];

  // Modal inventario
  modalInventario = false;
  nuevoInventario = { idcolor: 0, idtalla: 0, stock: 0 };
  loadingInventario = false;
  errorInventario: string | null = null;

  // Modal insumo — agregar
  modalInsumo = false;
  nuevoInsumo = { idinsumo: 0, cantidad: 0 };
  loadingInsumo = false;
  errorInsumo: string | null = null;

  // Modal insumo — editar
  modalEditarInsumo = false;
  insumoEditando: InsumosProductoDto | null = null;
  cantidadEditar = 0;
  loadingEditarInsumo = false;

  // Modal inventario — editar stock
  modalEditarStock = false;
  inventarioEditando: InventarioDto | null = null;
  stockEditar = 0;
  loadingEditarStock = false;


  // Modal nuevo producto
  modalNuevoProducto = false;
  productosBase: ProductoModel[] = [];
  productoBaseSeleccionado: ProductoModel | null = null;
  cargandoProductosBase = false;

  // Variantes a agregar
  variantesNuevas: { idcolor: number; idtalla: number; stock: number }[] = [];
  varianteTemp = { idcolor: 0, idtalla: 0, stock: 0 };

  // Insumos a agregar
  insumosNuevos: { idinsumo: number; cantidad: number }[] = [];
  insumoTemp = { idinsumo: 0, cantidad: 0 };

  loadingNuevoProducto = false;
  errorNuevoProducto: string | null = null;
  pasoNuevoProducto: 'seleccionar' | 'configurar' = 'seleccionar';

  errorCategorias: string | null = null;

  constructor(
    private categoriaService: CategoriaProductoService,
    private productoService: ProductoService,
    private inventarioService: InventarioService,
    private coloresService: ColoresService,
    private tallasService: TallasService,
    private insumosService: InsumosService,
  ) {}

  ngOnInit(): void {
    this.cargarCategorias();
    this.cargarCatalogos();
  }

  cargarCatalogos(): void {
    forkJoin({
      colores: this.coloresService.getColores(),
      tallas: this.tallasService.getTallas(),
      insumos: this.insumosService.getInsumos(),
    }).subscribe({
      next: ({ colores, tallas, insumos }) => {
        this.colores = colores;
        this.tallas = tallas;
        this.insumosDisponibles = insumos;
      },
    });
  }

  cargarCategorias(): void {
    this.categoriaService.getCategoriasProducto().subscribe({
      next: (data: CategoriaProductoDto[]) => (this.categorias = data),
      error: () => (this.errorCategorias = 'Error al cargar categorías'),
    });
  }

  seleccionarCategoria(categoria: CategoriaProductoDto): void {
    if (this.categoriaSeleccionada?.id === categoria.id) return;
    this.categoriaSeleccionada = categoria;
    this.productoSeleccionado = null;
    this.inventario = [];
    this.insumos = [];
    this.productos = [];
    this.errorProductos = null;
    this.cargandoProductos = true;

    this.productoService.getProductos().subscribe({
      next: (todos: ProductoModel[]) => {
        const porCategoria = todos.filter(p => p.categoria === categoria.nombre && p.activo);
        if (porCategoria.length === 0) { this.cargandoProductos = false; return; }

        const checks$ = porCategoria.map(p =>
          forkJoin({
            inventario: this.inventarioService.getInventarioPorProducto(p.id).pipe(catchError(() => of([]))),
            insumos: this.productoService.getInsumos(p.id).pipe(catchError(() => of([]))),
          }).pipe(map(({ inventario, insumos }) => ({
            producto: p,
            tieneInventario: (inventario as any[]).length > 0,
            tieneInsumos: (insumos as any[]).length > 0,
          })))
        );

        forkJoin(checks$).subscribe({
          next: (res) => {
            this.productos = res.filter(r => r.tieneInventario && r.tieneInsumos).map(r => r.producto);
            this.cargandoProductos = false;
          },
          error: () => { this.errorProductos = 'Error al verificar productos.'; this.cargandoProductos = false; },
        });
      },
      error: () => { this.errorProductos = 'Error al cargar productos.'; this.cargandoProductos = false; },
    });
  }

  seleccionarProducto(producto: ProductoModel): void {
    if (this.productoSeleccionado?.id === producto.id) {
      this.productoSeleccionado = null;
      this.inventario = [];
      this.insumos = [];
      return;
    }
    this.productoSeleccionado = producto;
    this.recargarDetalle();
  }

  recargarDetalle(): void {
    if (!this.productoSeleccionado) return;
    this.cargandoDetalle = true;
    this.errorDetalle = null;

    forkJoin({
      inventario: this.inventarioService.getInventarioPorProducto(this.productoSeleccionado.id),
      insumos: this.productoService.getInsumos(this.productoSeleccionado.id),
    }).subscribe({
      next: ({ inventario, insumos }) => {
        this.inventario = inventario;
        this.insumos = insumos;
        this.cargandoDetalle = false;
      },
      error: () => { this.errorDetalle = 'Error al cargar detalle.'; this.cargandoDetalle = false; },
    });
  }

  estaSeleccionado(producto: ProductoModel): boolean {
    return this.productoSeleccionado?.id === producto.id;
  }

  // ── Inventario ─────────────────────────────────────────────

  abrirModalInventario(): void {
    this.nuevoInventario = { idcolor: 0, idtalla: 0, stock: 0 };
    this.errorInventario = null;
    this.modalInventario = true;
  }

  agregarInventario(): void {
    if (!this.productoSeleccionado) return;
    if (!this.nuevoInventario.idcolor || !this.nuevoInventario.idtalla || this.nuevoInventario.stock <= 0) {
      this.errorInventario = 'Completa todos los campos correctamente.';
      return;
    }
    this.loadingInventario = true;
    this.inventarioService.createInventario(this.productoSeleccionado.id, {
      idproducto: this.productoSeleccionado.id,
      idcolor: this.nuevoInventario.idcolor,
      idtalla: this.nuevoInventario.idtalla,
      stock: this.nuevoInventario.stock,
    }).subscribe({
      next: () => { this.modalInventario = false; this.loadingInventario = false; this.recargarDetalle(); },
      error: () => { this.errorInventario = 'Error al agregar inventario.'; this.loadingInventario = false; },
    });
  }

  abrirEditarStock(item: InventarioDto): void {
    this.inventarioEditando = item;
    this.stockEditar = item.stock;
    this.modalEditarStock = true;
  }

  guardarStock(): void {
    if (!this.inventarioEditando || !this.productoSeleccionado) return;
    this.loadingEditarStock = true;
    this.inventarioService.updateInventario(
      this.productoSeleccionado.id,
      this.inventarioEditando.id,
      { stock: this.stockEditar }
    ).subscribe({
      next: () => { this.modalEditarStock = false; this.loadingEditarStock = false; this.recargarDetalle(); },
      error: () => { this.loadingEditarStock = false; },
    });
  }

  eliminarInventario(item: InventarioDto): void {
    if (!this.productoSeleccionado) return;
    this.inventarioService.deleteInventario(this.productoSeleccionado.id, item.id).subscribe({
      next: () => this.recargarDetalle(),
    });
  }

  // ── Insumos ────────────────────────────────────────────────

  abrirModalInsumo(): void {
    this.nuevoInsumo = { idinsumo: 0, cantidad: 0 };
    this.errorInsumo = null;
    this.modalInsumo = true;
  }

  agregarInsumo(): void {
    if (!this.productoSeleccionado) return;
    if (!this.nuevoInsumo.idinsumo || this.nuevoInsumo.cantidad <= 0) {
      this.errorInsumo = 'Selecciona un insumo e ingresa una cantidad válida.';
      return;
    }
    this.loadingInsumo = true;
    this.productoService.createInsumoProducto(this.productoSeleccionado.id, this.nuevoInsumo).subscribe({
      next: () => { this.modalInsumo = false; this.loadingInsumo = false; this.recargarDetalle(); },
      error: () => { this.errorInsumo = 'Error al agregar insumo.'; this.loadingInsumo = false; },
    });
  }

  abrirEditarInsumo(insumo: InsumosProductoDto): void {
    this.insumoEditando = insumo;
    this.cantidadEditar = insumo.cantidad;
    this.modalEditarInsumo = true;
  }

  guardarInsumo(): void {
    if (!this.insumoEditando || !this.productoSeleccionado) return;
    this.loadingEditarInsumo = true;
    this.productoService.updateInsumoProducto(
      this.productoSeleccionado.id,
      this.insumoEditando.id,
      this.cantidadEditar
    ).subscribe({
      next: () => { this.modalEditarInsumo = false; this.loadingEditarInsumo = false; this.recargarDetalle(); },
      error: () => { this.loadingEditarInsumo = false; },
    });
  }

  eliminarInsumo(insumo: InsumosProductoDto): void {
    if (!this.productoSeleccionado) return;
    this.productoService.deleteInsumoProducto(this.productoSeleccionado.id, insumo.id).subscribe({
      next: () => this.recargarDetalle(),
    });
  }

  get insumosFiltrados(): InsumoDto[] {
    const usados = new Set(this.insumos.map(i => i.nombreInsumo));
    return this.insumosDisponibles.filter(i => !usados.has(i.nombre) && i.activo);
  }

  abrirNuevoProducto() {
  this.modalNuevoProducto = true;
  this.pasoNuevoProducto = 'seleccionar';
  this.productoBaseSeleccionado = null;
  this.variantesNuevas = [];
  this.insumosNuevos = [];
  this.errorNuevoProducto = null;
  this.cargandoProductosBase = true;

  // Traer todos los productos y filtrar los que NO tienen inventario
  this.productoService.getProductos().subscribe({
    next: productos => {
      const activos = productos.filter(p => p.activo);
      const checks$ = activos.map(p =>
        this.inventarioService.getInventarioPorProducto(p.id).pipe(
          catchError(() => of([])),
          map(inv => ({ producto: p, tieneInventario: (inv as any[]).length > 0 }))
        )
      );
      forkJoin(checks$).subscribe({
        next: res => {
          this.productosBase = res.filter(r => !r.tieneInventario).map(r => r.producto);
          this.cargandoProductosBase = false;
        },
        error: () => { this.cargandoProductosBase = false; }
      });
    },
    error: () => { this.cargandoProductosBase = false; }
  });
}

seleccionarProductoBase(producto: ProductoModel) {
  this.productoBaseSeleccionado = producto;
  this.pasoNuevoProducto = 'configurar';
}

agregarVarianteTemp() {
  if (!this.varianteTemp.idcolor || !this.varianteTemp.idtalla || this.varianteTemp.stock <= 0) return;
  this.variantesNuevas.push({ ...this.varianteTemp });
  this.varianteTemp = { idcolor: 0, idtalla: 0, stock: 0 };
}

quitarVariante(i: number) { this.variantesNuevas.splice(i, 1); }

agregarInsumoTemp() {
  if (!this.insumoTemp.idinsumo || this.insumoTemp.cantidad <= 0) return;
  this.insumosNuevos.push({ ...this.insumoTemp });
  this.insumoTemp = { idinsumo: 0, cantidad: 0 };
}

quitarInsumoNuevo(i: number) { this.insumosNuevos.splice(i, 1); }

nombreColor(id: number): string {
  return this.colores.find(c => c.id === id)?.nombre ?? '';
}

nombreTalla(id: number): string {
  return this.tallas.find(t => t.id === id)?.nombre ?? '';
}

nombreInsumo(id: number): string {
  return this.insumosDisponibles.find(i => i.id === id)?.nombre ?? '';
}

async guardarNuevoProducto() {
  if (!this.productoBaseSeleccionado || this.variantesNuevas.length === 0) {
    this.errorNuevoProducto = 'Debes agregar al menos una variante.';
    return;
  }
  this.loadingNuevoProducto = true;
  this.errorNuevoProducto = null;

  try {
    // Guardar variantes
    for (const v of this.variantesNuevas) {
      await this.inventarioService.createInventario(this.productoBaseSeleccionado.id, {
        idproducto: this.productoBaseSeleccionado.id,
        idcolor: v.idcolor,
        idtalla: v.idtalla,
        stock: v.stock
      }).toPromise();
    }
    // Guardar insumos
    for (const ins of this.insumosNuevos) {
      await this.productoService.createInsumoProducto(
        this.productoBaseSeleccionado.id, ins
      ).toPromise();
    }
    this.modalNuevoProducto = false;
    this.loadingNuevoProducto = false;
    // Recargar la categoría actual
    if (this.categoriaSeleccionada) this.seleccionarCategoria(this.categoriaSeleccionada);
  } catch {
    this.errorNuevoProducto = 'Error al guardar. Intenta de nuevo.';
    this.loadingNuevoProducto = false;
  }
}
}
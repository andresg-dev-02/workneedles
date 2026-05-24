import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PedidoService } from '../../../Services/Pedido/pedido.service';
import { ProductoService, InventarioDto } from '../../../Services/Producto/producto.service';
import { ProductoModel } from '../../../Models/Producto/producto.model';
import { PedidoModel } from '../../../Models/Pedido/pedido.model';

interface DetallePedidoDto {
  id: number;
  nombreProducto: string;
  talla: string;
  color: string;
  cantidad: number;
  preciounitario: number;
  subtotal: number;
}

interface UpdateDetallePedidoDto {
  idinventario: number | null;
  cantidad: number;
  preciounitario: number;
}

@Component({
  selector: 'app-pedido',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pedido.component.html',
})
export class PedidoComponent implements OnInit {

  pedidos: PedidoModel[] = [];
  loading = false;
  error = '';

  // Modal detalles
  modalDetalles = false;
  pedidoSeleccionado: PedidoModel | null = null;
  detalles: DetallePedidoDto[] = [];
  loadingDetalles = false;

  // Agregar producto en modal detalles
  mostrarFormAgregar = false;
  productos: ProductoModel[] = [];
  idProductoNuevo: number | null = null;
  inventarioNuevo: InventarioDto[] = [];
  inventarioSeleccionadoNuevo: InventarioDto | null = null;
  cantidadNueva = 1;
  precioNuevo = 0;
  loadingInventarioNuevo = false;
  loadingAgregarDetalle = false;
  errorAgregarDetalle = '';
  successAgregarDetalle = '';

  // Modal editar pedido
  modalEditar = false;
  pedidoEditar: PedidoModel | null = null;
  loadingEditar = false;

  // Modal editar detalle
  modalEditarDetalle = false;
  detalleEditar: DetallePedidoDto | null = null;
  editarCantidad = 1;
  editarPrecio = 0;
  loadingEditarDetalle = false;

  // Modal eliminar pedido
  modalEliminar = false;
  pedidoEliminar: PedidoModel | null = null;
  loadingEliminar = false;
  errorEliminar = '';

  pedidoEditarIdCliente = 0;
  pedidoEditarIdUsuario = 0;

  readonly estadoOpciones = ['pendiente', 'en preparacion', 'enviado', 'entregado', 'cancelado'];

  readonly estadoClases: Record<string, string> = {
    'pendiente':      'bg-yellow-100 text-yellow-700',
    'en preparacion': 'bg-blue-100 text-blue-700',
    'enviado':        'bg-purple-100 text-purple-700',
    'entregado':      'bg-green-100 text-green-700',
    'cancelado':      'bg-red-100 text-red-700',
  };

  constructor(
    private pedidoService: PedidoService,
    private productoService: ProductoService,
    private router: Router
  ) {}

  ngOnInit() {
    this.cargarPedidos();
    this.productoService.getProductos().subscribe({
      next: p => this.productos = p,
      error: () => {}
    });
  }

  cargarPedidos() {
    this.loading = true;
    this.error = '';
    this.pedidoService.getPedidos().subscribe({
      next: p => { this.pedidos = p; this.loading = false; },
      error: () => { this.error = 'No se pudieron cargar los pedidos.'; this.loading = false; }
    });
  }

  nuevoPedido() { this.router.navigate(['/admin/pedidos/nuevo']); }

  estadoClase(estado: string): string {
    return this.estadoClases[estado.toLowerCase()] ?? 'bg-gray-100 text-gray-600';
  }

  // ── Ver detalles ──
  verDetalles(pedido: PedidoModel) {
    this.pedidoSeleccionado = pedido;
    this.detalles = [];
    this.modalDetalles = true;
    this.loadingDetalles = true;
    this.mostrarFormAgregar = false;
    this.resetFormAgregar();

    this.pedidoService.getDetalles(pedido.id).subscribe({
      next: d => { this.detalles = d; this.loadingDetalles = false; },
      error: () => { this.loadingDetalles = false; }
    });
  }

  // ── Agregar producto en modal detalles ──
  toggleFormAgregar() {
    this.mostrarFormAgregar = !this.mostrarFormAgregar;
    if (!this.mostrarFormAgregar) this.resetFormAgregar();
  }

  onProductoNuevoChange() {
    this.inventarioNuevo = [];
    this.inventarioSeleccionadoNuevo = null;
    this.precioNuevo = 0;
    if (!this.idProductoNuevo) return;

    this.loadingInventarioNuevo = true;
    this.productoService.getInventario(Number(this.idProductoNuevo)).subscribe({
      next: inv => { this.inventarioNuevo = inv.filter(i => i.stock > 0); this.loadingInventarioNuevo = false; },
      error: () => { this.loadingInventarioNuevo = false; }
    });

    const producto = this.productos.find(p => p.id === Number(this.idProductoNuevo));
    if (producto) this.precioNuevo = producto.preciobase ?? 0;
  }

  agregarProductoAPedido() {
    if (!this.pedidoSeleccionado || !this.idProductoNuevo || !this.inventarioSeleccionadoNuevo) return;
    if (this.cantidadNueva <= 0 || this.precioNuevo <= 0) return;
    if (this.cantidadNueva > this.inventarioSeleccionadoNuevo.stock) {
      this.errorAgregarDetalle = `Stock insuficiente. Disponible: ${this.inventarioSeleccionadoNuevo.stock}`;
      return;
    }

    this.loadingAgregarDetalle = true;
    this.errorAgregarDetalle = '';
    this.successAgregarDetalle = '';

    this.pedidoService.addDetalle(this.pedidoSeleccionado.id, {
      idpedido: this.pedidoSeleccionado.id,
      idproducto: Number(this.idProductoNuevo),
      idinventario: this.inventarioSeleccionadoNuevo.id,
      cantidad: this.cantidadNueva,
      preciounitario: this.precioNuevo
    }).subscribe({
      next: () => {
        this.successAgregarDetalle = '✓ Producto agregado.';
        this.loadingAgregarDetalle = false;
        this.resetFormAgregar();
        this.mostrarFormAgregar = false;
        // Recargar detalles y pedidos
        this.pedidoService.getDetalles(this.pedidoSeleccionado!.id).subscribe({
          next: d => { this.detalles = d; },
          error: () => {}
        });
        this.cargarPedidos();
      },
      error: () => { this.errorAgregarDetalle = 'No se pudo agregar el producto.'; this.loadingAgregarDetalle = false; }
    });
  }

  private resetFormAgregar() {
    this.idProductoNuevo = null;
    this.inventarioNuevo = [];
    this.inventarioSeleccionadoNuevo = null;
    this.cantidadNueva = 1;
    this.precioNuevo = 0;
    this.errorAgregarDetalle = '';
    this.successAgregarDetalle = '';
  }

  // ── Editar pedido ──
  abrirEditar(pedido: PedidoModel) {
    this.pedidoEditar = { ...pedido };
    this.pedidoService.getPedidoById(pedido.id).subscribe({
      next: (p: any) => {
        this.pedidoEditarIdCliente = p.idCliente ?? p.idcliente ?? 0;
        this.pedidoEditarIdUsuario = p.idUsuario ?? p.idusuario ?? 0;
      },
      error: () => {}
    });
    this.modalEditar = true;
  }

  guardarEdicion() {
    if (!this.pedidoEditar) return;
    this.loadingEditar = true;

    const updateDatos = this.pedidoService.updatePedido(this.pedidoEditar.id, {
      fechEntregaAprox: this.pedidoEditar.fechentregaaprox,
      fechaEntrega: this.pedidoEditar.fechaentrega,
      direccionEntrega: this.pedidoEditar.direccionentrega,
      observaciones: this.pedidoEditar.observaciones,
      descuento: this.pedidoEditar.descuento,
    });

    const updateEstado = this.pedidoService.cambiarEstado(
      this.pedidoEditar.id,
      this.pedidoEditar.estado
    );

    Promise.all([updateDatos.toPromise(), updateEstado.toPromise()]).then(() => {
      this.modalEditar = false;
      this.loadingEditar = false;
      this.cargarPedidos();
    }).catch(() => { this.loadingEditar = false; });
  }

  // ── Editar detalle ──
  abrirEditarDetalle(detalle: DetallePedidoDto) {
    this.detalleEditar = detalle;
    this.editarCantidad = detalle.cantidad;
    this.editarPrecio = detalle.preciounitario;
    this.modalEditarDetalle = true;
  }

  guardarDetalle() {
    if (!this.detalleEditar || !this.pedidoSeleccionado) return;
    this.loadingEditarDetalle = true;

    const dto: UpdateDetallePedidoDto = {
      idinventario: null,
      cantidad: this.editarCantidad,
      preciounitario: this.editarPrecio
    };

    this.pedidoService.updateDetalle(this.pedidoSeleccionado.id, this.detalleEditar.id, dto).subscribe({
      next: () => {
        this.modalEditarDetalle = false;
        this.loadingEditarDetalle = false;
        this.verDetalles(this.pedidoSeleccionado!);
      },
      error: () => { this.loadingEditarDetalle = false; }
    });
  }

  // ── Eliminar detalle ──
  eliminarDetalle(detalle: DetallePedidoDto) {
    if (!this.pedidoSeleccionado) return;
    this.pedidoService.deleteDetalle(this.pedidoSeleccionado.id, detalle.id).subscribe({
      next: () => {
        this.detalles = this.detalles.filter(d => d.id !== detalle.id);
        this.cargarPedidos();
      },
      error: () => {}
    });
  }

  // ── Eliminar pedido ──
  abrirEliminar(pedido: PedidoModel) {
    this.pedidoEliminar = pedido;
    this.errorEliminar = '';
    this.modalEliminar = true;
  }

  async confirmarEliminar() {
    if (!this.pedidoEliminar) return;
    this.loadingEliminar = true;
    this.errorEliminar = '';

    this.pedidoService.getDetalles(this.pedidoEliminar.id).subscribe({
      next: async (detalles) => {
        for (const detalle of detalles) {
          await this.pedidoService.deleteDetalle(this.pedidoEliminar!.id, detalle.id).toPromise();
        }
        this.pedidoService.deletePedido(this.pedidoEliminar!.id).subscribe({
          next: () => {
            this.pedidos = this.pedidos.filter(p => p.id !== this.pedidoEliminar!.id);
            this.modalEliminar = false;
            this.loadingEliminar = false;
          },
          error: () => { this.errorEliminar = 'No se pudo eliminar el pedido.'; this.loadingEliminar = false; }
        });
      },
      error: () => { this.errorEliminar = 'No se pudieron cargar los detalles.'; this.loadingEliminar = false; }
    });
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP', maximumFractionDigits: 0 }).format(value);
  }

  formatDate(date: string | null): string {
    if (!date) return '—';
    return new Date(date).toLocaleDateString('es-CO', { day: '2-digit', month: 'short', year: 'numeric' });
  }
}
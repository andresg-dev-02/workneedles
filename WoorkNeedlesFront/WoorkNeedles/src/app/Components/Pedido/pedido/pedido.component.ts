import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PedidoService } from '../../../Services/Pedido/pedido.service';
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

  readonly estadoOpciones = ['pendiente', 'en preparacion', 'enviado', 'entregado', 'cancelado'];

  readonly estadoClases: Record<string, string> = {
    'pendiente':      'bg-yellow-100 text-yellow-700',
    'en preparacion': 'bg-blue-100 text-blue-700',
    'enviado':        'bg-purple-100 text-purple-700',
    'entregado':      'bg-green-100 text-green-700',
    'cancelado':      'bg-red-100 text-red-700',
  };

  constructor(private pedidoService: PedidoService, private router: Router) {}

  ngOnInit() { this.cargarPedidos(); }

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

    this.pedidoService.getDetalles(pedido.id).subscribe({
      next: d => { this.detalles = d; this.loadingDetalles = false; },
      error: () => { this.loadingDetalles = false; }
    });
  }
  pedidoEditarIdCliente = 0;
  pedidoEditarIdUsuario = 0;
  // ── Editar pedido ──
  abrirEditar(pedido: PedidoModel) {
  this.pedidoEditar = { ...pedido };
  // Necesitamos los IDs reales — los traemos del pedido completo
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

    this.pedidoService.updatePedido(this.pedidoEditar.id, {
      idCliente: this.pedidoEditarIdCliente,
      idUsuario: this.pedidoEditarIdUsuario,
      fechEntregaAprox: this.pedidoEditar.fechentregaaprox,
      fechaEntrega: this.pedidoEditar.fechaentrega,
      direccionEntrega: this.pedidoEditar.direccionentrega,
      observaciones: this.pedidoEditar.observaciones,
      descuento: this.pedidoEditar.descuento
    }).subscribe({
      next: () => { this.modalEditar = false; this.loadingEditar = false; this.cargarPedidos(); },
      error: () => { this.loadingEditar = false; }
    });
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

    // 1. Cargar detalles del pedido
    this.pedidoService.getDetalles(this.pedidoEliminar.id).subscribe({
      next: async (detalles) => {
        // 2. Eliminar cada detalle
        for (const detalle of detalles) {
          await this.pedidoService.deleteDetalle(this.pedidoEliminar!.id, detalle.id).toPromise();
        }
        // 3. Eliminar el pedido
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
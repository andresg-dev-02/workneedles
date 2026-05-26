import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PagoService, PagoDto, CreatePagoDto } from '../../Services/Pago/pago.service';
import { PedidoService, PedidoDto } from '../../Services/Pedido/pedido.service';
import { AuthService } from '../../Services/Auth/auth.service';

@Component({
  selector: 'app-pagos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pagos-component.html',
})
export class PagosComponent implements OnInit {
  // Pedidos
  pedidos: PedidoDto[] = [];
  pedidoSeleccionado: PedidoDto | null = null;
  cargandoPedidos = false;
  errorPedidos: string | null = null;

  // Pagos
  pagos: PagoDto[] = [];
  cargandoPagos = false;
  errorPagos: string | null = null;

  // Modal crear
  modalCrear = false;
  loadingCrear = false;
  errorCrear: string | null = null;
  nuevoPago: Partial<CreatePagoDto> = this.formVacio();

  // Modal estado
  modalEstado = false;
  pagoEditando: PagoDto | null = null;
  nuevoEstado = '';
  loadingEstado = false;

  // Modal eliminar
  modalEliminar = false;
  pagoEliminar: PagoDto | null = null;
  loadingEliminar = false;

  readonly tiposPago = [
    'Efectivo',
    'Transferencia',
    'Tarjeta_credito',
    'Tarjeta_debito',
    'Credito_empresa'
  ];
  readonly estados = ['pendiente', 'completado', 'reembolsado', 'fallido'];

  constructor(
    private pagoService: PagoService,
    private pedidoService: PedidoService,
    private authService: AuthService,
  ) {}

  ngOnInit(): void {
    this.cargarPedidos();
  }

  private formVacio(): Partial<CreatePagoDto> {
    return { monto: 0, tipopago: '', referencia: '', observaciones: '' };
  }

  cargarPedidos(): void {
    this.cargandoPedidos = true;
    this.pedidoService.getPedidos().subscribe({
      next: (data) => { this.pedidos = data; this.cargandoPedidos = false; },
      error: () => { this.errorPedidos = 'Error al cargar pedidos.'; this.cargandoPedidos = false; },
    });
  }

  seleccionarPedido(pedido: PedidoDto): void {
    if (this.pedidoSeleccionado?.id === pedido.id) {
      this.pedidoSeleccionado = null;
      this.pagos = [];
      return;
    }
    this.pedidoSeleccionado = pedido;
    this.cargarPagos();
  }

  cargarPagos(): void {
    if (!this.pedidoSeleccionado) return;
    this.cargandoPagos = true;
    this.errorPagos = null;

    this.pagoService.getPagos(this.pedidoSeleccionado.id).subscribe({
      next: (data) => { this.pagos = data; this.cargandoPagos = false; },
      error: () => { this.errorPagos = 'Error al cargar pagos.'; this.cargandoPagos = false; },
    });
  }

  // ── Crear ──────────────────────────────────────────────────
  abrirCrear(): void {
    this.nuevoPago = this.formVacio();
    this.errorCrear = null;
    this.modalCrear = true;
  }

  crearPago(): void {
    if (!this.pedidoSeleccionado) return;
    if (!this.nuevoPago.tipopago || !this.nuevoPago.monto || this.nuevoPago.monto <= 0) {
      this.errorCrear = 'Completa los campos requeridos.';
      return;
    }
    this.loadingCrear = true;
    this.errorCrear = null;

    const dto: CreatePagoDto = {
      idpedido: this.pedidoSeleccionado.id,
      idusuario: this.authService.getUsuarioId(),
      monto: this.nuevoPago.monto!,
      tipopago: this.nuevoPago.tipopago!,
      referencia: this.nuevoPago.referencia,
      observaciones: this.nuevoPago.observaciones,
    };

    this.pagoService.createPago(this.pedidoSeleccionado.id, dto).subscribe({
      next: () => { this.modalCrear = false; this.loadingCrear = false; this.cargarPagos(); },
      error: () => { this.errorCrear = 'Error al registrar el pago.'; this.loadingCrear = false; },
    });
  }

  // ── Cambiar estado ─────────────────────────────────────────
  abrirEstado(pago: PagoDto): void {
  this.pagoEditando = pago;
  this.nuevoEstado = pago.estado.toLowerCase();  // <-- esto
  this.modalEstado = true;
}

 guardarEstado(): void {
  if (!this.pagoEditando || !this.pedidoSeleccionado) return;
  this.loadingEstado = true;

  this.pagoService.cambiarEstado(
    this.pedidoSeleccionado.id,
    this.pagoEditando.id,
    { estado: this.nuevoEstado }
  ).subscribe({
    next: () => { this.modalEstado = false; this.loadingEstado = false; this.cargarPagos(); },
    error: (err) => { 
      console.error('Error body:', err.error);
      this.loadingEstado = false; 
    },
  });
}

  // ── Eliminar ───────────────────────────────────────────────
  abrirEliminar(pago: PagoDto): void {
    this.pagoEliminar = pago;
    this.modalEliminar = true;
  }

  confirmarEliminar(): void {
    if (!this.pagoEliminar || !this.pedidoSeleccionado) return;
    this.loadingEliminar = true;

    this.pagoService.deletePago(this.pedidoSeleccionado.id, this.pagoEliminar.id).subscribe({
      next: () => { this.modalEliminar = false; this.loadingEliminar = false; this.cargarPagos(); },
      error: () => { this.loadingEliminar = false; },
    });
  }

  estadoClass(estado: string): string {
  switch (estado.toLowerCase()) {
    case 'completado':  return 'bg-green-100 text-green-700';
    case 'pendiente':   return 'bg-yellow-100 text-yellow-700';
    case 'fallido':     return 'bg-red-100 text-red-600';
    case 'reembolsado': return 'bg-blue-100 text-blue-700';
    default: return 'bg-gray-100 text-gray-600';
  }
}

  estaSeleccionado(pedido: PedidoDto): boolean {
    return this.pedidoSeleccionado?.id === pedido.id;
  }
}
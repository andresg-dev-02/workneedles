import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ClienteService, ClienteDto } from '../../../Services/Client/cliente.service';
import { PedidoService, CreatePedidoDto, CreateDetallePedidoDto } from '../../../Services/Pedido/pedido.service';
import { ProductoService, InventarioDto } from '../../../Services/Producto/producto.service';
import { PagoService, CreatePagoDto } from '../../../Services/Pago/pago.service';
import { ProductoModel } from '../../../Models/Producto/producto.model';
import { AuthService } from '../../../Services/Auth/auth.service';

type Paso = 'pedido' | 'detalles' | 'pago';

interface DetalleLocal {
  idproducto: number;
  idinventario: number;
  nombreProducto: string;
  talla: string;
  color: string;
  cantidad: number;
  preciounitario: number;
  subtotal: number;
}

@Component({
  selector: 'app-crear-pedido',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './crear-pedido-component.html',
})
export class CrearPedidoComponent implements OnInit {

  paso: Paso = 'pedido';

  // Paso 1 — datos del pedido
  clientes: ClienteDto[] = [];
  idClienteSeleccionado: number | null = null;
  fechEntregaAprox = '';
  fechaEntrega = '';
  direccionEntrega = '';
  observaciones = '';
  descuento: number | null = null;

  // Paso 2 — detalles
  productos: ProductoModel[] = [];
  idProductoSeleccionado: number | null = null;
  inventario: InventarioDto[] = [];
  inventarioSeleccionado: InventarioDto | null = null;
  cantidad = 1;
  preciounitario = 0;
  detalles: DetalleLocal[] = [];

  // Paso 3 — pago
  registrarPago = false;
  tipopago = '';
  montoPago = 0;
  referenciaPago: string | null = null;
  observacionesPago: string | null = null;

  readonly tiposPago = [
    { value: 'efectivo',         label: 'Efectivo' },
    { value: 'transferencia',    label: 'Transferencia' },
    { value: 'tarjeta_credito',  label: 'Tarjeta de crédito' },
    { value: 'tarjeta_debito',   label: 'Tarjeta de débito' },
    { value: 'credito_empresa',  label: 'Crédito empresa' },
  ];

  // Estado
  pedidoCreadoId: number | null = null;
  loading = false;
  loadingInventario = false;
  loadingDetalle = false;
  loadingPago = false;
  error = '';
  successMsg = '';

  constructor(
    private clienteService: ClienteService,
    private productoService: ProductoService,
    private pedidoService: PedidoService,
    private pagoService: PagoService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.clienteService.getClientes().subscribe({
      next: c => this.clientes = c.filter(x => x.activo),
      error: () => this.error = 'No se pudieron cargar los clientes.'
    });
    this.productoService.getProductos().subscribe({
      next: p => this.productos = p,
      error: () => this.error = 'No se pudieron cargar los productos.'
    });
  }

  get clienteSeleccionado(): ClienteDto | null {
    return this.clientes.find(c => c.id === Number(this.idClienteSeleccionado)) ?? null;
  }

  get totalDetalles(): number {
    return this.detalles.reduce((s, d) => s + d.subtotal, 0);
  }

  get totalConDescuento(): number {
    return this.totalDetalles - (this.descuento ?? 0);
  }

  puedeCrearPedido(): boolean {
    return !!this.idClienteSeleccionado &&
      !!this.fechEntregaAprox &&
      !!this.fechaEntrega &&
      !!this.direccionEntrega;
  }

  // ── Paso 1: crear pedido ──
  crearPedido() {
    if (!this.puedeCrearPedido()) return;
    this.loading = true;
    this.error = '';

    const dto: CreatePedidoDto = {
      idCliente: Number(this.idClienteSeleccionado),
      idUsuario: this.authService.getUsuarioId(),
      fechEntregaAprox: this.fechEntregaAprox,
      fechaEntrega: this.fechaEntrega,
      direccionEntrega: this.direccionEntrega,
      observaciones: this.observaciones,
      descuento: this.descuento
    };

    this.pedidoService.createPedido(dto).subscribe({
      next: () => {
        this.pedidoService.getPedidos().subscribe({
          next: pedidos => {
            const ultimo = pedidos[pedidos.length - 1];
            this.pedidoCreadoId = ultimo.id;
            this.montoPago = this.totalConDescuento;
            this.paso = 'detalles';
            this.loading = false;
          },
          error: () => { this.error = 'Pedido creado pero no se pudo obtener su ID.'; this.loading = false; }
        });
      },
      error: () => { this.error = 'No se pudo crear el pedido.'; this.loading = false; }
    });
  }

  // ── Paso 2: detalles ──
  onProductoChange() {
    this.inventario = [];
    this.inventarioSeleccionado = null;
    this.preciounitario = 0;
    if (!this.idProductoSeleccionado) return;

    this.loadingInventario = true;
    this.productoService.getInventario(Number(this.idProductoSeleccionado)).subscribe({
      next: inv => { this.inventario = inv.filter(i => i.stock > 0); this.loadingInventario = false; },
      error: () => { this.loadingInventario = false; }
    });

    const producto = this.productos.find(p => p.id === Number(this.idProductoSeleccionado));
    if (producto) this.preciounitario = producto.preciobase ?? 0;
  }

  agregarDetalle() {
    if (!this.idProductoSeleccionado || !this.inventarioSeleccionado || this.cantidad <= 0 || this.preciounitario <= 0) return;
    if (!this.pedidoCreadoId) return;
    if (this.cantidad > this.inventarioSeleccionado.stock) {
      this.error = `Stock insuficiente. Disponible: ${this.inventarioSeleccionado.stock}`;
      return;
    }

    this.loadingDetalle = true;
    this.error = '';
    this.successMsg = '';

    const dto: CreateDetallePedidoDto = {
      idpedido: this.pedidoCreadoId,
      idproducto: Number(this.idProductoSeleccionado),
      idinventario: this.inventarioSeleccionado.id,
      cantidad: this.cantidad,
      preciounitario: this.preciounitario
    };

    this.pedidoService.addDetalle(this.pedidoCreadoId, dto).subscribe({
      next: () => {
        this.detalles.push({
          idproducto: Number(this.idProductoSeleccionado),
          idinventario: this.inventarioSeleccionado!.id,
          nombreProducto: this.inventarioSeleccionado!.nombreProducto,
          talla: this.inventarioSeleccionado!.nombreTalla,
          color: this.inventarioSeleccionado!.nombreColor,
          cantidad: this.cantidad,
          preciounitario: this.preciounitario,
          subtotal: this.cantidad * this.preciounitario
        });
        this.montoPago = this.totalConDescuento;
        this.successMsg = '✓ Producto agregado correctamente.';
        this.resetDetalle();
        this.loadingDetalle = false;
      },
      error: () => { this.error = 'No se pudo agregar el producto.'; this.loadingDetalle = false; }
    });
  }

  private resetDetalle() {
    this.idProductoSeleccionado = null;
    this.inventario = [];
    this.inventarioSeleccionado = null;
    this.cantidad = 1;
    this.preciounitario = 0;
  }

  irAPago() {
    this.montoPago = this.totalConDescuento;
    this.paso = 'pago';
  }

  // ── Paso 3: pago ──
  registrarPagoAhora() {
    if (!this.pedidoCreadoId || !this.tipopago || this.montoPago <= 0) return;
    this.loadingPago = true;
    this.error = '';

    const dto: CreatePagoDto = {
      idpedido: this.pedidoCreadoId,
      idusuario: this.authService.getUsuarioId(),
      monto: this.montoPago,
      tipopago: this.tipopago,
      referencia: this.referenciaPago || null,
      observaciones: this.observacionesPago || null
    };

    this.pagoService.createPago(this.pedidoCreadoId, dto).subscribe({
      next: () => { this.loadingPago = false; this.router.navigate(['/gestion/pedidos']); },
      error: () => { this.error = 'No se pudo registrar el pago.'; this.loadingPago = false; }
    });
  }

  finalizarSinPago() {
    this.router.navigate(['/gestion/pedidos']);
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('es-CO', {
      style: 'currency', currency: 'COP', maximumFractionDigits: 0
    }).format(value);
  }
}
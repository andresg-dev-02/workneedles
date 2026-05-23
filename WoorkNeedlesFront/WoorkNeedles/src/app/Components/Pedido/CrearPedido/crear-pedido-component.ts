import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ClienteService } from '../../../Services/Client/cliente.service';
import { ClienteDto } from '../../../Services/Client/cliente.service';
import { PedidoService, CreatePedidoDto, CreateDetallePedidoDto } from '../../../Services/Pedido/pedido.service';
import { ProductoService } from '../../../Services/Producto/producto.service';
import { ProductoModel } from '../../../Models/Producto/producto.model';
import { AuthService } from '../../../Services/Auth/auth.service';

type Paso = 'pedido' | 'detalles';

interface DetalleLocal {
  idproducto: number;
  nombreProducto: string;
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

  // Datos del pedido
  clientes: ClienteDto[] = [];
  idClienteSeleccionado: number | null = null;
  fechEntregaAprox = '';
  fechaEntrega = '';
  direccionEntrega = '';
  observaciones = '';
  descuento: number | null = null;

  // Datos del detalle
  productos: ProductoModel[] = [];
  idProductoSeleccionado: number | null = null;
  cantidad = 1;
  preciounitario = 0;
  detalles: DetalleLocal[] = [];

  // Estado
  pedidoCreadoId: number | null = null;
  loading = false;
  loadingDetalle = false;
  error = '';
  successMsg = '';

  constructor(
    private clienteService: ClienteService,
    private productoService: ProductoService,
    private pedidoService: PedidoService,
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

  get productoSeleccionado(): ProductoModel | null {
    return this.productos.find(p => p.id === Number(this.idProductoSeleccionado)) ?? null;
  }

  get totalDetalles(): number {
    return this.detalles.reduce((s, d) => s + d.subtotal, 0);
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
        // Obtener el último pedido creado para sacar su ID
        this.pedidoService.getPedidos().subscribe({
          next: pedidos => {
            const ultimo = pedidos[pedidos.length - 1];
            this.pedidoCreadoId = ultimo.id;
            this.paso = 'detalles';
            this.loading = false;
          },
          error: () => { this.error = 'Pedido creado pero no se pudo obtener su ID.'; this.loading = false; }
        });
      },
      error: () => { this.error = 'No se pudo crear el pedido.'; this.loading = false; }
    });
  }

  // ── Paso 2: agregar detalles ──
  onProductoChange() {
    const p = this.productoSeleccionado;
    if (p) this.preciounitario = p.preciobase ?? 0;
  }

  agregarDetalle() {
    if (!this.idProductoSeleccionado || this.cantidad <= 0 || this.preciounitario <= 0) return;
    const producto = this.productoSeleccionado;
    if (!producto || !this.pedidoCreadoId) return;

    this.loadingDetalle = true;
    this.error = '';
    this.successMsg = '';

    const dto: CreateDetallePedidoDto = {
      idpedido: this.pedidoCreadoId,
      idproducto: Number(this.idProductoSeleccionado),
      idinventario: null,
      cantidad: this.cantidad,
      preciounitario: this.preciounitario
    };

    this.pedidoService.addDetalle(this.pedidoCreadoId, dto).subscribe({
      next: () => {
        this.detalles.push({
          idproducto: Number(this.idProductoSeleccionado),
          nombreProducto: producto.nombre,
          cantidad: this.cantidad,
          preciounitario: this.preciounitario,
          subtotal: this.cantidad * this.preciounitario
        });
        this.successMsg = 'Producto agregado.';
        this.resetDetalle();
        this.loadingDetalle = false;
      },
      error: () => { this.error = 'No se pudo agregar el producto.'; this.loadingDetalle = false; }
    });
  }

  private resetDetalle() {
    this.idProductoSeleccionado = null;
    this.cantidad = 1;
    this.preciounitario = 0;
  }

  finalizarPedido() {
    this.router.navigate(['/admin/pedidos']);
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('es-CO', {
      style: 'currency', currency: 'COP', maximumFractionDigits: 0
    }).format(value);
  }
}
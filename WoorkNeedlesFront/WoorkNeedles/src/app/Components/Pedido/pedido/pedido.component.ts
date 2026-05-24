import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PedidoService } from '../../../Services/Pedido/pedido.service';
import { PedidoModel } from '../../../Models/Pedido/pedido.model';

@Component({
  selector: 'app-pedido',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './pedido.component.html',
})
export class PedidoComponent implements OnInit {

  pedidos: PedidoModel[] = [];
  loading = false;
  error = '';

  readonly estadoClases: Record<string, string> = {
    'pendiente':       'bg-yellow-100 text-yellow-700',
    'en preparacion':  'bg-blue-100 text-blue-700',
    'enviado':         'bg-purple-100 text-purple-700',
    'entregado':       'bg-green-100 text-green-700',
    'cancelado':       'bg-red-100 text-red-700',
  };

  constructor(
    private pedidoService: PedidoService,
    private router: Router
  ) {}

  ngOnInit() {
    this.cargarPedidos();
  }

  cargarPedidos() {
    this.loading = true;
    this.error = '';
    this.pedidoService.getPedidos().subscribe({
      next: p => { this.pedidos = p; console.log(p); this.loading = false; },
      error: () => { this.error = 'No se pudieron cargar los pedidos.'; this.loading = false; }
    });
  }

  nuevoPedido() {
    this.router.navigate(['/admin/pedidos/nuevo']);
  }

  verDetalle(id: number) {
    this.router.navigate(['/admin/pedidos', id]);
  }

  estadoClase(estado: string): string {
    return this.estadoClases[estado.toLowerCase()] ?? 'bg-gray-100 text-gray-600';
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('es-CO', {
      style: 'currency', currency: 'COP', maximumFractionDigits: 0
    }).format(value);
  }

  formatDate(date: string | null): string {
    if (!date) return '—';
    return new Date(date).toLocaleDateString('es-CO', {
      day: '2-digit', month: 'short', year: 'numeric'
    });
  }
}
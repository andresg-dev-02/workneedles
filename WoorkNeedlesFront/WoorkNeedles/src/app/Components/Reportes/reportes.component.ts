import { Component, OnInit, ChangeDetectorRef  } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ReportesService,
  ProductoMasVendidoDto,
  IngresoMensualDto,
  FrecuenciaPedidoDto,
  ComportamientoClienteDto,
} from '../../Services/Reportes/reportes.service';

type TabKey = 'productos' | 'ingresos' | 'frecuencia' | 'comportamiento';

@Component({
  selector: 'app-reportes',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './reportes.component.html',
})
export class ReportesComponent implements OnInit {

  activeTab: TabKey = 'productos';

  productos: ProductoMasVendidoDto[] = [];
  ingresos: IngresoMensualDto[] = [];
  frecuencia: FrecuenciaPedidoDto[] = [];
  comportamiento: ComportamientoClienteDto[] = [];

  loading = false;
  error = '';

  tabs: { key: TabKey; label: string; icon: string }[] = [
    { key: 'productos',      label: 'Productos más vendidos', icon: 'M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10M4 7v10l8 4' },
    { key: 'ingresos',       label: 'Ingresos mensuales',     icon: 'M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z' },
    { key: 'frecuencia',     label: 'Frecuencia de pedidos',  icon: 'M4 4v5h.582m15.356 2A8.001 8.001 0 004.582 9m0 0H9m11 11v-5h-.581m0 0a8.003 8.003 0 01-15.357-2m15.357 2H15' },
    { key: 'comportamiento', label: 'Comportamiento clientes', icon: 'M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0zm6 3a2 2 0 11-4 0 2 2 0 014 0zM7 10a2 2 0 11-4 0 2 2 0 014 0z' },
  ];

  constructor(private reportesService: ReportesService, private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    this.loadTab(this.activeTab);
    console.log('loading:', this.loading);
  console.log('activeTab:', this.activeTab);
  }

  selectTab(tab: TabKey) {
    this.activeTab = tab;
    this.loadTab(tab);
  }

  private loadTab(tab: TabKey) {
  this.error = '';
  this.loading = true;
  this.cdr.detectChanges();

  const calls: Record<TabKey, () => void> = {
    productos: () => this.reportesService.getProductosMasVendidos().subscribe({
      next: d => { console.log('productos:', d); this.productos = d; this.loading = false; },
      error: (e) => { console.error('error productos:', e); this.onError(); }
    }),
    ingresos: () => this.reportesService.getIngresosMensuales().subscribe({
      next: d => { console.log('ingresos:', d); this.ingresos = d; this.loading = false; },
      error: (e) => { console.error('error ingresos:', e); this.onError(); }
    }),
    frecuencia: () => this.reportesService.getFrecuenciaPedidos().subscribe({
      next: d => { console.log('frecuencia:', d); this.frecuencia = d; this.loading = false; },
      error: (e) => { console.error('error frecuencia:', e); this.onError(); }
    }),
    comportamiento: () => this.reportesService.getComportamientoClientes().subscribe({
      next: d => { console.log('comportamiento:', d); this.comportamiento = d; this.loading = false; },
      error: (e) => { console.error('error comportamiento:', e); this.onError(); }
    }),
  };

  calls[tab]();
}

  private onError() {
    this.loading = false;
    this.error = 'No se pudo cargar la información. Intenta de nuevo.';
    this.cdr.detectChanges();
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP', maximumFractionDigits: 0 }).format(value);
  }

  formatDate(date: string | null): string {
    if (!date) return '—';
    return new Date(date).toLocaleDateString('es-CO', { day: '2-digit', month: 'short', year: 'numeric' });
  }

  get maxVendido(): number {
    if (!this.productos.length) return 1;
    return Math.max(...this.productos.map(p => p.totalVendido));
  }

  get maxIngresos(): number {
    if (!this.ingresos.length) return 1;
    return Math.max(...this.ingresos.map(i => i.totalIngresos));
  }

  get totalIngresosSum(): number {
    return this.ingresos.reduce((s, i) => s + i.totalIngresos, 0);
  }

  get totalPedidosSum(): number {
    return this.ingresos.reduce((s, i) => s + i.totalPedidos, 0);
  }
}
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AuthService } from '../Auth/auth.service';

export interface ProductoMasVendidoDto {
  nombreProducto: string;
  totalVendido: number;
  totalIngresos: number;
}

export interface IngresoMensualDto {
  mes: string;
  anio: number;
  totalIngresos: number;
  totalPedidos: number;
}

export interface FrecuenciaPedidoDto {
  nombreCliente: string;
  totalPedidos: number;
  totalGastado: number;
}

export interface ComportamientoClienteDto {
  nombreCliente: string;
  totalPedidos: number;
  totalGastado: number;
  ultimoPedido: string | null;
  productoFavorito: string;
}

@Injectable({ providedIn: 'root' })
export class ReportesService {

  private apiUrl = environment.apiUrl + '/Reportes';

  constructor(private http: HttpClient, private authService: AuthService) {}

  private headers(): HttpHeaders {
    return new HttpHeaders({ Authorization: `Bearer ${this.authService.getToken()}` });
  }

  getProductosMasVendidos(): Observable<ProductoMasVendidoDto[]> {
   
    return this.http.get<ProductoMasVendidoDto[]>(
      `${this.apiUrl}/productos-mas-vendidos`,
      { headers: this.headers() }
    );
    
  }

  getIngresosMensuales(): Observable<IngresoMensualDto[]> {
    return this.http.get<IngresoMensualDto[]>(
      `${this.apiUrl}/ingresos-mensuales`,
      { headers: this.headers() }
    );
  }

  getFrecuenciaPedidos(): Observable<FrecuenciaPedidoDto[]> {
    return this.http.get<FrecuenciaPedidoDto[]>(
      `${this.apiUrl}/frecuencia-pedidos`,
      { headers: this.headers() }
    );
  }

  getComportamientoClientes(): Observable<ComportamientoClienteDto[]> {
    return this.http.get<ComportamientoClienteDto[]>(
      `${this.apiUrl}/comportamiento-clientes`,
      { headers: this.headers() }
    );
  }
}
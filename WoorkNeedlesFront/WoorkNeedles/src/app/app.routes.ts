import { Routes } from '@angular/router';
import { ProductoComponent } from './Components/Producto/producto/producto.component';
import { LoginComponent } from './Components/Auth/login/login.component';
import { ReportesComponent } from './Components/Reportes/reportes.component';
import { PedidoComponent } from './Components/Pedido/pedido/pedido.component';
import { CrearPedidoComponent } from './Components/Pedido/CrearPedido/crear-pedido-component';

export const routes: Routes = [
  { path: 'productos', component: ProductoComponent },
  { path: 'login', component: LoginComponent },
  { path: 'admin/reportes', component: ReportesComponent },
  { path: '', redirectTo: 'productos', pathMatch: 'full' },
  { path: 'admin/pedidos', component: PedidoComponent},
  { path: 'admin/pedidos/nuevo', component: CrearPedidoComponent}
];
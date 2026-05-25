import { Routes } from '@angular/router';
import { ProductoComponent } from './Components/Producto/producto/producto.component';
import { LoginComponent } from './Components/Auth/login/login.component';
import { ReportesComponent } from './Components/Reportes/reportes.component';
import { PedidoComponent } from './Components/Pedido/pedido/pedido.component';
import { CrearPedidoComponent } from './Components/Pedido/CrearPedido/crear-pedido-component';
import { ClienteComponent } from './Components/Client/cliente.component';
import { ColoresComponent } from './Components/Colores/colores.component';
import { InsumosComponent } from './Components/Insumo/insumo-component';
import { TallasComponent } from './Components/Tallas/tallas-component';
import { CategoriaPComponent } from './Components/CategoriaProducto/categoria-p-component';
import { CategoriaInsumoComponent } from './Components/CategoriaInsumo/categoria-insumo.component';
import { InsumoProductoComponent } from './Components/Inventario/inventario-component';
import { ProductoBaseComponent } from './Components/ProductosBase/producto-base-component';
import { adminGuard, empleadoGuard } from './Guards/auth-guard';

export const routes: Routes = [
  { path: 'productos', component: ProductoComponent },
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: 'productos', pathMatch: 'full' },
  

  {
  path: 'admin',
  canActivate: [adminGuard],
  children:[
    { path: 'usuarios', component: LoginComponent },
    { path: 'colores', component: ColoresComponent },
    { path: 'pedidos/nuevo', component: CrearPedidoComponent},
    { path: 'clientes', component: ClienteComponent },
    { path: 'insumos', component: InsumosComponent },
    { path: 'tallas', component: TallasComponent },
    { path: 'categorias', component: CategoriaPComponent },
    { path: 'categorias-insumos', component: CategoriaInsumoComponent },
    { path: 'productos-base', component: ProductoBaseComponent }
  ]
},
  {
  path: 'gestion',
  canActivate: [empleadoGuard],
  children: [
    { path: 'pedidos', component: PedidoComponent},
    { path: 'reportes', component: ReportesComponent },
    { path: 'inventario', component: InsumoProductoComponent },
  ],
},
];
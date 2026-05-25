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
import { InsumoProductoComponent } from './Components/InsumoProducto/insumo-producto.component';
import { ProductoBaseComponent } from './Components/ProductosBase/producto-base-component';

export const routes: Routes = [
  { path: 'productos', component: ProductoComponent },
  { path: 'login', component: LoginComponent },
  { path: 'admin/usuarios', component: LoginComponent },
  { path: 'admin/colores', component: ColoresComponent },
  { path: 'admin/reportes', component: ReportesComponent },
  { path: '', redirectTo: 'productos', pathMatch: 'full' },
  { path: 'admin/pedidos', component: PedidoComponent},
  { path: 'admin/pedidos/nuevo', component: CrearPedidoComponent},
  { path: 'admin/clientes', component: ClienteComponent },
  { path: 'admin/insumos', component: InsumosComponent },
  { path: 'admin/tallas', component: TallasComponent },
  { path: 'admin/categorias', component: CategoriaPComponent },
  { path: 'admin/categorias-insumos', component: CategoriaInsumoComponent },
  { path: 'admin/gestor-productos-insumo', component: InsumoProductoComponent },
  { path: 'admin/productos-base', component: ProductoBaseComponent }
];
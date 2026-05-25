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
      { path: 'admin/usuarios', component: LoginComponent },
      { path: 'admin/colores', component: ColoresComponent },
      
      
      { path: 'admin/pedidos/nuevo', component: CrearPedidoComponent},
      { path: 'admin/clientes', component: ClienteComponent },
      { path: 'admin/insumos', component: InsumosComponent },
      { path: 'admin/tallas', component: TallasComponent },
      { path: 'admin/categorias', component: CategoriaPComponent },
      { path: 'admin/categorias-insumos', component: CategoriaInsumoComponent },
      
      { path: 'admin/productos-base', component: ProductoBaseComponent }


    ]
  },
  {
    path: 'gestion',
    canActivate: [empleadoGuard],
    children: [
      { path: 'admin/pedidos', component: PedidoComponent},
      { path: 'admin/reportes', component: ReportesComponent },
      { path: 'admin/inventario', component: InsumoProductoComponent },
    ],
  },
];
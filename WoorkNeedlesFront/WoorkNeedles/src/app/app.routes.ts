import { Routes } from '@angular/router';
import { ProductoComponent } from './Components/Producto/producto/producto.component';
import { LoginComponent } from './Components/Auth/login/login.component';
import { ReportesComponent } from './Components/Reportes/reportes.component';

export const routes: Routes = [
  { path: 'productos', component: ProductoComponent },
  { path: 'login', component: LoginComponent },
  { path: 'admin/reportes', component: ReportesComponent },
  { path: '', redirectTo: 'productos', pathMatch: 'full' },
];
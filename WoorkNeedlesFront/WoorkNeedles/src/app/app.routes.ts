import { Routes } from '@angular/router';
import { ProductoComponent } from './Components/Producto/producto/producto.component';
import { LoginComponent } from './Components/Auth/login/login.component';

export const routes: Routes = [
  { path: 'productos', component: ProductoComponent },
  { path: '', redirectTo: 'productos', pathMatch: 'full' },
  { path: 'login', component: LoginComponent}
];
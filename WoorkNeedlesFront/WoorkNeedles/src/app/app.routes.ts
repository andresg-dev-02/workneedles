import { Routes } from '@angular/router';
import { ProductoComponent } from './Components/Producto/producto/producto.component';

export const routes: Routes = [
  { path: 'productos', component: ProductoComponent },
  { path: '', redirectTo: 'productos', pathMatch: 'full' } // página de inicio
];
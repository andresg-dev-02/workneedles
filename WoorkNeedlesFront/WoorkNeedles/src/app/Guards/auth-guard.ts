import { CanActivateFn } from '@angular/router';
import { Router } from '@angular/router';

export const authGuard: CanActivateFn = () => {
  const router = new Router();

  const token = localStorage.getItem('token');

  if (!token) {

    window.location.href = '/producto';

    return false;

  }

  return true;
};

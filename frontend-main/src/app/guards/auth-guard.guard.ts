import { Injectable, inject } from '@angular/core';
import { 
  CanActivateFn, 
  Router, 
  ActivatedRouteSnapshot, //gives snapshot of curret activated route or url
  RouterStateSnapshot //gives current status of state within localstorage or session management
} from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot ) => {
  const router: Router = inject(Router);
  const _authService: AuthService = inject(AuthService);

  const protectedRoutes: RegExp[] = [
    /^\/booking\/\d+$/, // Matches /booking/:id where :id is a number , route: '/booking/:id'
    /^\/booking-history$/,  // '/booking-history'
    /^\/profile$/,  // '/profile'
    /^\/profile\/edit$/  // '/profile/edit'
  ];

  const isProtectedRoute = protectedRoutes.some(routeRegex => routeRegex.test(state.url));
  
  if (isProtectedRoute && !_authService.isAuthenticated()) {
    return router.navigate(['/login']);
  }
  
  return true;
};

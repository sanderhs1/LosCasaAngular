import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
import { Observable, of } from 'rxjs'; 

import { AuthService } from '../login/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ExpenseGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree > {

    let url: string = state.url;

    return this.checkLogin(url);
  }

  checkLogin(url: string): Observable<boolean | UrlTree> {
    let val: string | null = localStorage.getItem('isUserLoggedIn');
    if ( val == "true") {
      if (url == "/login")
        return of(this.router.parseUrl('/expenses'));

      else
        return of(true);
    } else {
      return of(this.router.parseUrl('/login'));

    }
  }
}

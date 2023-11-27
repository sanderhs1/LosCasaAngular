import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { ExpenseGuard } from './expense.guard';
import { AuthService } from '../login/auth.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Observable, of } from 'rxjs';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';

describe('ExpenseGuard', () => {
  let guard: ExpenseGuard;
  let authService: AuthService;
  let router: Router;

  // Mock the ActivatedRouteSnapshot and RouterStateSnapshot
  const route = {} as ActivatedRouteSnapshot;
  const state = { url: '/login' } as RouterStateSnapshot;

  // Mock the Router's parseUrl method
  const routerMock = {
    navigate: jasmine.createSpy('navigate'),
    parseUrl: jasmine.createSpy('parseUrl').and.callFake((url: string) => {
      return new UrlTree();
    })
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule.withRoutes([])
      ],
      providers: [
        ExpenseGuard,
        { provide: AuthService, useValue: authService },
        { provide: Router, useValue: routerMock }
      ]
    });

    guard = TestBed.inject(ExpenseGuard);
    authService = TestBed.inject(AuthService);
    router = TestBed.inject(Router);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });

  it('should redirect an authenticated user to the expenses page when they try to access login', (done: DoneFn) => {
    const urlTree = new UrlTree();
    spyOn(guard, 'checkLogin').and.returnValue(of(urlTree));
    guard.canActivate(route, state).subscribe((result) => {
      expect(guard.checkLogin).toHaveBeenCalledWith('/login');
      expect(result).toBe(urlTree);
      done();
    });
  });

  it('should allow unauthenticated user to access login', (done: DoneFn) => {
    spyOn(guard, 'checkLogin').and.returnValue(of(true));
    guard.canActivate(route, state).subscribe((result) => {
      expect(guard.checkLogin).toHaveBeenCalledWith('/login');
      expect(result).toBe(true);
      done();
    });
  });
});

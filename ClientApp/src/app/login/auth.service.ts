import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authUrl = '/api/auth'; // Adjust if you have a different API URL

  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.authUrl}/login`, { email, password });

  }
  register(email: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.authUrl}/register`, { email, password });
  }
}

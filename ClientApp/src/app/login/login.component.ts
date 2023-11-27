import { Component } from '@angular/core';
import { AuthService } from './auth.service'; // Import the AuthService


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  user = {
    email: '',
    password: ''
  };
  constructor(private authService: AuthService) { } // AuthService injected here


  onLogin() {
    this.authService.login(this.user.email, this.user.password).subscribe({
      next: (response) => {
        console.log('Login successful:', response);
        // Here you would typically store the token and redirect the user
      },
      error: (error) => {
        console.error('Login failed:', error);
      }
    });
  }
}


import { Component } from '@angular/core';
import { AuthService } from '../login/auth.service'; 

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  user = {
    email: '',
    password: '',
    confirmPassword: ''
  };

  constructor(private authService: AuthService) { }

  onRegister() {
    if (this.user.password === this.user.confirmPassword) {
      this.authService.register(this.user.email, this.user.password).subscribe({
        next: (response) => {
          console.log('Registration successful:', response);
          // show success message
        },
        error: (error) => {
          console.error('Registration failed:', error);
          // Show error message
        }
      });
    } else {
      console.error('Passwords do not match');
      // Show some error to the user
    }
  }
}


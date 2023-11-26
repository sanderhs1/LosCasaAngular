import { Component } from '@angular/core';

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

  onLogin() {
    // Your login logic here, like calling a service to handle the login
    console.log('User login:', this.user);
  }
}


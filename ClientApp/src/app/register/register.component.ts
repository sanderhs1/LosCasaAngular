import { Component } from '@angular/core';

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

  onRegister() {
    console.log(this.user);
  }
}


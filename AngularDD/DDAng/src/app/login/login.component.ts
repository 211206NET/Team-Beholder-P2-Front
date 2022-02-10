import { Component, OnInit } from '@angular/core';
<<<<<<< HEAD
=======
import { AuthService } from '@auth0/auth0-angular';
import { Console } from 'console';
>>>>>>> 3c3e267c31066b09c7c93649da29b16bce3c1288

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

<<<<<<< HEAD
  constructor() { }

  ngOnInit(): void {
=======
  constructor(public auth: AuthService) { }

  logIn(){
    this.auth.loginWithRedirect();
  }

  LogOut(){
    this.auth.logout();
  }

  loggedIn: boolean = false;


  ngOnInit(): void {
    this.auth.isAuthenticated$.subscribe((isLoggedIn) =>
    {
      this.loggedIn = isLoggedIn;
      console.log(isLoggedIn);
    })
    
>>>>>>> 3c3e267c31066b09c7c93649da29b16bce3c1288
  }

}

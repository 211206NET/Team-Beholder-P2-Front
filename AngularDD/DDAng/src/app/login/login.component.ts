import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
<<<<<<< HEAD
<<<<<<< HEAD
import { Console } from 'console';
=======

>>>>>>> 7eba812b63d1f0bc7f65e3c2ad4cb0b5b0b0e72f
=======

>>>>>>> 7eba812b63d1f0bc7f65e3c2ad4cb0b5b0b0e72f

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(public auth: AuthService) { }

  logIn(){
    this.auth.loginWithRedirect();
  }

<<<<<<< HEAD
<<<<<<< HEAD
  LogOut(){
=======
  logOut(){
>>>>>>> 7eba812b63d1f0bc7f65e3c2ad4cb0b5b0b0e72f
=======
  logOut(){
>>>>>>> 7eba812b63d1f0bc7f65e3c2ad4cb0b5b0b0e72f
    this.auth.logout();
  }

  loggedIn: boolean = false;


  ngOnInit(): void {
    this.auth.isAuthenticated$.subscribe((isLoggedIn) =>
    {
      this.loggedIn = isLoggedIn;
      console.log(isLoggedIn);
    })
    
  }

}

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ScoreboardComponent } from './scoreboard/scoreboard.component';
import { NavbarComponent } from './navbar/navbar.component';
import { HttpClientModule } from '@angular/common/http';
import { HomePageComponent } from './home-page/home-page.component';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
<<<<<<< HEAD
import { VIP1Component } from './vip1/vip1.component';
<<<<<<< HEAD
=======
import { AuthModule } from '@auth0/auth0-angular';
import { environment } from 'src/environments/environment';
>>>>>>> 3c3e267c31066b09c7c93649da29b16bce3c1288
=======
import { AuthModule } from '@auth0/auth0-angular';
import { environment } from 'src/environments/environment';

>>>>>>> 7eba812b63d1f0bc7f65e3c2ad4cb0b5b0b0e72f

@NgModule({
  declarations: [
    AppComponent,
    ScoreboardComponent,
    NavbarComponent,
    HomePageComponent,
<<<<<<< HEAD
    LoginComponent,
    VIP1Component,
<<<<<<< HEAD
=======
    LoginComponent
>>>>>>> 3c3e267c31066b09c7c93649da29b16bce3c1288
=======
    LoginComponent
>>>>>>> 7eba812b63d1f0bc7f65e3c2ad4cb0b5b0b0e72f
  ],
  imports: [
    AuthModule.forRoot({
      domain: environment.authDomain,
      clientId: environment.authClientId
    }),
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

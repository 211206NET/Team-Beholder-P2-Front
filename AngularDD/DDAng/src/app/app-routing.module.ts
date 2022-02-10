import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { ScoreboardComponent } from './scoreboard/scoreboard.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from '@auth0/auth0-angular';

const routes: Routes = [
  // {
  //   path: "game/:id",
  //   component: 
  // },

  {
    path: 'scoreboard',
    component: ScoreboardComponent
  },

  {
    path: 'home',
    component : HomePageComponent
  },
  {
    path: 'login',
    component : LoginComponent,
    canActivate: [AuthGuard]
  },

  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

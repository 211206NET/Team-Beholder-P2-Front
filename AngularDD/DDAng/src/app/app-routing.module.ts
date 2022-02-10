import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginComponent } from './login/login.component';
import { ScoreboardComponent } from './scoreboard/scoreboard.component';
import { VIP1Component } from './vip1/vip1.component';


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
    component:LoginComponent
  },

  {
    path: 'vip1',
    component: VIP1Component
  },


  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

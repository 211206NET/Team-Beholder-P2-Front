import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { ScoreboardComponent } from './scoreboard/scoreboard.component';

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
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

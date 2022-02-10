import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { DdApiService } from '../Services/dd-api.service';
import { Scoreboard } from '../Models/scoreboard';
import { ScoreboardComponent } from '../scoreboard/scoreboard.component';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

  placeholder={
    username:'enter username'
  }
  constructor(private _apiService : DdApiService) { }

  proccessForm(userForm:NgForm)
  {
    console.log('form has been submitted')
    console.log(userForm.value);
    this._apiService.getUserByUsername(userForm.value)
  }

  userGameInfo: Scoreboard[]= [];
  clickme(username:string) {
    console.log(username);
    
    this._apiService.getUserByUsername(username)
    // this._apiService.getUserByUsername(username).then((scoreToArray) =>
    // {
    //   console.log(scoreToArray)
    //   this.userGameInfo = scoreToArray;
    // })
  }


  ngOnInit(): void {
  }

}

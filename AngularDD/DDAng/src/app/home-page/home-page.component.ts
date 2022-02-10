import { Component, OnInit } from '@angular/core';
//import { NgForm } from '@angular/forms';
// import { Scoreboard } from '../Models/scoreboard';
import { DdApiService } from '../Services/dd-api.service';
import { Scoreboard } from '../Models/scoreboard';


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


  userGameInfo : Scoreboard={
    id : 0,
    username : "",
    gamesPlayed : 0,
    gamesWon : 0,
    totalKills : 0
  }

  clickme(id:string) {
    console.log(id);
    
    this._apiService.getUserById(id).then((player)=>
    {
      this.userGameInfo.id = player.id
      this.userGameInfo.username = player.username
      this.userGameInfo.gamesPlayed = player.gamesPlayed
      this.userGameInfo.gamesWon = player.gamesWon
      this.userGameInfo.totalKills = player.totalKills
    })
    
  }


  ngOnInit(): void {
  }

}

import { Component, OnInit } from '@angular/core';
import { Scoreboard } from '../Models/scoreboard';
import { DdApiService } from '../Services/dd-api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-scoreboard',
  templateUrl: './scoreboard.component.html',
  styleUrls: ['./scoreboard.component.css']
})
export class ScoreboardComponent implements OnInit {

  constructor(private apiService : DdApiService, private router:Router) { }

  allScores: Scoreboard[]= [];

  userVIP : Scoreboard={
    id : 0,
    username : "",
    gamesPlayed : 0,
    gamesWon : 0,
    totalKills : 0
  }

  placeholder={
    username:'Enter User ID'
  }

  checkVIP(checkVIP : any){
    this.userVIP.id = checkVIP
    this.apiService.getUserById(this.userVIP.id).then((totalKills)=>
    {
      console.log(totalKills)
      this.userVIP.totalKills=totalKills.totalKills
      if(this.userVIP.totalKills>0)
      {
        this.router.navigate(['vip1']);
      }
      
    })
    
  }

  ngOnInit(): void {
    this.apiService.getAllScores().then((scoreToArray) =>
    {
      console.log(scoreToArray)
      this.allScores = scoreToArray;
    })
  }

}

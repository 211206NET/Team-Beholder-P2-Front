import { Component, OnInit } from '@angular/core';
import { Scoreboard } from '../Models/scoreboard';
import { DdApiService } from '../Services/dd-api.service';

@Component({
  selector: 'app-scoreboard',
  templateUrl: './scoreboard.component.html',
  styleUrls: ['./scoreboard.component.css']
})
export class ScoreboardComponent implements OnInit {

  constructor(private apiService : DdApiService) { }

  allScores: Scoreboard[]= [];

  ngOnInit(): void {
    this.apiService.getAllScores().then((scoreToArray) =>
    {
      console.log(scoreToArray)
      this.allScores = scoreToArray;
    })
  }

}

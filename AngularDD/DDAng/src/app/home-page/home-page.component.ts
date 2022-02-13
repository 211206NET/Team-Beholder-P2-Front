import { Component, OnInit } from '@angular/core';
<<<<<<< HEAD
<<<<<<< HEAD
import { NgForm } from '@angular/forms';
import { DdApiService } from '../Services/dd-api.service';
=======
=======
>>>>>>> 7eba812b63d1f0bc7f65e3c2ad4cb0b5b0b0e72f
//import { NgForm } from '@angular/forms';
// import { Scoreboard } from '../Models/scoreboard';
import { DdApiService } from '../Services/dd-api.service';
import { Scoreboard } from '../Models/scoreboard';

<<<<<<< HEAD
>>>>>>> 7eba812b63d1f0bc7f65e3c2ad4cb0b5b0b0e72f
=======
>>>>>>> 7eba812b63d1f0bc7f65e3c2ad4cb0b5b0b0e72f

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

  placeholder={
<<<<<<< HEAD
<<<<<<< HEAD
    username:'enter username'
  }
  constructor(private _apiService : DdApiService) { }

  proccessForm(userForm:NgForm)
  {
    console.log('form has been submitted')
    console.log(userForm.value);
    this._apiService.getUserByUsername(userForm.value)
  }


=======
    username:'Enter User ID number',
    newCharacter:'Enter a Username'
  }


=======
    username:'Enter User ID number',
    newCharacter:'Enter a Username'
  }


>>>>>>> 7eba812b63d1f0bc7f65e3c2ad4cb0b5b0b0e72f
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

    userToAdd : Scoreboard={
      id : 0,
      username : "",
      gamesPlayed : 0,
      gamesWon : 0,
      totalKills : 0
    }
  createUser(newUser : any){
    this.userToAdd.username = newUser
    
    this._apiService.addNewUser(this.userToAdd)
    
  }


<<<<<<< HEAD
>>>>>>> 7eba812b63d1f0bc7f65e3c2ad4cb0b5b0b0e72f
=======
>>>>>>> 7eba812b63d1f0bc7f65e3c2ad4cb0b5b0b0e72f
  ngOnInit(): void {
  }

}

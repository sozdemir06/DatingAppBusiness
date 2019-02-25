import { Component, OnInit } from '@angular/core';
import { UsersService } from '../_services/users.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model:any={};

  constructor(
    private userService:UsersService
  ) { }

  ngOnInit() {
    
  }

  login(){
    this.userService.login(this.model).subscribe(next=>{
      console.log("Successfuly logged in");
      console.log(next);
    },error=>{
      console.log("Failed to login app");
      console.log(error);
    })
  }

  loggedIn(){
    const token=localStorage.getItem("token");
    return !!token;
  }

  logout(){
    localStorage.removeItem("token");
    console.log("Loggout");
  }

}

import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UsersService } from './_services/users.service';
import { IUserWithToken } from './_models/IUserWithToken';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  jwtHelper=new JwtHelperService();

  constructor(
    private userService:UsersService
  ){}

  ngOnInit(){
    const token=localStorage.getItem("token");
    const user:IUserWithToken=JSON.parse(localStorage.getItem("user"));
    if(token)
    {
      this.userService.decodedtoken=this.jwtHelper.decodeToken(token);
    }
    if(user)
    {
      this.userService.currentUser=user;
      this.userService.changeMemberPhoto(user.photoUrl);

    }
  }

}

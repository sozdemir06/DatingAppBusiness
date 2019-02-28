import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UsersService } from './_services/users.service';

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
    if(token)
    {
      this.userService.decodedtoken=this.jwtHelper.decodeToken(token);
    }
  }

}

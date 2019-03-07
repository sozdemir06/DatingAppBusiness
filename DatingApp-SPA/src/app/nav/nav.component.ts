import { Component, OnInit } from '@angular/core';
import { UsersService } from '../_services/users.service';
import { SnackbarGlobalErrorService } from '../_services/snackbar-global-error.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model:any={};
photoUrl:string;

  constructor(
    public userService:UsersService,
    private alert:SnackbarGlobalErrorService,
    private router:Router
  ) { }

  ngOnInit() {
   this.userService.currentPhotoUrl.subscribe(data=>{
      this.photoUrl=data;
   })
  }

  login(){
    this.userService.login(this.model).subscribe(next=>{
      this.alert.message("success","Loged In Successfuly");
    },error=>{
       this.alert.message("error",error);
    },()=>{
      this.router.navigate(["/members"]);
    })
  }

  loggedIn(){
    return this.userService.loggedIn();
  }

  logout(){
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    this.alert.message("success","Logout Succesfuly.!");
    this.router.navigate(["/home"]);
  }

}

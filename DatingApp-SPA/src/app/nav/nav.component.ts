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

  constructor(
    public userService:UsersService,
    private alert:SnackbarGlobalErrorService,
    private router:Router
  ) { }

  ngOnInit() {
   
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
    this.router.navigate(["/home"]);
  }

}

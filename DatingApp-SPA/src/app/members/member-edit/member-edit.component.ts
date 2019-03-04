import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { IUser } from 'src/app/_models/IUser';
import { ActivatedRoute } from '@angular/router';
import { SnackbarGlobalErrorService } from 'src/app/_services/snackbar-global-error.service';
import { NgForm } from '@angular/forms';
import { UsersService } from 'src/app/_services/users.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
user:IUser;
@ViewChild("editForm") editForm:NgForm;
@HostListener("window:beforeunload",['$event'])
unloadNotification($event:any){
if(this.editForm.dirty){
  $event.returnValue=true;
}
}
  constructor(
    private route:ActivatedRoute,
    private alert:SnackbarGlobalErrorService,
    private userService:UsersService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data=>{
      this.user=data["user"];
    })
  }

  updateUser(){
    this.userService.updateUser(this.userService.decodedtoken.nameid,this.user).subscribe(data=>{
      this.alert.message("success" ,"Your  Profile Successfuly updated.!");
      this.editForm.reset(this.user);
    },error=>{
      this.alert.message("error",error);
    })
   
    
  }

}

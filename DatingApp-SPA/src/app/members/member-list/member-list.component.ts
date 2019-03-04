import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../_services/users.service';
import { SnackbarGlobalErrorService } from '../../_services/snackbar-global-error.service';
import { IUser } from '../../_models/IUser';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
users:IUser[];

  constructor(
    private userService:UsersService,
    private alert:SnackbarGlobalErrorService,
    private route:ActivatedRoute
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data=>{
      this.users=data["users"];
    })
  }

  getUsers(){
    this.userService.getUsers().subscribe(data=>{
      this.users=data;
    },error=>{
      this.alert.message("error",error);
    })
  }

}

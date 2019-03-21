import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../_services/users.service';
import { SnackbarGlobalErrorService } from '../../_services/snackbar-global-error.service';
import { IUser } from '../../_models/IUser';
import { ActivatedRoute } from '@angular/router';
import { IPagination, PaginatedResult } from 'src/app/_models/IPagination';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
users:IUser[];
user:IUser=JSON.parse(localStorage.getItem("user"));
pagination:IPagination;
genderList=[{value:"male",display:"Males"},{value:"female",display:"Females"}];
userParams:any={};
filterForms:FormGroup;

  constructor(
    private route:ActivatedRoute,
    private usersService:UsersService,
    private alert:SnackbarGlobalErrorService,
    private fb:FormBuilder
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data=>{
      this.users=data["users"].result;
      this.pagination=data["users"].pagination;
    });
    this.userParams.gender = this.user.gender === 'female' ? 'male' : 'female';
    this.userParams.minAge = 18;
    this.userParams.maxAge = 99;
  }

  resetFilters(){
    this.userParams.gender = this.user.gender === 'female' ? 'male' : 'female';
    this.userParams.minAge = 18;
    this.userParams.maxAge = 99;
    this.loadUsers();
  }

  onChangePage($event){
    this.pagination.currentPage=$event.pageIndex+1;
    this.loadUsers();
  }

  loadUsers(){
    this.usersService.getUsers(this.pagination.currentPage,this.pagination.itemsPerPage,this.userParams)
                    .subscribe((res:PaginatedResult<IUser[]>)=>{
                      this.users=res.result;
                      this.pagination=res.pagination;
                    },error=>{
                      this.alert.message("error",error);
                    }); 
  }




}

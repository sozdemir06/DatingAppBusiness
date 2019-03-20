import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../_services/users.service';
import { SnackbarGlobalErrorService } from '../../_services/snackbar-global-error.service';
import { IUser } from '../../_models/IUser';
import { ActivatedRoute } from '@angular/router';
import { IPagination, PaginatedResult } from 'src/app/_models/IPagination';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
users:IUser[];
pagination:IPagination;

  constructor(
    private route:ActivatedRoute,
    private usersService:UsersService,
    private alert:SnackbarGlobalErrorService,
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data=>{
      this.users=data["users"].result;
      this.pagination=data["users"].pagination;
    });
   
  }

  onChangePage($event){
    let pageNumber=$event.pageIndex+1;
    this.loadUsers(pageNumber,$event.pageSize);
  }

  loadUsers(page?:any,pageSize?:any){
    this.usersService.getUsers(page,pageSize)
                    .subscribe((res:PaginatedResult<IUser[]>)=>{
                      this.users=res.result;
                      this.pagination=res.pagination;
                    },error=>{
                      this.alert.message("error",error);
                    }); 
  }




}

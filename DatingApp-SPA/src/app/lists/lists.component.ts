import { Component, OnInit } from '@angular/core';
import { IUser } from '../_models/IUser';
import { ActivatedRoute } from '@angular/router';
import { UsersService } from '../_services/users.service';
import { SnackbarGlobalErrorService } from '../_services/snackbar-global-error.service';
import { IPagination, PaginatedResult } from '../_models/IPagination';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
users:IUser[];
likeParams:string;
pagination:IPagination;
  constructor(
    private route:ActivatedRoute,
    private usersService:UsersService,
    private alert:SnackbarGlobalErrorService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data=>{
      if(data["users"].result.length==0 && this.likeParams=='Likers'){
        this.alert.message("error","Not found members who you liked.!!");
      }
      if(data["users"].result.length==0 && this.likeParams=='Likees'){
        this.alert.message("error","Not found members who liked you.!!");
      }
      this.users=data["users"].result;
      this.pagination=data["users"].pagination;
    });
    this.likeParams="Likers";
  }

  onChangePage($event){
    this.pagination.currentPage=$event.pageIndex+1;
    this.loadUsers();
  }

  loadUsers(){
    this.usersService.getUserLikers(this.pagination.currentPage,this.pagination.itemsPerPage,this.likeParams)
                    .subscribe((res:PaginatedResult<IUser[]>)=>{
                     
                      this.users=res.result;
                      this.pagination=res.pagination;
                      if(this.users.length==0 && this.likeParams=='Likers'){
                        this.alert.message("error","Not found members who you liked.!!");
                      }
                      if(this.users.length==0 && this.likeParams=='Likees'){
                        this.alert.message("error","Not found members who liked you.!!");
                      }
                    },error=>{
                      this.alert.message("error",error);
                    }); 
  }

}

import { Component, OnInit } from '@angular/core';
import { IMessage } from '../_models/IMessage';
import { IPagination, PaginatedResult } from '../_models/IPagination';
import { UsersService } from '../_services/users.service';
import { ActivatedRoute } from '@angular/router';
import { SnackbarGlobalErrorService } from '../_services/snackbar-global-error.service';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
messages:IMessage[];
pagination:IPagination;
messageContainer="Unread";
displayedColumns:string[]=["Message","From/To","Send/Received","Action"];
dataSource=new MatTableDataSource<IMessage>();


  constructor(
    private usersService:UsersService,
    private route:ActivatedRoute,
    private alert:SnackbarGlobalErrorService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data=>{
      this.dataSource.data=data["messages"].result;
      this.pagination=data["messages"].pagination;
    })
  }

  loadMessages(){
    this.usersService.getUserMessages(this.usersService.decodedtoken.nameid,this.messageContainer,this.pagination.currentPage,this.pagination.itemsPerPage)
                    .subscribe((res:PaginatedResult<IMessage[]>)=>{
                      this.dataSource.data=res.result;
                      this.pagination=res.pagination;
                    },error=>{
                      this.alert.message("error",error);
                    }); 
  }

  deleteMesssage(messageId:number){
    const confirm=window.confirm("Are you sure delete this messages.?");
    
    if(confirm){
      this.usersService.deleteMessage(messageId,this.usersService.decodedtoken.nameid).subscribe(data=>{
        this.alert.message("success","Message successfuly deleted.!!");
        this.dataSource.data=this.dataSource.data.splice(this.dataSource.data.findIndex(m=>m.id==messageId),1);
        
      },error=>{
        this.alert.message("error",error);
      })
    }
   
  }

  onChangePage($event){
    this.pagination.currentPage=$event.pageIndex+1;
    this.loadMessages();
  }


}

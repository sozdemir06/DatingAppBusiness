import { Component, OnInit, Input } from '@angular/core';
import { IMessage } from 'src/app/_models/IMessage';
import { UsersService } from 'src/app/_services/users.service';
import { SnackbarGlobalErrorService } from 'src/app/_services/snackbar-global-error.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
@Input() recipientId:number;
@Input() recipientName:string;
messages:IMessage[];
newMessage:any={};

  constructor(
    private usersService:UsersService,
    private alert:SnackbarGlobalErrorService
  ) { }

  ngOnInit() {
    this.loadMessages();
  }

  loadMessages(){
    this.usersService.getMessagesThread(this.usersService.decodedtoken.nameid,this.recipientId).subscribe(data=>{
      this.messages=data;
    },error=>{
      this.alert.message("error",error);
    })
  }

  sendMessage(){
    this.newMessage.recipientId=this.recipientId;
    this.usersService.createMessage(this.usersService.decodedtoken.nameid,this.newMessage).subscribe((data:IMessage)=>{
      this.newMessage.content="";
      this.messages.unshift(data);
      this.alert.message("success","Message was send successfuly to "+this.recipientName);
    },error=>{
      this.alert.message("error",error);
    })
  }

}

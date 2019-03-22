import { Component, OnInit, Input } from '@angular/core';
import { IUser } from 'src/app/_models/IUser';
import { UsersService } from 'src/app/_services/users.service';
import { SnackbarGlobalErrorService } from 'src/app/_services/snackbar-global-error.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
@Input() user:IUser;

  constructor(
    private userService:UsersService,
    private alert:SnackbarGlobalErrorService
  ) { }

  ngOnInit() {
  }

  likeUser(recipientId:number){
    this.userService.likeUser(this.userService.decodedtoken.nameid,recipientId).subscribe(data=>{
      this.alert.message("success","You like:"+this.user.knownAs);
    },error=>{
      this.alert.message("error",error);
    })

  }

}

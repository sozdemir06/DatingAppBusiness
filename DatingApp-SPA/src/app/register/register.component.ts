import { Component, OnInit,Input, Output, EventEmitter } from '@angular/core';
import { UsersService } from '../_services/users.service';
import { SnackbarGlobalErrorService } from '../_services/snackbar-global-error.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
@Output() cancelRegister=new EventEmitter();
model:any={};

  constructor(
    private usersService:UsersService,
    private alert:SnackbarGlobalErrorService
  ) { }

  ngOnInit() {

  }

cancel(){
    this.cancelRegister.emit(false);
}

register(){
  this.usersService.register(this.model).subscribe(()=>{
    this.alert.message("success","LoggedIn Successfuly");
  },error=>{
    this.alert.message("error",error);
  })
}

}

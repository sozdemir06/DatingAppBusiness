import { Component, OnInit,Input, Output, EventEmitter } from '@angular/core';
import { UsersService } from '../_services/users.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
@Output() cancelRegister=new EventEmitter();
model:any={};

  constructor(
    private usersService:UsersService
  ) { }

  ngOnInit() {

  }

cancel(){
    this.cancelRegister.emit(false);
}

register(){
  this.usersService.register(this.model).subscribe(()=>{
    console.log("Registered.!!");
  },error=>{
    console.log(error);
  })
}

}

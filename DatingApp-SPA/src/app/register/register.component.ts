import { Component, OnInit,Input, Output, EventEmitter } from '@angular/core';
import { UsersService } from '../_services/users.service';
import { SnackbarGlobalErrorService } from '../_services/snackbar-global-error.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { IUser } from '../_models/IUser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
@Output() cancelRegister=new EventEmitter();
user:IUser;
registerForm:FormGroup;

  constructor(
    private usersService:UsersService,
    private alert:SnackbarGlobalErrorService,
    private fb:FormBuilder,
    private router:Router
  ) { } 

  ngOnInit() {
    this.createRegisterFrom();
  }

createRegisterFrom(){
  this.registerForm=this.fb.group({
    gender:["male"],
    email:["",[Validators.required,Validators.email]],
    userName:["",Validators.required],
    knownAs:["",Validators.required],
    dateOfBirth:[null,Validators.required],
    city:["",Validators.required],
    country:["",Validators.required],
    password:["",[Validators.required,Validators.minLength(4),Validators.maxLength(8)]],
    confirmpassword:["",Validators.required]
  },{validator:this.checkConfirmPassword});
}

checkConfirmPassword(g:FormGroup){
  return g.get("password").value===g.get("confirmpassword").value?null:{'mismatch':true};
}

cancel(){
    this.cancelRegister.emit(false);
}

register(){
  if(this.registerForm.valid){
    this.user=Object.assign({},this.registerForm.value);
    this.usersService.register(this.user).subscribe(data=>{
      this.alert.message("success","Registration successfull.!");
    },error=>{
      this.alert.message("error",error);
    },()=>{
      this.usersService.login(this.user).subscribe(data=>{
        this.router.navigate(["/members"]);
      })
    })
  }
}

}

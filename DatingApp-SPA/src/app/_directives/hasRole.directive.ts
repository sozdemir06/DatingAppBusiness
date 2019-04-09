import { Directive, Input, ViewContainerRef, TemplateRef, OnInit } from '@angular/core';
import { UsersService } from '../_services/users.service';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit{
@Input() appHasRole:string[];
isVisible:boolean=false;

  constructor(
      private viewContainerRef:ViewContainerRef,
      private templateRef:TemplateRef<any>,
      private userService:UsersService
  ) { }


  ngOnInit(){
    const userRoles=this.userService.decodedtoken.role as Array<string>;
    if(!userRoles){
      this.viewContainerRef.clear();
    }

    if(this.userService.roleMatches(this.appHasRole)){
      if(!this.isVisible){
        this.isVisible=true;
        this.viewContainerRef.createEmbeddedView(this.templateRef);
      }else{
        this.isVisible=false;
        this.viewContainerRef.clear();
      }
      
    }



  }

}

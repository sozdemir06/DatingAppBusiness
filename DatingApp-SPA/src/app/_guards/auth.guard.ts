import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { SnackbarGlobalErrorService } from '../_services/snackbar-global-error.service';
import { UsersService } from '../_services/users.service';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(
        private alert:SnackbarGlobalErrorService,
        private usersService:UsersService,
        private router:Router

    ){}
    canActivate(route: ActivatedRouteSnapshot,state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        const roles=route.firstChild.data["roles"] as Array<string>;
        if(roles){
            const match=this.usersService.roleMatches(roles);
            if(match){
                return true;
            }else{
                this.router.navigate(["members"]);
                this.alert.message("error","You can't access this area.You must have admin roles.!!");
                return false;
            }
        }
        if(this.usersService.loggedIn()){
            return true;
        }

        this.alert.message("error","You cannot access this area without a registered member");
        this.router.navigate(["/home"]);
        return false;
       
    }
}

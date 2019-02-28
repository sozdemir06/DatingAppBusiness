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
        if(this.usersService.loggedIn()){
            return true;
        }

        this.alert.message("error","You cannot access this area without a registered member");
        this.router.navigate(["/home"]);
        return false;
       
    }
}

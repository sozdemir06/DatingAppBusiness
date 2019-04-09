import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { IUser } from '../_models/IUser';
import { UsersService } from '../_services/users.service';
import { SnackbarGlobalErrorService } from '../_services/snackbar-global-error.service';
import { catchError } from 'rxjs/operators';

@Injectable()
export class AdminPanelResolver implements Resolve<IUser[]> {

    constructor(
        private usersService:UsersService,
        private route:Router,
        private alert:SnackbarGlobalErrorService
    ){}
    resolve(route: ActivatedRouteSnapshot,state: RouterStateSnapshot): Observable<IUser[]> {
        return this.usersService.getUsersWithRoles().pipe(
            catchError(error=>{
                this.alert.message("error",error);
                this.route.navigate(["/home"]);
                return of(null);
            })
        );
    }
}

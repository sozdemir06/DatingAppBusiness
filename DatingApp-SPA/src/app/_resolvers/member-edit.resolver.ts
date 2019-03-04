import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { IUser } from '../_models/IUser';
import { UsersService } from '../_services/users.service';
import { SnackbarGlobalErrorService } from '../_services/snackbar-global-error.service';
import { catchError } from 'rxjs/operators';

@Injectable()
export class MemberEditResolver implements Resolve<IUser> {

    constructor(
        private userService:UsersService,
        private alert:SnackbarGlobalErrorService,
        private router:Router,
    ){}
    resolve(route: ActivatedRouteSnapshot,state: RouterStateSnapshot): Observable<IUser>{
        return this.userService.getUser(this.userService.decodedtoken.nameid).pipe(
            catchError(error=>{
                this.alert.message("error","Problem retrieving data");
                this.router.navigate(["/members"]);
                return of(null);
            })
        )
    }
}

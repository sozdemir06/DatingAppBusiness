import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { IUser } from '../_models/IUser';
import { UsersService } from '../_services/users.service';
import { SnackbarGlobalErrorService } from '../_services/snackbar-global-error.service';
import { catchError } from 'rxjs/operators';

@Injectable()
export class MemberListResolver implements Resolve<IUser[]> {
    pageNumber=1;
    pageSize=5;
    constructor(
        private userService:UsersService,
        private alert:SnackbarGlobalErrorService,
        private router:Router
    ){}
    resolve(route: ActivatedRouteSnapshot,state: RouterStateSnapshot): Observable<IUser[]>{
        return this.userService.getUsers(this.pageNumber,this.pageSize).pipe(
            catchError(error=>{
                this.alert.message("error","An occured while retrieved users list data");
                this.router.navigate(["/home"]);
                return of(null);
            })
        )
    }
}

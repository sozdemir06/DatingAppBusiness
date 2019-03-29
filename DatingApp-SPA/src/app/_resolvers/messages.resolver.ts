import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { IUser } from '../_models/IUser';
import { UsersService } from '../_services/users.service';
import { SnackbarGlobalErrorService } from '../_services/snackbar-global-error.service';
import { catchError } from 'rxjs/operators';
import { IMessage } from '../_models/IMessage';

@Injectable()
export class MessagesResolver implements Resolve<IMessage[]> {
    pageNumber=1;
    pageSize=5;
    messageContainer="Unread";

    constructor(
        private userService:UsersService,
        private alert:SnackbarGlobalErrorService,
        private router:Router
    ){}
    resolve(route: ActivatedRouteSnapshot,state: RouterStateSnapshot): Observable<IMessage[]>{
        return this.userService.getUserMessages(this.userService.decodedtoken.nameid,this.messageContainer,this.pageNumber,this.pageSize).pipe(
            catchError(error=>{
                this.alert.message("error","An occured while retrieved Messages list data");
                this.router.navigate(["/home"]);
                return of(null);
            })
        )
    }
}

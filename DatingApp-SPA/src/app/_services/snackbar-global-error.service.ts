import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material';
import { SnackbarGlobalErrorComponent } from '../snackbar-global-error/snackbar-global-error.component';

@Injectable({
  providedIn: 'root'
})
export class SnackbarGlobalErrorService {
config:MatSnackBarConfig={
  duration:4000,
  horizontalPosition:"right",
  verticalPosition:"bottom"
}

constructor(
  private matSnackbar:MatSnackBar,
) { }

message(typeofMessage:string,message:string){

this.matSnackbar.openFromComponent(SnackbarGlobalErrorComponent,{
  data:{
    typeofMessage:typeofMessage,
    message:message
  },
  ...this.config
});
}

}
 

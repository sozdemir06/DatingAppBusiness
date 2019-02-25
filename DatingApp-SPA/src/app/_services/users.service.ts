import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class UsersService {
apiUrl=environment.apiUrl;

constructor(private http:HttpClient) { }


login(model:any){
  return this.http.post(this.apiUrl+"users/login",model)
      .pipe(
        map((response:any)=>{
            const user=response;
            if(user)
            {
              localStorage.setItem("token",user.token);
            } 
        })
      );
  }

register(model:any){
  return this.http.post(this.apiUrl+"users/register",model);
}


}

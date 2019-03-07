import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from "rxjs/operators";
import { JwtHelperService } from "@auth0/angular-jwt";
import { IUser } from '../_models/IUser';
import { Observable, BehaviorSubject } from 'rxjs';
import { IUserWithToken } from '../_models/IUserWithToken';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
apiUrl=environment.apiUrl;
jwtHelper=new JwtHelperService();
decodedtoken:any;
currentUser:IUserWithToken;
currentPhoto=new BehaviorSubject<string>("../../assets/user.png");
currentPhotoUrl=this.currentPhoto.asObservable();

constructor(private http:HttpClient) { }

changeMemberPhoto(url:string){
  this.currentPhoto.next(url);
}

getUsers():Observable<IUser[]>{
  return this.http.get<IUser[]>(this.apiUrl+"users");
}

getUser(id:number):Observable<IUser>{
  return this.http.get<IUser>(this.apiUrl+"users/"+id);
}

updateUser(id:number,user:IUser){
  return this.http.put(this.apiUrl+"users/"+id,user);
}

setMainPhoto(userId:number,photoId:number){
  return this.http.post(this.apiUrl+"users/"+userId+"/photos/"+photoId+"/setMain",{});
}

login(model:any){
  return this.http.post(this.apiUrl+"users/login",model)
      .pipe(
        map((response:any)=>{
            const user=response;
            if(user)
            {
              localStorage.setItem("token",user.token);
              localStorage.setItem("user",JSON.stringify(user));
              this.decodedtoken=this.jwtHelper.decodeToken(user.token);
              this.currentUser=user;
              this.changeMemberPhoto(this.currentUser.photoUrl);
            } 
        })
      );
  }

register(model:any){
  return this.http.post(this.apiUrl+"users/register",model);
}

loggedIn():boolean{
  const token=localStorage.getItem("token");
  return !this.jwtHelper.isTokenExpired(token);
}


}

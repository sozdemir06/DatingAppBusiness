import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from "rxjs/operators";
import { JwtHelperService } from "@auth0/angular-jwt";
import { IUser } from '../_models/IUser';
import { Observable, BehaviorSubject } from 'rxjs';
import { IUserWithToken } from '../_models/IUserWithToken';
import { PaginatedResult } from '../_models/IPagination';

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

getUsers(page?:any,itemPerPage?:any,userParams?:any):Observable<PaginatedResult<IUser[]>>{
  const paginatedResult:PaginatedResult<IUser[]>=new PaginatedResult<IUser[]>();
  let params:any;

  if(page!=null && itemPerPage!=null){
      params=new HttpParams()
            .set("pageNumber",page)
            .set("pageSize",itemPerPage);
  }
 
  if(userParams!=null){
    params=new HttpParams()
           .set("minAge",userParams.minAge)
           .set("maxAge",userParams.maxAge)
           .set("gender",userParams.gender); 
  }
  
  return this.http.get<IUser[]>(this.apiUrl+"users",{observe:'response',params})
                  .pipe(
                    map(response=>{
                      paginatedResult.result=response.body;
                      if(response.headers.get("Pagination")!=null){
                        paginatedResult.pagination=JSON.parse(response.headers.get("Pagination"));
                      }
                      return paginatedResult;
                    })
                  );
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

deletePhoto(userId:number,photoId:number){
  return this.http.delete(this.apiUrl+"users/"+userId+"/photos/"+photoId);
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

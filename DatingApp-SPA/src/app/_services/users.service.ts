import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from "rxjs/operators";
import { JwtHelperService } from "@auth0/angular-jwt";
import { IUser } from '../_models/IUser';
import { Observable, BehaviorSubject } from 'rxjs';
import { IUserWithToken } from '../_models/IUserWithToken';
import { PaginatedResult } from '../_models/IPagination';
import { IMessage } from '../_models/IMessage';

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

getUsers(page?:any,itemPerPage?:any,userParams?:any,likesParams?:any):Observable<PaginatedResult<IUser[]>>{
  const paginatedResult:PaginatedResult<IUser[]>=new PaginatedResult<IUser[]>();
  let params:HttpParams=new HttpParams()
             .set("likesParams",'Likers'); 


  if(page!=null && itemPerPage!=null){
      params=params.append("pageNumber",page);
      params=params.append("pageSize",itemPerPage);        
  }
 
  if(userParams!=null){
    params=params.append("minAge",userParams.minAge);
    params=params.append("maxAge",userParams.maxAge);
    params=params.append("gender",userParams.gender); 
           
          
  }

  if(likesParams==='Likees'){
    params=params.append("likees",'true');
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

getUserMessages(id:number,messageParams?:any,page?:any,itemPerPage?:any,){
  const paginatedResult:PaginatedResult<IMessage[]>=new PaginatedResult<IMessage[]>();
  
  let params:HttpParams=new HttpParams()
            .set('messageContainer',messageParams);
            
      if(page!=null && itemPerPage!=null){
          params=params.append("pageNumber",page)
          params=params.append("pageSize",itemPerPage);
      }
    return this.http.get<IMessage[]>(this.apiUrl+"users/"+id+"/messages",{observe:'response',params})
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

createMessage(userId:number,message:IMessage){
  return this.http.post(this.apiUrl+"users/"+userId+"/messages",message);
}

deleteMessage(messageId:number,userId:number){
  return this.http.delete(this.apiUrl+"users/"+userId+"/messages/"+messageId);
}

getMessagesThread(id:number,recipientId:number){
  return this.http.get<IMessage[]>(this.apiUrl+"users/"+id+"/messages/thread/"+recipientId);
}

getUserLikers(pageNumber?:any,itemsPerPage?:any,likeParams?:any){
  const paginatedResult:PaginatedResult<IUser[]>=new PaginatedResult<IUser[]>();
  let params:any;

  if(likeParams==='Likers'){
    params=new HttpParams()
           .set("likers","true");
  }
  if(likeParams==='Likees'){
    params=new HttpParams()
           .set("likees","true");
  }

  return this.http.get<IUser[]>(this.apiUrl+"users/getlikers/",{observe:'response',params})
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

likeUser(userId:number,recipientId:number){
  return this.http.post(this.apiUrl+"users/"+userId+"/like/"+recipientId,{});
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

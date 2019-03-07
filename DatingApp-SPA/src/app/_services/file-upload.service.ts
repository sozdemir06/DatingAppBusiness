import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpEventType } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { IPhoto } from '../_models/IPhoto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
apiUrl=environment.apiUrl;

constructor(
 private http:HttpClient,
) { }

uploadPhoto(id:number,formData){
  return this.http.post<any>(this.apiUrl+"users/"+id+"/photos",formData,{
    reportProgress:true,
    observe:'events'
  });
}

getEventMessage(event:HttpEvent<any>,formData){
  switch (event.type) {
    case HttpEventType.UploadProgress:
      return this.fileUploadProgress(event);
      break;
    case HttpEventType.Response:
      return this.apiResponse(event);
      break;
      default:
      return `File "${formData.get('File').name}" surprising upload event: ${event.type}.`;
      break;
  }
}

fileUploadProgress(event){
  const percentDone = Math.round(100 * event.loaded / event.total);
  return { status: 'progress', message: percentDone };
}

apiResponse(event){
  return event.body;
}

}

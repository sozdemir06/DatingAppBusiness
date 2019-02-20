import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IValue } from '../_models/IValue';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ValueService {

  apiUrl=environment.apiUrl;

constructor(
  private http:HttpClient
) { }


getValues():Observable<IValue[]>{
  return this.http.get<IValue[]>(this.apiUrl+"values");
}

}

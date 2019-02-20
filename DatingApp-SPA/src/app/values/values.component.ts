import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IValue } from '../_models/IValue';
import { environment } from 'src/environments/environment';
import { ValueService } from '../_services/value.service';

@Component({
  selector: 'app-values',
  templateUrl: './values.component.html',
  styleUrls: ['./values.component.css']
})
export class ValuesComponent implements OnInit {

  values:IValue[];
  apiUrl=environment.apiUrl;

  constructor(
    private valueService:ValueService
  ) { }

  ngOnInit() {
    this.valueService.getValues().subscribe(data=>{
      this.values=data;
    },error=>{
      console.log(error);
    })
  }

}

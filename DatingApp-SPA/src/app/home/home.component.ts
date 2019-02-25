import { Component, OnInit } from '@angular/core';
import { ValueService } from '../_services/value.service';
import { IValue } from '../_models/IValue';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
registerMod:boolean=false;
values:IValue[];

  constructor(
    private valuesService:ValueService
  ) { }

  ngOnInit() {
    
  }

  registerToggle(){
    this.registerMod=true;
  }

  changeRegisterMod(event){
    this.registerMod=event;
  }

}

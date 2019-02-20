import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ValuesComponent } from './values/values.component';

const routes: Routes = [
  {path:"",component:ValuesComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

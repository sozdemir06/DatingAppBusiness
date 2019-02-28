import { Component, OnInit, Inject } from '@angular/core';
import { MAT_SNACK_BAR_DATA } from '@angular/material';

@Component({
  selector: 'app-snackbar-global-error',
  templateUrl: './snackbar-global-error.component.html',
  styleUrls: ['./snackbar-global-error.component.css']
})
export class SnackbarGlobalErrorComponent implements OnInit {

  constructor(
    @Inject(MAT_SNACK_BAR_DATA) public data: any
  ) { }

  ngOnInit() {
    
  }

}

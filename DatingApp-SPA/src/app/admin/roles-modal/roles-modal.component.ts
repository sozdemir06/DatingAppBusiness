import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-roles-modal',
  templateUrl: './roles-modal.component.html',
  styleUrls: ['./roles-modal.component.css']
})
export class RolesModalComponent implements OnInit {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    private matDialogRef:MatDialogRef<RolesModalComponent>
  ) { }

  ngOnInit() {

  }

  updateRoels(){
    this.matDialogRef.close(this.data.roles);
  }

  closeDialog(){
    this.matDialogRef.close();
  }

}

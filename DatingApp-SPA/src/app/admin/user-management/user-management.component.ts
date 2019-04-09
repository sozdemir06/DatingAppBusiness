import { Component, OnInit } from '@angular/core';
import { UsersService } from 'src/app/_services/users.service';
import { SnackbarGlobalErrorService } from 'src/app/_services/snackbar-global-error.service';
import { ActivatedRoute } from '@angular/router';
import { IUser } from 'src/app/_models/IUser';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { RolesModalComponent } from '../roles-modal/roles-modal.component';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
users:IUser[];
dataSource=new MatTableDataSource<IUser>();
displayedColumns: string[] = ['No', 'UserName', 'UserRoles', 'Action'];
  constructor(
    private userService:UsersService,
    private alert:SnackbarGlobalErrorService,
    private router:ActivatedRoute,
    private matDialog:MatDialog

  ) { }

  ngOnInit() {
    this.router.data.subscribe(data=>{
      this.dataSource.data=data["users"];
      this.users=data["users"];
    })
  }

  editUserRoles(element:IUser){
    const dialogRef=this.matDialog.open(RolesModalComponent,{
      width:"400px",
      data:{
        user:element,
        roles:this.getRolesArray(element)
      }
    });

    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        const rolesToUpdate={
          rolesNames:[...result.filter(el=>el.checked===true).map(el=>el.roleName)]
        }
        const roleId:number[]=[...result.filter(el=>el.checked===true).map(el=>el.id)];
        const model:any={
          userId:element.id,
          roleId:roleId
        }

        this.userService.editRoles(model).subscribe(data=>{
          this.alert.message("success","Successfuly updated user roles.!!");
          element.userRoles=[...rolesToUpdate.rolesNames];
        },error=>{
          this.alert.message("error",error);
        })
      }
    })
  }

  private  getRolesArray(user:IUser){
    const roles=[];
    const userrole=user.userRoles;

    const availableRoles:any[]=[
      {id:1,roleName:"Member"},
      {id:2,roleName:"Admin"},
      {id:3,roleName:"Moderator"},
      {id:4,roleName:"VIP"}
    ];

    for (let i = 0; i< availableRoles.length; i++) {
      let isMatch=false;
     
      for (let j = 0; j < userrole.length; j++) {
        if(availableRoles[i].id===userrole[j].id){
          isMatch=true;
          availableRoles[i].checked=true;
          roles.push(availableRoles[i]);
          break;
        }
        
      }
      if(!isMatch){
        availableRoles[i].checked=false;
        roles.push(availableRoles[i]);
      }
      
    }
    return roles;
  }



}

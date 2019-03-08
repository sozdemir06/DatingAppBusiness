import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IPhoto } from 'src/app/_models/IPhoto';
import { FormGroup, FormBuilder } from '@angular/forms';
import { UsersService } from 'src/app/_services/users.service';
import { FileUploadService } from 'src/app/_services/file-upload.service';
import { SnackbarGlobalErrorService } from 'src/app/_services/snackbar-global-error.service';
import { HttpEventType } from '@angular/common/http';
import { enableDebugTools } from '@angular/platform-browser';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
@Input() photos:IPhoto[];
@Output() updatePhotoUrl=new EventEmitter<string>();

selectedImageUrl:string;
profileForm:FormGroup;
uploadProggress:number=0;
status:string;
currentMain:IPhoto;

  constructor(
    private fb:FormBuilder,
    private userService:UsersService,
    private fileUploadService:FileUploadService,
    private alert:SnackbarGlobalErrorService
  ) { }

  ngOnInit() {
    this.profileForm=this.fb.group({
      description:[""],
      File:[""],
    })
  
  }

  onSelectedFile($event){
    if($event.target.files.length>0){
      const file=<File>$event.target.files[0];
      this.profileForm.get("File").setValue(file);

      var filereader=new FileReader();
      filereader.onload=(e:any)=>{
          this.selectedImageUrl=e.target.result;  
      }
      filereader.readAsDataURL($event.target.files[0]);

    }
  }

  profileFormSubmit(){
    if(this.profileForm.get("File").value){
      const formData=new FormData();
      formData.append("File",this.profileForm.get("File").value);
      formData.append("description",this.profileForm.get("description").value);

      this.fileUploadService.uploadPhoto(this.userService.decodedtoken.nameid,formData).subscribe(event=>{
         if(event.type===HttpEventType.UploadProgress){
           this.uploadProggress=Math.round(event.loaded/event.total*100);
           this.status="progress";
         }else if(event.type===HttpEventType.Response){
           const photo={
            id:event.body["id"],
            url:event.body["url"],
            isMain:event.body["isMain"],
            description:event.body["description"],
            dateAdded:event.body["dateAdded"],
            publicId:event.body["publicId"],
           };
           this.photos.push(photo);
           if(photo.isMain){
             this.userService.changeMemberPhoto(photo.url);
             this.userService.currentUser.photoUrl=photo.url;
             localStorage.setItem("user",JSON.stringify(this.userService.currentUser));
           }
           this.status="ok";
           this.selectedImageUrl="";
           this.alert.message("success","Photo successfuly uploaded.!");
         }
      },error=>{
        this.alert.message("error",error);
      })
    }
  }

  setMainPhoto(photo:IPhoto){
    this.userService.setMainPhoto(this.userService.decodedtoken.nameid,photo.id).subscribe(data=>{
      this.currentMain=this.photos.filter(p=>p.isMain=== true)[0];
      this.currentMain.isMain=false;
      photo.isMain=true;
      this.userService.changeMemberPhoto(photo.url);
      this.userService.currentUser.photoUrl=photo.url;
      localStorage.setItem("user",JSON.stringify(this.userService.currentUser));
      this.alert.message("success","Photo successfuly set as main photo");
    },error=>{
      this.alert.message("error",error);
    })
  }

  cancelSelectedImage(){
    this.selectedImageUrl="";
    this.profileForm.reset();
  }

  deletePhoto(photo:IPhoto){
    const dialogConfirm=confirm("Are you sure delete this photo.!!!");
    if(dialogConfirm==true){
        this.userService.deletePhoto(this.userService.decodedtoken.nameid,photo.id).subscribe(data=>{
        this.photos.splice(this.photos.findIndex(i=>i.id==photo.id),1);
        this.alert.message("success","Photo deleted successfuly.!");
      },error=>{
        this.alert.message("error",error);
      });
    }
    
  }

}

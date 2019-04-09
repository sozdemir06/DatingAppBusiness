import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from "@angular/flex-layout";
import { NgxGalleryModule } from "ngx-gallery";
import { MomentModule } from "ngx-moment";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';
import { ValuesComponent } from './values/values.component';
import { HttpClientModule } from '@angular/common/http';
import { NavComponent } from './nav/nav.component';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { UsersService } from './_services/users.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvide } from './_interceptions/ErrorInterceptor';
import { SnackbarGlobalErrorComponent } from './snackbar-global-error/snackbar-global-error.component';
import { SnackbarGlobalErrorService } from './_services/snackbar-global-error.service';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { JwtModule } from '@auth0/angular-jwt';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { FileUploadService } from './_services/file-upload.service';
import { ListsResolver } from './_resolvers/list.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { MemberMessagesComponent } from './members/member-messages/member-messages.component';
import { HasRoleDirective } from './_directives/hasRole.directive';
import { AdminPanelResolver } from './_resolvers/admin-panel.resolver';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { RolesModalComponent } from './admin/roles-modal/roles-modal.component';


export function tokenGetter(){
   return localStorage.getItem("token");
};

@NgModule({
   declarations: [
      AppComponent,
      ValuesComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      SnackbarGlobalErrorComponent,
      MemberListComponent,
      MessagesComponent,
      ListsComponent,
      MemberCardComponent,
      MemberDetailComponent,
      MemberEditComponent,
      PhotoEditorComponent,
      MemberMessagesComponent,
      HasRoleDirective,
      AdminPanelComponent,
      UserManagementComponent,
      RolesModalComponent
     
   ],
   imports: [
      FormsModule,
      BrowserModule,
      ReactiveFormsModule,
      AppRoutingModule,
      MomentModule,
      BrowserAnimationsModule,
      HttpClientModule,
      MaterialModule,
      FlexLayoutModule,
      NgxGalleryModule,
      JwtModule.forRoot({
         config:{
             tokenGetter:tokenGetter,
             whitelistedDomains:["localhost:5000"],
             blacklistedRoutes:[
                "localhost:5000/api/users/login",
                "localhost:5000/api/users/register"
               ]
         }
        })
   ],
   providers: [
      UsersService,
      SnackbarGlobalErrorService,
      ErrorInterceptorProvide,
      AuthGuard,
      MemberDetailResolver,
      MemberListResolver,
      MemberEditResolver,
      PreventUnsavedChanges,
      FileUploadService,
      ListsResolver,
      MessagesResolver,
      AdminPanelResolver
   ],
   entryComponents: [
      SnackbarGlobalErrorComponent,
      RolesModalComponent
     
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }

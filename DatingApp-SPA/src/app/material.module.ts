import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatListModule,MatToolbarModule,
          MatButtonModule,
          MatFormFieldModule,
          MatInputModule,MatMenuModule,MatIconModule,MatCardModule,MatSnackBarModule,
          MatTabsModule } from "@angular/material";

@NgModule({
    declarations: [],
    imports: [ 
        CommonModule,
        MatListModule,
        MatToolbarModule,
        MatButtonModule,
        MatFormFieldModule,
        MatInputModule ,
        MatMenuModule,
        MatIconModule,
        MatCardModule,
        MatSnackBarModule,
        MatTabsModule
    ],
    exports: [
        MatListModule,
        MatToolbarModule,
        MatButtonModule,
        MatFormFieldModule,
        MatInputModule,
        MatMenuModule,
        MatIconModule,
        MatCardModule,
        MatSnackBarModule,
        MatTabsModule
    ],
    providers: [],
})
export class MaterialModule {}
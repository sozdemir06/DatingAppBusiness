import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatListModule,MatToolbarModule,
          MatButtonModule,
          MatFormFieldModule,
          MatInputModule,MatMenuModule,MatIconModule,MatCardModule,MatSnackBarModule,
          MatTabsModule,MatProgressBarModule } from "@angular/material";

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
        MatTabsModule,
        MatProgressBarModule
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
        MatTabsModule,
        MatProgressBarModule
    ],
    providers: [],
})
export class MaterialModule {}
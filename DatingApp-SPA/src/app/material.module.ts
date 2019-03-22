import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatListModule,MatToolbarModule,
          MatButtonModule,
          MatFormFieldModule,
          MatInputModule,MatMenuModule,MatIconModule,MatCardModule,MatSnackBarModule,
          MatTabsModule,MatProgressBarModule,MatRadioModule,MatDatepickerModule, MatNativeDateModule,
          MatPaginatorModule,MatSelectModule,MatButtonToggleModule  } from "@angular/material";

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
        MatProgressBarModule,
        MatRadioModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatPaginatorModule,
        MatSelectModule,
        MatButtonToggleModule
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
        MatProgressBarModule,
        MatRadioModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatPaginatorModule,
        MatSelectModule,
        MatButtonToggleModule
    ],
    providers: [],
})
export class MaterialModule {}
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatListModule,MatToolbarModule,
          MatButtonModule,
          MatFormFieldModule,
          MatInputModule,MatMenuModule,MatIconModule,MatCardModule } from "@angular/material";

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
        MatCardModule
    ],
    exports: [
        MatListModule,
        MatToolbarModule,
        MatButtonModule,
        MatFormFieldModule,
        MatInputModule,
        MatMenuModule,
        MatIconModule,
        MatCardModule
    ],
    providers: [],
})
export class MaterialModule {}
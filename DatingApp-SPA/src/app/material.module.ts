import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatListModule } from "@angular/material";

@NgModule({
    declarations: [],
    imports: [ 
        CommonModule,
        MatListModule 
    ],
    exports: [
        MatListModule
    ],
    providers: [],
})
export class MaterialModule {}
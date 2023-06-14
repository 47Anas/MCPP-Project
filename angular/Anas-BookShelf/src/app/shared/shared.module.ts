import { NgModule } from "@angular/core";
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
    declarations: [],
    imports: [],
    exports: [
        MatButtonModule,
        MatTableModule,
        MatSnackBarModule,
        MatDialogModule
    ]
})

export class SharedModule { }

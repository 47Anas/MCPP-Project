import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { MatMomentDateModule } from '@angular/material-moment-adapter';

@NgModule({
    declarations: [],
    imports: [],
    exports: [
        MatButtonModule,
        MatTableModule,
        MatFormFieldModule,
        MatInputModule,
        MatDialogModule,
        MatDatepickerModule,
        MatMomentDateModule,
        MatSelectModule,
        MatSnackBarModule
    ]
})

export class SharedModule { }

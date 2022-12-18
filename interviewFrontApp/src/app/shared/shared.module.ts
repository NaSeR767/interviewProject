import { NgModule } from "@angular/core";
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { ModalModule } from '../shared/modules/modal/modal.module';
import { MatFormFieldModule, } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule } from '@angular/material/paginator';

@NgModule({
    declarations: [

    ],
    imports: [
        MatCardModule,
        MatTableModule,
        MatButtonModule,
        ModalModule,
        MatInputModule,
        MatFormFieldModule,
        FormsModule,
        ReactiveFormsModule,
        MatIconModule,
        MatPaginatorModule
    ],
    exports: [
        MatCardModule,
        MatTableModule,
        MatButtonModule,
        ModalModule,
        MatInputModule,
        MatFormFieldModule,
        FormsModule,
        ReactiveFormsModule,
        MatIconModule,
        MatPaginatorModule
    ]
})
export class SharedModule { }
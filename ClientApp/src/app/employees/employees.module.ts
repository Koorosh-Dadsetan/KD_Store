import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { EmployeesRoutingModule } from './employees-routing.module';
import { ListComponent } from './list/list.component';
import { CreateComponent } from './create/create.component';
import { DeleteComponent } from './delete/delete.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';

@NgModule({
  declarations: [
    ListComponent,
    CreateComponent,
    DeleteComponent,
    DetailsComponent,
    EditComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    EmployeesRoutingModule
  ],
  exports: []
})
export class EmployeesModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PropertyViewerRoutingModule } from './property-viewer-routing.module';
import { PropertyViewerComponent } from './property-viewer.component';
import { DropdownModule } from '../common/module/dropdown/dropdown.module';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { DragulaModule } from 'ng2-dragula';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [PropertyViewerComponent],
  imports: [
    CommonModule,
    FormsModule,
    PropertyViewerRoutingModule,
    DropdownModule,
    HttpClientModule,
    ModalModule.forRoot(),
    DragulaModule.forRoot()
  ]
})
export class PropertyViewerModule { }

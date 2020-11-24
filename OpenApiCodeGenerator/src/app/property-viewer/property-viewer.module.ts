import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PropertyViewerRoutingModule } from './property-viewer-routing.module';
import { PropertyViewerComponent } from './property-viewer.component';
import { DropdownModule } from '../common/module/dropdown/dropdown.module';


@NgModule({
  declarations: [PropertyViewerComponent],
  imports: [
    CommonModule,
    PropertyViewerRoutingModule,
    DropdownModule
  ]
})
export class PropertyViewerModule { }

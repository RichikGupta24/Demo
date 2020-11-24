import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PropertyViewerComponent } from './property-viewer.component';


const routes: Routes = [
  {
    path: '',
    component: PropertyViewerComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PropertyViewerRoutingModule { }

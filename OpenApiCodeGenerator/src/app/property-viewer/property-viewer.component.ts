import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ContainerService } from '../service/container.service';

@Component({
  selector: 'app-property-viewer',
  templateUrl: './property-viewer.component.html',
  styleUrls: ['./property-viewer.component.scss']
})
export class PropertyViewerComponent implements OnInit {

  propertyList: any;
  selectedObj: any;
  dropdownInputFieldList: Array<string> = [
    "Text",
    "Password",
    "Email",
    "Text Area",
    "Dropdown",
    "Radio",
    "Range",
    "Button",
    "submit",
    "Reset",
    "Checkbox",
    "Color",
    "Calendar",
    "File",
    "Hidde",
    "Image",
    "Number",
    "Search",
    "Telephone"
  ];

  constructor(private containerService: ContainerService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.populatePropertyDetails();
  }

  populatePropertyDetails() {
    this.containerService.getApiProperties({ "apiId": this.activatedRoute.snapshot.paramMap.get('apiId') }).subscribe(
      resp => {
        this.propertyList = resp['propertyList'];
        console.log(this.propertyList);
      }, err => {
        console.log(err);
      });
  }

  selectedDropdownValue(event: any) {
    this.propertyList.map(item => {
      if (item.id === event.selectedObj.id) item.selectedProp = event.selectedVal;
    });
    console.log(this.propertyList);
  }

  selectedObjDefaultValue(defaultValList: any) {
    this.selectedObj = defaultValList;
  }

  selectedDefaultSuggestionValue(event: any) {

  }

  save() {

  }

  reset() {
    this.populatePropertyDetails();
  }

  navigateToDashboard() {
    this.router.navigateByUrl('');
  }

}

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
  selectedTemplateAction: string;
  templateActionList: Array<string> = ['Create', 'Update', 'Delete'];
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
    this.selectedTemplateAction = this.templateActionList[0];
    this.containerService.getApiProperties({ "apiId": this.activatedRoute.snapshot.paramMap.get('apiId') }).subscribe(
      resp => {
        this.propertyList = resp['propertyList'];
        console.log(this.propertyList);
      }, err => {
        console.log(err);
      });
  }

  selectedDropdownValue(event) {
    console.log();
    if (typeof event === 'object') {
      this.propertyList.map(item => {
        if (item.id === event.selectedObj.id) item.selectedProp = event.selectedVal;
      });
    }
    if (typeof event === 'string') {
      this.selectedTemplateAction = event;
    }
  }

  selectedObjDefaultValue(defaultValList: any) {
    this.selectedObj = defaultValList;
  }

  selectedDefaultSuggestionValue(event: any) {

  }

  generate() {

  }

  reset() {
    this.populatePropertyDetails();
  }

  saveDefaultValue() {

  }

  navigateToDashboard() {
    this.router.navigateByUrl('');
  }

}

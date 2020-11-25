import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'cm-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.scss']
})
export class DropdownComponent implements OnInit {

  @Input() selected: any;
  @Input() items: any;
  @Output() getProperty: EventEmitter<any> = new EventEmitter<any>();

  selectedInputType: string;

  constructor() { }

  ngOnInit(): void {
    this.getTypeOfSeletecInput();
  }

  getTypeOfSeletecInput() {
    this.selectedInputType = typeof this.selected;
    console.log(this.selectedInputType);
  }

  selectedProperty(item: string, obj?: any) {
    if (this.selectedInputType === 'object') this.getProperty.emit({ 'selectedVal': item, 'selectedObj': obj });
    if (this.selectedInputType === 'string') this.getProperty.emit(item);
  }

}

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

  constructor() { }

  ngOnInit(): void {
  }

  selectedProperty(property: string, obj: any) {
    this.getProperty.emit({ 'selectedVal': property, 'selectedObj': obj });
  }

}

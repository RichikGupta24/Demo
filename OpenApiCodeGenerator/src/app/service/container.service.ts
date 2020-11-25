import { Injectable } from '@angular/core';
import { WebAPI } from '../common/api/webApi';
import { DataService } from './http-data.service';

@Injectable({
  providedIn: 'root'
})
export class ContainerService {

  constructor(private ds: DataService) { }

  getApiProperties(request: any) {
    return this.ds.getData(WebAPI.GET_API_PROPERTY, request);
  }
 
}

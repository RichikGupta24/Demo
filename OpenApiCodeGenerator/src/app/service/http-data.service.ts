import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  isMock: boolean = environment.isMockService;
  headers = new HttpHeaders();

  constructor(private httpClient: HttpClient) { }

  getData(serviceApi, params?: any, headers?: any): Observable<any> {
    let apiData: Observable<any> = null;
    if (this.isMock || serviceApi.isMockApi) {
      apiData = this.getDataMockUp(serviceApi.mockApi);
    } else {
      apiData = this.getDataService(serviceApi.api, params, headers)
        .pipe(
          catchError((error) => {
            return throwError(error);
          })
        )
    }
    return apiData;
  }

  getDataMockUp(serviceApi: string) {
    return this.httpClient.get(serviceApi).pipe(
      catchError((error) => {
        this.errorHandler(error);
        throw (error);
      })
    );
  }

  getDataService(serviceApi: string, params?: any, headers?: any): Observable<any> {
    if (!!params) {
      let queryString = '?' + this.objectToParams(params);
      serviceApi += queryString;
    }
    return this.httpClient.get(serviceApi, {}).pipe(
      map((response) => {
        return response;
      }),
      catchError((error) => {
        this.errorHandler(error);
        throw (error);
      })
    );
  }

  postData(serviceApi, params?: any, headers?: any): Observable<any> {
    let apiData: Observable<any> = null;
    if (this.isMock) {
      apiData = this.postDataMockUp(serviceApi.mockApi);
    } else {
      apiData = this.postDataService(serviceApi.api, params, headers)
    }
    return apiData;
  }

  postDataMockUp(serviceApi: string) {
    return this.httpClient.get(serviceApi).pipe(
      catchError((error) => {
        this.errorHandler(error);
        throw (error);
      })
    );
  }

  postDataService(serviceApi: string, params?: any, headers?: any): Observable<any> {
    let body = null;
    body = JSON.stringify(params);
    return this.httpClient.post(serviceApi, params, {}).pipe(
      map((response) => {
        return response;
      }),
      catchError((error) => {
        this.errorHandler(error);
        throw (error);
      })
    );
  }

  private errorHandler(error): any {
    const errMsg = (error.message) ? error.message :
      error.status ? `${error.status} - ${error.statusText}` : 'Server Error: Service Unavailable';
  } 

  objectToParams(object): string {
    return Object.keys(object).map((key) => this.isJsObject(object[key]) ?
      this.subObjectToParams(encodeURIComponent(key), object[key]) :
      `${encodeURIComponent(key)}=${encodeURIComponent(object[key])}`).join('&');
  }

  subObjectToParams(key, object): string {
    return Object.keys(object).map((childKey) => this.isJsObject(object[childKey]) ?
      this.subObjectToParams(`${key}[${encodeURIComponent(childKey)}]`, object[childKey]) :
      `${key}[${encodeURIComponent(childKey)}]=${encodeURIComponent(object[childKey])}`).join('&');
  }

  isJsObject(o: any): boolean {
    return o !== null && (typeof 0 === 'function' || typeof 0 === 'object');
  }
}
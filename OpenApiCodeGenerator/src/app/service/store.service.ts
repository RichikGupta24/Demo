import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject, ReplaySubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class StoreService {
    apiDetail: BehaviorSubject<Object> = new BehaviorSubject<Object>(null);

    constructor() { }

    getApiDetail() {
        return this.apiDetail;
    }

    setApiDetail(apiDetail: Object) {
        this.apiDetail.next({ ...apiDetail });
    }

}

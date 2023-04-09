import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RelationService {
  test: Subject<boolean> = new Subject<boolean>();
}

//How to use this service?

//Answer:

//For set data:
//this.relationService.test.next(true or false);

//For get data:
//testStatus!: boolean;
//this.relationService.test.subscribe((value) => {
//  this.testStatus = value;
//});

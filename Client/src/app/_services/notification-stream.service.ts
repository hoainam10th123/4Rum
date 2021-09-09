import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationStreamService {
  private count: number = 0;

  private countSource = new ReplaySubject<number>(1);
  count$ = this.countSource.asObservable();

  constructor() { }

  set Count(value: number) {
    this.countSource.next(value);
    this.count = value;
  }

  get Count(){
    return this.count;
  }
}

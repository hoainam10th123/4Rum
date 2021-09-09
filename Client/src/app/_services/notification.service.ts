import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Notification } from '../_models/notification';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUnread(){
    return this.http.get<Notification[]>(this.baseUrl+'Notification');
  }

  update(){
    return this.http.put(this.baseUrl+'Notification', {});
  }

  getAll(pageNumber, pageSize){
    let params = getPaginationHeaders(pageNumber, pageSize);
    return getPaginatedResult<Notification[]>(this.baseUrl+'Notification/get-pagination', params, this.http);
  }
}

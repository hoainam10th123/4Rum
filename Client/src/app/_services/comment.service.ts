import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CommentChildren, CommentParent } from '../_models/CommentParent';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

  addCommentParent(model){
    return this.http.post(this.baseUrl+'Comment/add-parent', model);
  }

  addCommentChildrent(model){
    return this.http.post(this.baseUrl+'Comment/add-childrent', model);
  }

  deleteParent(id){
    return this.http.delete(this.baseUrl+'Comment/delete-parent/'+id);
  }

  deleteChildrent(id){
    return this.http.delete(this.baseUrl+'Comment/delete-childrent/'+id);
  }

  getCommentParent(id){
    return this.http.get<CommentParent>(this.baseUrl+'Comment/get-parent/'+id);
  }

  getCommentChildrent(id){
    return this.http.get<CommentChildren>(this.baseUrl+'Comment/get-childrent/'+id);
  }

  updateParent(model){
    return this.http.put(this.baseUrl+'Comment/update-parent', model);
  }

  updateChildrent(model){
    return this.http.put(this.baseUrl+'Comment/update-childrent', model);
  }
}

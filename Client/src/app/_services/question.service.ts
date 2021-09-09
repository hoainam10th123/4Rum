import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Question, QuestionAtHome, SearchQuestion } from '../_models/question';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

  addQuestion(model: any){
    return this.http.post(this.baseUrl+'Question/add', model);
  }

  detail(id){
    return this.http.get<Question>(this.baseUrl+'Question/'+id);
  }

  deleteQuestion(id){
    return this.http.delete(this.baseUrl+'Question/'+id);
  }

  getQuestions(pageNumber, pageSize){
    let params = getPaginationHeaders(pageNumber, pageSize);
    return getPaginatedResult<QuestionAtHome[]>(this.baseUrl+'Question', params, this.http);
  }

  search(name:string){
    return this.http.get<QuestionAtHome[]>(this.baseUrl+'Question/search?name='+name);
  }

  getAll(){
    return this.http.get<SearchQuestion[]>(this.baseUrl+'Question/get-all');
  }

  updateQuestion(model){
    return this.http.put(this.baseUrl+'Question/update', model);
  }

  getTopTen(){
    return this.http.get<QuestionAtHome[]>(this.baseUrl+'Question/get-top-ten');
  }
}

import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { QuestionAtHome } from '../_models/question';

@Injectable({
  providedIn: 'root'
})
export class StreamQuestionService {

  private questionsSource = new BehaviorSubject<QuestionAtHome[]>([]);
  questions$ = this.questionsSource.asObservable();
  
  constructor() { }

  set Questions(value: any){
    this.questionsSource.next(value);
  }
}

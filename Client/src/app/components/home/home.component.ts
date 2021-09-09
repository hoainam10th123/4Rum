import { Component, OnInit } from '@angular/core';
import { Pagination } from 'src/app/_models/pagination';
import { QuestionAtHome } from 'src/app/_models/question';
import { QuestionService } from 'src/app/_services/question.service';
import { StreamQuestionService } from 'src/app/_services/stream-question.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  questions: QuestionAtHome[];
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination;
  maxSize = 5;

  constructor(private streamQuestion: StreamQuestionService, private questionService: QuestionService) { 
    this.streamQuestion.questions$.subscribe(data=>{
      if(data){
        this.questions = data;
      }
    })
  }

  ngOnInit(): void {
    this.loadQuestions();    
  }

  loadQuestions(){
    this.questionService.getQuestions(this.pageNumber, this.pageSize).subscribe(res=>{
      this.questions = res.result;
      this.pagination = res.pagination;  
    })
  }

  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadQuestions();
  }

}

import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Question } from 'src/app/_models/question';
import { MyStreamService } from 'src/app/_services/my-stream.service';
import { QuestionService } from 'src/app/_services/question.service';

@Component({
  selector: 'app-confirm-question',
  templateUrl: './confirm-question.component.html',
  styleUrls: ['./confirm-question.component.css']
})
export class ConfirmQuestionComponent implements OnInit {

  questionId: string;

  constructor(private toastr: ToastrService, private stream: MyStreamService, public bsModalRef: BsModalRef, private questionService: QuestionService) { }

  ngOnInit(): void {
  }

  confirm(){
    this.questionService.deleteQuestion(this.questionId).subscribe((data: Question)=>{      
      if(data){
        this.stream.delQuestion = this.questionId;
      }else{
        this.toastr.warning("Không có gì để xóa");        
      }
      this.bsModalRef.hide();
    })
  }
}

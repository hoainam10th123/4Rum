import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { QuestionAtHome } from 'src/app/_models/question';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { QuestionService } from 'src/app/_services/question.service';

@Component({
  selector: 'app-site-layout',
  templateUrl: './site-layout.component.html',
  styleUrls: ['./site-layout.component.css']
})
export class SiteLayoutComponent implements OnInit {

  user: User;
  questions: QuestionAtHome[] = [];
  
  constructor(private questionService: QuestionService, private router: Router, private accountService: AccountService, private toastr: ToastrService) {
    this.accountService.currentUser$.subscribe(user=> this.user = user);
  }

  ngOnInit(): void {
    this.questionService.getTopTen().subscribe(data=>{
      this.questions = data;
    })
  }

  askQuestion(){
    if(this.user){
      this.router.navigateByUrl('/ask-question');
    }else{
      this.toastr.info('Vui lòng đăng nhập để soạn câu hỏi!')
    }
  }
}

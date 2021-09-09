import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';
import { Notification } from 'src/app/_models/notification';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MyStreamService } from 'src/app/_services/my-stream.service';
import { NotificationStreamService } from 'src/app/_services/notification-stream.service';
import { NotificationService } from 'src/app/_services/notification.service';
import { QuestionService } from 'src/app/_services/question.service';
import { StreamQuestionService } from 'src/app/_services/stream-question.service';

@Component({
  selector: 'app-site-header',
  templateUrl: './site-header.component.html',
  styleUrls: ['./site-header.component.css']
})
export class SiteHeaderComponent implements OnInit {
  selected: string;
  states: string[] = [];
  count = 0;
  user: User;

  constructor(private notificationService: NotificationService, 
    private streamQuestion: StreamQuestionService, 
    private questionService: QuestionService, 
    private stream: MyStreamService, public accountService: AccountService, 
    private router: Router,
    private notiStream: NotificationStreamService) {
      this.accountService.currentUser$.subscribe(data=>{
        this.user = data;
      })
      this.notiStream.count$.subscribe(data=>{
        this.count = data;
      });      
    }

  ngOnInit(): void {
    this.questionService.getAll().subscribe(data=>{
      data.forEach(models=>{
        this.states.push(models.tittle);
      })
    });

    if(this.user){
      this.notificationService.getUnread().subscribe(data=>{
        this.notiStream.Count = data.length;
      });
    }

  }

  onSelect(event: TypeaheadMatch): void {
    //console.log(event.item);
    this.questionService.search(event.item).subscribe(data=>{
      this.streamQuestion.Questions = data;
    })
  }

  signOut() {
    this.accountService.logout();
    //this.router.navigateByUrl('/');
    if (this.stream.socialUser) {
      this.stream.signOutSocial();
    }
  }

  notifications: Notification[] = [];
  pageNumber = 1;
  pageSize = 10;
  pagination: Pagination;

  loadNotification(){
    this.pageNumber = 1;
    this.notificationService.getAll(this.pageNumber, this.pageSize).subscribe(res=>{
      this.notifications = res.result;
      this.pagination = res.pagination;
    });
    this.notiStream.Count = 0;
    this.notificationService.update().subscribe(()=>{
      console.log("OK");
    });
  }

  onScroll() {
    if(this.pageNumber < this.pagination.totalPages){
      this.pageNumber +=1;
      this.notificationService.getAll(this.pageNumber, this.pageSize).subscribe(res=>{
        this.notifications.push(...res.result);
        this.pagination = res.pagination; 
      });
    }

  }

}

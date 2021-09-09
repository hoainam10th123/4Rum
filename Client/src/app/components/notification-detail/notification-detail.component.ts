import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { AccountService } from 'src/app/_services/account.service';
import { MyStreamService } from 'src/app/_services/my-stream.service';
import { DetailQuestionComponent } from '../detail-question/detail-question.component';

@Component({
  selector: 'app-notification-detail',
  templateUrl: './notification-detail.component.html',
  styleUrls: ['./notification-detail.component.css']
})
export class NotificationDetailComponent extends DetailQuestionComponent implements OnInit, AfterViewInit {

  commentParentId:string;

  constructor(
    public accountService:AccountService, 
    public stream: MyStreamService, 
    public route: ActivatedRoute, 
    public modalService: BsModalService, 
    public router: Router) {
    super(accountService, stream, route, modalService, router);
  }
  
  ngOnInit(): void {
    super.ngOnInit();
    this.route.paramMap.subscribe(result =>
      {       
        this.commentParentId =  result.get('commentid');     
        //console.log(`commentid: ${result.get('commentid')}`);       
      }); 
  }
  
  ngAfterViewInit(): void {
    document.getElementById(this.commentParentId).scrollIntoView({
      behavior: "smooth",
      block: "start",
      inline: "nearest"
    });
    //this.router.navigate([], { fragment: this.commentParentId }); 
  }

}

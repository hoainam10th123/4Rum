import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CommentChildren } from 'src/app/_models/CommentParent';
import { Question } from 'src/app/_models/question';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { CommentService } from 'src/app/_services/comment.service';
import { MyStreamService } from 'src/app/_services/my-stream.service';
import { AsnwerModalComponent } from '../modals/asnwer-modal/asnwer-modal.component';
import { ConfirmQuestionComponent } from '../modals/confirm-question/confirm-question.component';

@Component({
  selector: 'app-detail-question',
  templateUrl: './detail-question.component.html',
  styleUrls: ['./detail-question.component.css']
})
export class DetailQuestionComponent implements OnInit {
  bsModalRef: BsModalRef;
  question: Question;
  userCurrent: User;
  
  constructor(
    public accountService:AccountService, 
    public stream: MyStreamService, 
    public route: ActivatedRoute, 
    public modalService: BsModalService, 
    public router: Router) {
    this.accountService.currentUser$.subscribe(user=>{
      this.userCurrent = user;
    })
  }
//them dong nay ngoai app-routing.module.ts: resolve:{member: QuestionDetailedResolver}
  ngOnInit(): void {
    this.route.data.subscribe(data=>{
      this.question = data.question;
      this.question.commentParents.sort((a, b) => (a.datePosted > b.datePosted) ? 1 : -1);
    });

    this.stream.commentParent$.subscribe(parent=>{
      if(this.question){
        if(parent){
          //binh luan cua cau hoi nay       
          if(this.question.id === parent.questionId){
            let cmt = this.question.commentParents.some(x=>x.id === parent.id);
            if(!cmt)//false = chua co
              this.question.commentParents.push(parent);
          }
        }          
      }
    });

    //Xoa cau hoi thi chuyen ve Home
    this.stream.delQuestion$.subscribe(id=>{
      if(id){
        this.router.navigateByUrl('/');
        this.stream.delQuestion = null;
      }
    });

    this.stream.delComment$.subscribe(data=>{
      if(data){
        //console.log(typeof (id));
        if(typeof (data.id) === 'string'){//xoa comment cha
          this.question.commentParents = this.question.commentParents.filter(x=>x.id !== data.id);
        }else{// number, xoa comment con
          let commentParent = this.question.commentParents.find(x=>x.id === data.parentId);
          if(commentParent)
            commentParent.childrentComments = commentParent.childrentComments.filter(x=>x.id !== data.id);
        }
        this.stream.delComment = null;
      }
    });

    this.stream.updateCommentParent$.subscribe(data=>{
      let commentParent = this.question.commentParents.find(x=>x.id === data.id);
      if(commentParent){
        commentParent.noiDung = data.noiDung;
      }
    })
  }

  //false: thêm mới, true là edit
  openModalWithComponent(id, isEdit: boolean, comment) { 
    const initialState = {
      list: [
        'Open a modal with component',
        'Pass your data',
        'Do something else',
      ],
      questionId: id,
      _isEdit: isEdit,
      comment:comment,
      userCommentTo: this.question.userName
    };   
    this.bsModalRef = this.modalService.show(AsnwerModalComponent, {initialState});
  }

  openConfirmModal(id){
    const initialState = {      
      questionId: id
    };   
    this.bsModalRef = this.modalService.show(ConfirmQuestionComponent, {initialState});
  }  
}

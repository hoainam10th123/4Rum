import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CommentChildren, CommentParent } from 'src/app/_models/CommentParent';
import { Question } from 'src/app/_models/question';
import { AccountService } from 'src/app/_services/account.service';
import { CommentService } from 'src/app/_services/comment.service';

@Component({
  selector: 'app-comment-childrent',
  templateUrl: './comment-childrent.component.html',
  styleUrls: ['./comment-childrent.component.css']
})
export class CommentChildrentComponent implements OnInit {
  @Input() commentParent: CommentParent;
  cmtForm: FormGroup;
  isDisplay = false;
  @Input() question: Question;
  @Input() commentChildrentId: number;
  
  constructor(private commentService: CommentService, public accountService: AccountService) { }

  ngOnInit(): void {
    this.khoiTaoForm();
  }

  content = '';

  khoiTaoForm(){
    this.cmtForm = new FormGroup({
      content: new FormControl(this.content, Validators.required)     
    })
  }

  submitChildComment(){
    if(this.commentChild){//update
      this.commentChild.noiDung = this.content;
      this.commentService.updateChildrent(this.commentChild).subscribe(()=>{
        this.cmtForm.reset();
      })
    }else{//add new CommentChildrent
      let model = {
        noiDung: this.cmtForm.value.content,
        questionId:this.question.id,
        parentId: this.commentParent.id,
        userCommentTo:this.commentParent.userName
      };
      this.commentService.addCommentChildrent(model).subscribe((data: CommentChildren) =>{
        if(data){
          let commentParent = this.question.commentParents.find(x=>x.id === data.parentId);
          if(commentParent){
            commentParent.childrentComments.push(data);
            this.cmtForm.reset();
          }
        }
      })
    }    
  }

  commentChild: CommentChildren;

  editChildCommentMode(event: CommentChildren){
    this.commentChild = event;
    this.content = event.noiDung;
    this.isDisplay = true;
  }

}

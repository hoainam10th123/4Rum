import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { CommentParent } from 'src/app/_models/CommentParent';
import { UpdateComment } from 'src/app/_models/updateComment';
import { CommentService } from 'src/app/_services/comment.service';
import { MyStreamService } from 'src/app/_services/my-stream.service';

@Component({
  selector: 'app-asnwer-modal',
  templateUrl: './asnwer-modal.component.html',
  styleUrls: ['./asnwer-modal.component.css']
})
export class AsnwerModalComponent implements OnInit {

  @ViewChild('form') form: NgForm;
  markdown = "";
  
  constructor(public bsModalRef: BsModalRef, private commentService: CommentService, private stream: MyStreamService) { }

  ngOnInit(): void { 
    if(this._isEdit){
      this.markdown = this.comment.noiDung;
    }
  }

  list: any[] = [];//demo
  questionId: any;
  _isEdit: boolean;
  comment: CommentParent;
  userCommentTo: string;

  save(){
    if(this._isEdit){//true: edit
      this.comment.noiDung = this.markdown;
      this.commentService.updateParent(this.comment).subscribe(()=>{
        this.stream.UpdateCommentParent = new UpdateComment(this.comment.id, this.markdown);
        this.bsModalRef.hide();
      })
    }else{// them moi
      let model = {
        noiDung:this.markdown,
        questionId: this.questionId,
        userCommentTo: this.userCommentTo
      };
      this.commentService.addCommentParent(model).subscribe((data : CommentParent)=>{
        this.stream.commentParent = data;
        this.bsModalRef.hide();
      })
    }
  }
}

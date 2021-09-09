import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { CommentChildren, CommentParent } from 'src/app/_models/CommentParent';
import { CommentService } from 'src/app/_services/comment.service';
import { MyStreamService } from 'src/app/_services/my-stream.service';

@Component({
  selector: 'app-confirm-comment',
  templateUrl: './confirm-comment.component.html',
  styleUrls: ['./confirm-comment.component.css']
})
export class ConfirmCommentComponent implements OnInit {

  id: any;
  isCommentParent: boolean;

  constructor(private toastr: ToastrService, private stream: MyStreamService, public bsModalRef: BsModalRef, private commentService: CommentService) { }

  ngOnInit(): void {
  }

  confirm(){
    if(this.isCommentParent){
      this.commentService.deleteParent(this.id).subscribe((data: CommentParent) =>{
        if(data){
          this.stream.delComment = data;
        }else{
          this.toastr.warning("Không có gì để xóa");        
        }
        this.bsModalRef.hide();
      });
    }else{
      this.commentService.deleteChildrent(this.id).subscribe((data: CommentChildren) =>{
        if(data){
          this.stream.delComment = data;
        }else{
          this.toastr.warning("Không có gì để xóa");        
        }
        this.bsModalRef.hide();
      });
    }
  }
}

import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CommentChildren, CommentParent } from 'src/app/_models/CommentParent';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { AsnwerModalComponent } from '../modals/asnwer-modal/asnwer-modal.component';
import { ConfirmCommentComponent } from '../modals/confirm-comment/confirm-comment.component';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  @Input() commentParent: CommentParent;
  @Input() commentChildrent: CommentChildren;
  @Input() commentParentId: string = "";
  @Input() commentChildrentId:number;
  @Output() editChildCommentEvent = new EventEmitter();//edit(comment: CommentChildren)
  bsModalRef: BsModalRef;  
  userCurrent: User;

  constructor(private accountService: AccountService, private modalService: BsModalService) {
    this.accountService.currentUser$.subscribe(user=>{
      this.userCurrent = user;
    })     
  }

  ngOnInit(): void {
  }

  openConfirmModal(id, isParent: boolean){
    if(isParent){
      const initialState = {      
        id: id,
        isCommentParent: true
      };   
      this.bsModalRef = this.modalService.show(ConfirmCommentComponent, {initialState});
    }else{
      const initialState = {      
        id: id,
        isCommentParent: false
      };   
      this.bsModalRef = this.modalService.show(ConfirmCommentComponent, {initialState});
    }    
  }

  //false: thêm mới, true là edit
  openModalWithComponent(id, isEdit: boolean, comment) { 
    const initialState = {
      list: ['a', 'b'],
      questionId: id,
      _isEdit: isEdit,
      comment:comment
    };   
    this.bsModalRef = this.modalService.show(AsnwerModalComponent, {initialState});
  }
  
  edit(comment: CommentChildren){
    this.editChildCommentEvent.emit(comment);
  }

}

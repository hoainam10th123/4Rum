import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommentParent } from 'src/app/_models/CommentParent';
import { CommentService } from 'src/app/_services/comment.service';

@Component({
  selector: 'app-notifi-comment-child',
  templateUrl: './notifi-comment-child.component.html',
  styleUrls: ['./notifi-comment-child.component.css']
})
export class NotifiCommentChildComponent implements OnInit {
  commentParent: CommentParent;
  idChildrent: number;  

  constructor(private route: ActivatedRoute, private commentService: CommentService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(result =>
    {
      this.commentService.getCommentParent(result.get('idparent')).subscribe(parent=>{
        this.commentParent = parent;
      })
      this.idChildrent = Number.parseInt(result.get('idchildrent'));
    }); 
  }

}

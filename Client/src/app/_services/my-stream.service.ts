import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService, SocialUser } from 'angularx-social-login';
import { ReplaySubject } from 'rxjs';
import { CommentParent } from '../_models/CommentParent';
import { UpdateComment } from '../_models/updateComment';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class MyStreamService {

  socialUser: SocialUser;

  private commentParentSource = new ReplaySubject<CommentParent>(1);
  commentParent$ = this.commentParentSource.asObservable();

  private updateCommentParentSource = new ReplaySubject<UpdateComment>(1);
  updateCommentParent$ = this.updateCommentParentSource.asObservable();

  private delCommentSource = new ReplaySubject<any>(1);
  delComment$ = this.delCommentSource.asObservable();

  private delQuestionSource = new ReplaySubject<string>(1);
  delQuestion$ = this.delQuestionSource.asObservable();

  constructor(private router: Router, private accountService: AccountService, private authService: SocialAuthService) {
    this.authService.authState.subscribe((user) => {
      this.userSocial = user;
      if (this.userSocial) {
        let model = {
          email: this.userSocial.email,
          name: this.userSocial.name,
          photoUrl: this.userSocial.photoUrl
        }
        this.accountService.loginWithSocial(model).subscribe(res => {
          this.router.navigateByUrl('/');
        })
      }
    });
  }

  set commentParent(value: CommentParent) {
    this.commentParentSource.next(value);
  }

  set UpdateCommentParent(value: UpdateComment) {
    this.updateCommentParentSource.next(value);
  }

  set delComment(value: any) {
    this.delCommentSource.next(value);
  }

  set delQuestion(value: string) {
    this.delQuestionSource.next(value);
  }

  set userSocial(value: SocialUser) {
    this.socialUser = value;
  }

  get userSocial(): SocialUser {
    return this.socialUser;
  }

  signOutSocial(): void {
    this.authService.signOut();
  }

  signInWithGoogle(): void {
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  signInWithFB(): void {
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }
}

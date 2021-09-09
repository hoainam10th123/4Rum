import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminHeaderComponent } from './_layout/admin-header/admin-header.component';
import { AdminLayoutComponent } from './_layout/admin-layout/admin-layout.component';
import { SiteLayoutComponent } from './_layout/site-layout/site-layout.component';
import { SiteHeaderComponent } from './_layout/site-header/site-header.component';
import { SiteFooterComponent } from './_layout/site-footer/site-footer.component';
import { HomeComponent } from './components/home/home.component';
import { AboutComponent } from './components/about/about.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { LoginComponent } from './components/login/login.component';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { DetailQuestionComponent } from './components/detail-question/detail-question.component';
import { CommentComponent } from './components/comment/comment.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import {ToastrModule} from 'ngx-toastr';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { NgxSpinnerModule } from 'ngx-spinner';

import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { LoadingInterceptor } from './_interceptor/loading.interceptor';
import { JwtInterceptor } from './_interceptor/jwt.interceptor';
import { ErrorInterceptor } from './_interceptor/error.interceptor';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ServerErrorComponent } from './components/server-error/server-error.component';
import { UserComponent } from './components/user/user.component';
import { UserManaComponent } from './components/admin/user/user.component';
import { JobComponent } from './components/job/job.component';

import { EditorQuestionComponent } from './components/editor-question/editor-question.component';
import { MarkdownModule } from 'ngx-markdown';
import { AlertModule } from 'ngx-bootstrap/alert';

import { Select2Module } from "ng-select2-component";
import { AsnwerModalComponent } from './components/modals/asnwer-modal/asnwer-modal.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ShortDatePipe } from './pipes/short-date.pipe';
import { ConfirmQuestionComponent } from './components/modals/confirm-question/confirm-question.component';
import { CommentChildrentComponent } from './components/comment-childrent/comment-childrent.component';
import { SocialLoginModule, SocialAuthServiceConfig } from 'angularx-social-login';
import { GoogleLoginProvider, FacebookLoginProvider } from 'angularx-social-login';
import { ConfirmCommentComponent } from './components/modals/confirm-comment/confirm-comment.component';
import { EditQuestionComponent } from './components/edit-question/edit-question.component';
import { ViewNumberPipe } from './pipes/view-number.pipe';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { NotificationDetailComponent } from './components/notification-detail/notification-detail.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { NotifiCommentChildComponent } from './components/notifi-comment-child/notifi-comment-child.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminHeaderComponent,
    AdminLayoutComponent,
    SiteLayoutComponent,
    SiteHeaderComponent,
    SiteFooterComponent,
    HomeComponent,
    AboutComponent,
    DashboardComponent,
    LoginComponent,
    DetailQuestionComponent,
    CommentComponent,
    NotFoundComponent,
    ServerErrorComponent,
    UserComponent,
    UserManaComponent,
    JobComponent,
    EditorQuestionComponent,
    AsnwerModalComponent,
    ShortDatePipe,
    ConfirmQuestionComponent,
    CommentChildrentComponent,
    ConfirmCommentComponent,
    EditQuestionComponent,
    ViewNumberPipe,
    NotificationDetailComponent,
    HasRoleDirective,
    NotifiCommentChildComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NgxSpinnerModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    Select2Module,
    SocialLoginModule,
    InfiniteScrollModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    TypeaheadModule.forRoot(),
    MarkdownModule.forRoot(),
    AlertModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-center'//left, right, top, bottom
    })
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi:true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi:true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi:true},
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '190644001912-b8m15k6eojce7gpmeupskv8lgrceebgb.apps.googleusercontent.com'
            )
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider('')
          }
        ]
      } as SocialAuthServiceConfig,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

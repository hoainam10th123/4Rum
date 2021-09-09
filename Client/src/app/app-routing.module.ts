import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './components/about/about.component';
import { UserManaComponent } from './components/admin/user/user.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DetailQuestionComponent } from './components/detail-question/detail-question.component';
import { EditQuestionComponent } from './components/edit-question/edit-question.component';
import { EditorQuestionComponent } from './components/editor-question/editor-question.component';
import { HomeComponent } from './components/home/home.component';
import { JobComponent } from './components/job/job.component';
import { LoginComponent } from './components/login/login.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { NotifiCommentChildComponent } from './components/notifi-comment-child/notifi-comment-child.component';
import { NotificationDetailComponent } from './components/notification-detail/notification-detail.component';
import { ServerErrorComponent } from './components/server-error/server-error.component';
import { UserComponent } from './components/user/user.component';
import { AdminGuard } from './_guards/admin.guard';
import { AuthGuard } from './_guards/auth.guard';
import { AdminLayoutComponent } from './_layout/admin-layout/admin-layout.component';
import { SiteLayoutComponent } from './_layout/site-layout/site-layout.component';
import { QuestionDetailedResolver } from './_resolvers/question-detail.resolver';

const routes: Routes = [
  {
    path: '',
    component: SiteLayoutComponent,
    children: [
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'about', component: AboutComponent },
      { path: 'login', component: LoginComponent },  
      { path: 'user', component: UserComponent },    
      { path: 'job', component: JobComponent },  
      { path: 'ask-question', component: EditorQuestionComponent, canActivate: [AuthGuard] }, 
      { path: 'detail/:id', component: DetailQuestionComponent, resolve:{question: QuestionDetailedResolver} },
      { path: 'edit/:id', component: EditQuestionComponent, resolve:{question: QuestionDetailedResolver} },
      { path: 'notification-comment-child/:idparent/:idchildrent', component: NotifiCommentChildComponent },
      { path: 'notification-detail/:id/:commentid', component: NotificationDetailComponent, resolve:{question: QuestionDetailedResolver} }
    ]
  },
  // App routes goes here here
  {
    path: '',
    runGuardsAndResolvers:'always',
    canActivate: [AdminGuard],
    component: AdminLayoutComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'user-mana', component: UserManaComponent }
    ]
  },

  { path: 'login-admin', component: LoginComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' },

  // otherwise redirect to home
  //{ path: '**', redirectTo: 'home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

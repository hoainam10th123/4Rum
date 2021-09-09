import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { SocialAuthService, SocialUser } from "angularx-social-login";
import { MyStreamService } from 'src/app/_services/my-stream.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  user: User;
  
  constructor(private stream: MyStreamService, private accountService: AccountService, private router: Router, private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=> this.user = user);
  }

  ngOnInit(): void {
    if(this.user){//if user login then redict to home page
      this.router.navigateByUrl('/');
    }
    this.khoiTaoForm();    
  }

  khoiTaoForm() {
    this.loginForm = new FormGroup({
      userName: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(80)])
    })
  }

  login(){
    this.accountService.login(this.loginForm.value).subscribe(res=>{
      //this.toastr.success('Đăng nhập thành công!');
      this.router.navigateByUrl('/');
    }, error=>{
      console.log(error);
      this.toastr.error(error.error);
    })
  }

  loginSocial(){
    this.stream.signInWithGoogle();
  }

}

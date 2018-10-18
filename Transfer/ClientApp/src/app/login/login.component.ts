import { Component, OnInit } from '@angular/core';
import { LoginService } from './login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private data: LoginService, private router: Router) { }

  errorMessage: string = "";

  public creds = {
    username: "",
    password: ""
  };

  onLogin() {
    console.log("it went into onLogin()");

    this.data.login(this.creds)
      .subscribe(success => {
        if (success) {
          this.router.navigate(["home"]); // a moze i na partnera

        }
      }, err => this.errorMessage = "Failed to login")
  }

  ngOnInit() {
  }

}

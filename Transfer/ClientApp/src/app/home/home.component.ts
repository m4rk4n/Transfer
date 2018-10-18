import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../login/login.service';

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(public data: LoginService, private router: Router) {

  }
  title = 'Transfer d.o.o.';

  onAddPartner() {
    if (this.data.loginRequired) {
      // force login
      this.router.navigate(["login"]);
    } else {
      // go to checkout
      this.router.navigate(["partner"]);
    }
  }
}

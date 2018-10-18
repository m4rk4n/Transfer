import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from './login/login.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(private translate: TranslateService, public toastr: ToastrService, public data: LoginService, private router: Router) {
    this.translate.setDefaultLang('en');
  }
  title = 'Transfer d.o.o.';

  onLogout() {
    this.data.token = "";
    this.toastr.info('Logout successful!', '', {
      timeOut: 3000
    });
    this.router.navigate(["home"]);
  }

  useLanguage(language: string) {
    this.translate.use(language);
  }
}

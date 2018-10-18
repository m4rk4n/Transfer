import { Component, OnInit } from '@angular/core';
import { RegisterService } from './register.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

    public errorMessage: string = "";
    public userInfo = {
        userName: "",
        firstName: "",
        lastName: "",
        email: "",
        phoneNumber: "",
        country: "",
        password: ""
    }

  constructor(private toastr: ToastrService, private data: RegisterService, private router: Router) { }

  onRegister() {
    this.data.register(this.userInfo)
      .subscribe(success => {
        this.toastr.success('Registration successful!', '', {
          timeOut: 3000
        });
        this.router.navigate(['home']);

      }, err => this.errorMessage = "Failed to register")
  }

  ngOnInit() {
  }
}

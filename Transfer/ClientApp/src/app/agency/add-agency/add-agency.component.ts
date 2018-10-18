import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AgencyService } from '../agency.service';
import { LoginService } from '../../login/login.service';
import { Agency } from '../agency';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-add-agency',
  templateUrl: './add-agency.component.html',
  styleUrls: ['./add-agency.component.css']
})
export class AddAgencyComponent implements OnInit {

  constructor(private translate: TranslateService, private toastr: ToastrService, public data: AgencyService, public loginData: LoginService, public router: Router) { }
  public errorMessage: string;

  public agency = new Agency();

  ngOnInit() {
    if (this.loginData.loginRequired) {
      this.router.navigate(["login"]);
    }
  }

  OnAddNewAgency() {
    this.data.addNewAgency(this.agency)
      .subscribe(success => {
        if (success) {
          this.translate.get('agency.add-agency.agency_added_toastr').subscribe((res: string) => {
            this.toastr.success(res, '', {
              timeOut: 3000
            });
            this.router.navigate(["agency"]);
          }, err => this.translate.get('agency.add-agency.failed_to_save_agency_toastr').subscribe((res: string) => {
            this.toastr.error(res, '', {
              timeOut: 3000
            });
          }) 
          );
        }
      }); // loool
  }
}

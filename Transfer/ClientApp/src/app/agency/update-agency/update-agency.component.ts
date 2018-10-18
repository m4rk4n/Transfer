import { Component, OnInit } from '@angular/core';
import { AgencyService } from '../agency.service';
import { LoginService } from '../../login/login.service';
import { Router } from '@angular/router';
import { Agency } from '../agency';
import { agencyPartnersViewModel } from '../agencyPartnersViewModel';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';


@Component({
  selector: 'app-update-agency',
  templateUrl: './update-agency.component.html',
  styleUrls: ['./update-agency.component.css']
})
export class UpdateAgencyComponent implements OnInit {

  constructor(
    private translate: TranslateService,
    private toastr: ToastrService,
    public data: AgencyService,
    public loginData: LoginService,
    public router: Router
  ) { }
  agencyPartners: agencyPartnersViewModel[] = [];
  updAgency: Agency;
  errorMessage = "";

  ngOnInit() {
    if (this.loginData.loginRequired) {
      this.router.navigate(["login"]);
    } else {
      this.updAgency = this.data.selectedAgency; // i can live without this one
      this.data.getAgencyPartners(this.updAgency)
        .subscribe(success => {
          if (success) {
            console.log(this.data.agencyPartners);
            this.agencyPartners = this.data.agencyPartners;
          }
        }, err => "Failed to get agency partners");
    }
  }

  OnUpdateAgency() {
    console.log(this.updAgency);
    console.log(this.agencyPartners);
    this.data.updateAgency(this.updAgency, this.agencyPartners)
      .subscribe(success => {
        if (success) {
          this.translate.get('agency.update-agency.agency_updated_toastr').subscribe((res: string) => {
            this.toastr.success(res, '', {
              timeOut: 3000
            });
          });
          this.router.navigate(["agency"])
        }
      }, err => this.errorMessage = "Failed to update the agency");
  }

}

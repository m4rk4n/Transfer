import { Component, OnInit } from '@angular/core';
import { AgencyService } from './agency.service';
import { LoginService } from '../login/login.service';
import { Router } from '@angular/router';
import { Agency } from './agency';

import { TemplateRef } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-agency',
  templateUrl: './agency.component.html',
  styleUrls: ['./agency.component.css']
})
export class AgencyComponent implements OnInit {

  constructor(private translate: TranslateService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    public data: AgencyService,
    public loginData: LoginService,
    public router: Router) { }

  modalRef: BsModalRef;
  public agencies: Agency[] = [];
  private errorMessage = "";

  ngOnInit() {
    if (this.loginData.loginRequired) {
      this.router.navigate(["login"]);
    } else {
      this.data.getAllAgencies()
        .subscribe(success => {
          if (success) {
            this.agencies = this.data.AllAgencies;
          }
        }, err => this.translate.get('agency.getAllAgencies_error').subscribe((res: string) => {
          this.toastr.error(res, '', {
            timeOut: 3000
          });
        })
        );
    }
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirmDeleteModal(agency: Agency): void {
    this.data.deleteAgency(agency)
      .subscribe(success => {
        if (success) {
          this.translate.get('agency.toastr_deleted').subscribe((res: string) => {
            this.toastr.success(res, '', {
              timeOut: 3000
            });
          });
          this.router.navigate(["home"])
            .then(() => {
              this.router.navigate(["agency"]);
              this.modalRef.hide();
            });
        }
      }, err => this.translate.get('agency.deleteAgency_error').subscribe((res: string) => {
        this.toastr.success(res, '', {
          timeOut: 3000
        });
      })
      );
    this.modalRef.hide();
  }

  decline(): void {
    this.modalRef.hide();
  }

  OnUpdateClicked(agency: Agency) {
    console.log(agency);
    this.data.selectedAgency = agency;
    this.router.navigate(["agency/Update"]);
  }

}

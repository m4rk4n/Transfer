import { Component, OnInit } from '@angular/core';
import { PartnerService } from './partner.service';
import { LoginService } from '../login/login.service';
import { Router } from '@angular/router';
import { Partner } from './partner';

import { TemplateRef } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-partner',
  templateUrl: './partner.component.html',
  styleUrls: ['./partner.component.css']
})
export class PartnerComponent implements OnInit {

  constructor(private toastr: ToastrService, private modalService: BsModalService, public data: PartnerService, public loginData: LoginService, public router: Router) { }

  public errorMessage = "";
  modalRef: BsModalRef;
  public partners: Partner[] = [];

  ngOnInit() {
    if (this.loginData.loginRequired) {
      this.router.navigate(["login"]);
    } else {
      this.data.getAllPartners()
        .subscribe(success => {
          if (success) {
            this.partners = this.data.allPartners;
          }
        }, err => this.errorMessage = "Failed to get all partners");
    }
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirmDeleteModal(partner: Partner): void {
    this.data.deletePartner(partner)
      .subscribe(success => {
        if (success) {
          this.toastr.success('Partner deleted!', '', {
            timeOut: 3000
          });
          this.router.navigate(["home"])
            .then(() => {
            this.router.navigate(["partner"]);
            this.modalRef.hide();
          });
        }
      }, err => "failed to delete the partner");
  }

  decline(): void {
    this.modalRef.hide();
  }

  OnUpdateClicked(partner: Partner) {
    this.data.selectedPartner = partner;
    this.router.navigate(["partner/Update"]);
  }
}

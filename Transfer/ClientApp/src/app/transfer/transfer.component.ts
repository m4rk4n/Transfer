import { Component, OnInit, ViewChild  } from '@angular/core';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { Transfer } from './transfer';
import { TransferService } from './transfer.service';
import { Agency } from '../agency/agency';
import { Partner } from '../partner/partner';
import { Vehicle } from '../vehicle/vehicle';
import { PartnerAgenciesViewModel } from '../partner/partnerAgenciesViewModel';
import { agencyPartnersViewModel } from '../agency/agencyPartnersViewModel';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from '../login/login.service';
import { Router } from '@angular/router';
import { TabsetComponent } from 'ngx-bootstrap';

@Component({
  selector: 'app-transfer',
  templateUrl: './transfer.component.html',
  styleUrls: ['./transfer.component.css']
})
export class TransferComponent implements OnInit {

  errorMessage = "";
  agencyTravel: boolean;
  selectedAgency: Agency;
  selectedPartner: Partner;
  partnerVehicle: Vehicle;
  transfer: Transfer = new Transfer();
  agencies: Agency[] = [];
  agencyPartners: agencyPartnersViewModel[] = [];
  allPartners: Partner[] = [];
  partnersToChooseFrom: Partner[] = [];
  allVehicles: Vehicle[] = []; // get them all at once, or get one by one?

  constructor(private toastr: ToastrService, private data: TransferService, private loginData: LoginService, private router: Router) { }

  ngOnInit() {
    if (this.loginData.loginRequired) {
      this.router.navigate(["login"]);
    } else {
      this.data.GetAllPartners()
        .subscribe(success => {
          this.allPartners = this.data.allPartners;
          this.partnersToChooseFrom = this.allPartners;
        }, err => this.errorMessage = "Failed to get partners");

      this.data.GetAllVehicles()
        .subscribe(success => {
          if (success) {
            this.allVehicles = this.data.allVehicles;
          }
        }, err => this.errorMessage = "Failed to get vehicles");
    }
  }

  OnAgencyTravelChanged() {
    this.agencyTravel = !this.agencyTravel;
    this.data.GetAllAgencies()
      .subscribe(success => {
        if (success) {
          this.agencies = this.data.agencies
        }
      }, err => this.errorMessage = "Failed to get agencies");
  }

  OnAgencySelected(selectedAgency) {
    //Load Corresponding partners
    this.data.getAgencyPartners(selectedAgency)
      .subscribe(success => {
        if (success) {
          this.selectedAgency = <Agency>selectedAgency;
          this.agencyPartners = this.data.agencyPartners;
          this.partnersToChooseFrom = [];
          for (let i = 0; i < this.agencyPartners.length; i++) {
            if (this.agencyPartners[i].fromAgency == true) {
              this.partnersToChooseFrom.push(this.allPartners.find(x => x.id == this.agencyPartners[i].partnerId));
            }
          }
        }
      }, err => this.errorMessage = "Failed to get agencyPartners");

  }

  OnPartnerSelected(selectedPartner) {
    this.selectedPartner = <Partner>selectedPartner
    this.partnerVehicle = this.allVehicles.find(x => x.id == selectedPartner.vehicleId);
  }

  OnAddNewTransfer() {
    this.transfer.agency = this.selectedAgency;
    this.transfer.partner = this.selectedPartner;

    this.data.AddNewTransfer(this.transfer)
      .subscribe(success => {
        if (success) {
          this.toastr.success('New Transfer added!', '', {
            timeOut: 3000
          });
        }
      }, err => this.errorMessage = "Failed to save transfer");
  }
}

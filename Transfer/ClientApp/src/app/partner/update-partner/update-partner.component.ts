import { Component, OnInit } from '@angular/core';
import { PartnerService } from '../partner.service';
import { LoginService } from '../../login/login.service';
import { Router } from '@angular/router';
import { Partner } from '../partner';
import { PartnerAgenciesViewModel } from '../partnerAgenciesViewModel';
import { FormsModule } from '@angular/forms';
import { Vehicle } from '../../vehicle/vehicle';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-update-partner',
  templateUrl: './update-partner.component.html',
  styleUrls: ['./update-partner.component.css']
})
export class UpdatePartnerComponent implements OnInit {

  constructor(private toastr: ToastrService, public data: PartnerService, public loginData: LoginService, public router: Router) { }
  partnerAgencies: PartnerAgenciesViewModel[] = [];
  updPartner: Partner;
  selectedVehicle: Vehicle = new Vehicle();
  allVehicles: Vehicle[] = [];
  partnerVehicle: Vehicle = new Vehicle();
  vehicleId: number;
  errorMessage = "";

  ngOnInit() {
    if (this.loginData.loginRequired) {
      this.router.navigate(["login"]);
    } else {
      this.updPartner = this.data.selectedPartner; // i can live without this one
      this.data.getPartnerAgencies(this.updPartner)
        .subscribe(success => {
          if (success) {
            this.partnerAgencies = this.data.partnerAgencies;
          }
        }, err => "Failed to get partner agencies");

      this.data.getPartner() // selected partner id already in service, 
        .subscribe(success => {
          if (success) {
            this.vehicleId = this.data.partnerVehicleId; // do i need  it at all
            this.data.getVehicles()
              .subscribe(success => {
                if (success) {
                  this.allVehicles = this.data.allVehicles;
                  this.partnerVehicle = this.allVehicles.find(x => x.id === this.vehicleId);
                }
              }, err => "Failed to get Vehicles");
          }
        }, err => "Failed to get Partner");
    }
  }

  onVehicleSelected(selectedVehicle) {
    this.partnerVehicle = selectedVehicle;
  }

  OnUpdatePartner() {
    console.log(this.updPartner);
    console.log(this.partnerAgencies);
    this.data.updatePartner(this.updPartner, this.partnerAgencies, this.partnerVehicle.id)
      .subscribe(success => {
        if (success) {
          this.toastr.success('Partner updated!', '', {
            timeOut: 3000
          });
          this.router.navigate(["partner"]);
        }
      }, err => this.errorMessage = "Failed to update the partner");
  }
}

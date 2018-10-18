import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PartnerService } from '../partner.service';
import { LoginService } from '../../login/login.service';
import { Partner } from '../partner';
import { Vehicle } from '../../vehicle/vehicle';
import { partnerAddModel } from '../partnerAddModel';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-partner',
  templateUrl: './add-partner.component.html',
  styleUrls: ['./add-partner.component.css']
})
export class AddPartnerComponent implements OnInit {

  constructor(private toastr: ToastrService, public data: PartnerService, public loginData: LoginService, public router: Router) { }

  public errorMessage: string;
  partnerAddModel: partnerAddModel = new partnerAddModel();
  public partner = new Partner();
  vehicles: Vehicle[] = [];
  selectedVehicle: Vehicle = new Vehicle();

  ngOnInit() {
    if (this.loginData.loginRequired) {
      this.router.navigate(["login"]);
    } else {
      this.data.getVehicles()
        .subscribe(success => {
          if (success) {
            this.vehicles = this.data.allVehicles;
          }
        }, err => this.errorMessage = "Failed to get vehicles");
    }
  }

  onVehicleSelected(selectedVehicle) {
    this.selectedVehicle = selectedVehicle;
  }

  OnAddNewPartner() {

    this.partnerAddModel.partner = this.partner;
    if (this.selectedVehicle.id) // it checks both for null and undefined
      this.partnerAddModel.vehicleId = this.selectedVehicle.id;
    this.data.addNewPartner(this.partnerAddModel)
      .subscribe(success => {
        if (success) {
          this.toastr.success('New partner added!', '', {
            timeOut: 3000
          });
          this.router.navigate(["partner"]);
        }
      }, err => this.errorMessage = "Failed to save partner");
  }
}

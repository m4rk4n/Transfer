import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { VehicleService } from './vehicle.service';
import { LoginService } from '../login/login.service';
import { Router } from '@angular/router';
import { Vehicle } from './vehicle';

import { TemplateRef } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.css']
})
export class VehicleComponent implements OnInit {

  constructor(private toastr: ToastrService, private modalService: BsModalService, public data: VehicleService, public loginData: LoginService, public router: Router) { }

  public errorMessage: string;
  public selectedVehicle: Vehicle;
  modalRef: BsModalRef;

  public Vehicles :Vehicle[] = [];

  ngOnInit() {
    if (this.loginData.loginRequired) {
      this.router.navigate(["login"]);
    } else {
      this.data.getAllVehicles()
        .subscribe(success => {
          if (success) {
            this.Vehicles = this.data.AllVehicles;
          }
        }, err => this.errorMessage = "Failed to get vehicles");
    }
  }

  OnUpdateClicked(vehicle: Vehicle) {
    this.data.selectedVehicle = vehicle;
    this.router.navigate(["vehicle/Update"]);
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirmDeleteModal(vehicle: Vehicle): void {
    this.data.deleteVehicle(vehicle)
      .subscribe(success => {
        if (success) {
          this.toastr.success('Vehicle deleted!', '', {
            timeOut: 3000
          });
          this.router.navigate(["home"])
            .then(() => {
              this.router.navigate(["vehicle"]);
              this.modalRef.hide();
            });
        }
      }, err => "failed to delete the vehicle");
    this.modalRef.hide();
  }

  decline(): void {
    this.modalRef.hide();
  }

}

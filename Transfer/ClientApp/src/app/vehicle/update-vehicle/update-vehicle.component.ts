import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../vehicle.service';
import { LoginService } from '../../login/login.service';
import { Router } from '@angular/router';
import { Vehicle } from '../vehicle';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-update-vehicle',
  templateUrl: './update-vehicle.component.html',
  styleUrls: ['./update-vehicle.component.css']
})
export class UpdateVehicleComponent implements OnInit {

  constructor(private toastr: ToastrService, public data: VehicleService, public loginData: LoginService, public router: Router) { }
  vehicle: Vehicle;
  regDate: Date;
  errorMessage = "";

  ngOnInit() {
    if (this.loginData.loginRequired) {
      this.router.navigate(["login"]);
    } else {
      this.vehicle = this.data.selectedVehicle;
      let datestring = this.data.selectedVehicle.registrationDate.toLocaleString();
      this.regDate = new Date(datestring);
    }
  }

  OnUpdateVehicle() {
    console.log(this.vehicle);
    this.vehicle.registrationDate = this.regDate;
    this.data.updateVehicle(this.vehicle)
      .subscribe(success => {
        if (success) {
          this.toastr.success('Vehicle Updated!', '', {
            timeOut: 3000
          });
          this.router.navigate(["vehicle"]);
        }
      }, err => this.errorMessage = "Failed to update the vehicle");
  }

}

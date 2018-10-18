import { Component, OnInit } from '@angular/core';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { Vehicle } from '../vehicle';
import { VehicleService } from '../vehicle.service';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from '../../login/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-vehicle',
  templateUrl: './add-vehicle.component.html',
  styleUrls: ['./add-vehicle.component.css']
})
export class AddVehicleComponent implements OnInit {

  constructor(private toastr: ToastrService, private data: VehicleService, private loginData: LoginService, private router: Router) { }

  bsConfig: Partial<BsDatepickerConfig>;
  vehicle: Vehicle = new Vehicle();
  regDate: Date;
  errorMessage = "";
  colorTheme = 'theme-green';

  ngOnInit() {
    if (this.loginData.loginRequired) {
      this.router.navigate(["login"]);
    } else {
      this.bsConfig = Object.assign({}, { containerClass: this.colorTheme });
    }
  }

  OnAddNewVehicle() {
    this.vehicle.registrationDate = this.regDate;
    this.data.addNewVehicle(this.vehicle)
      .subscribe(success => {
        if (success) {
          this.toastr.success('New vehicle added!', '', {
            timeOut: 3000
          });
          this.router.navigate(["vehicle"]);
        }
      }, err => this.errorMessage = "Failed to save vehicle");
  }
}

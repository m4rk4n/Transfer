import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs/Observable';
import { LoginService } from '../login/login.service';
import { Vehicle } from './vehicle';

@Injectable()
export class VehicleService {

  constructor(private http: HttpClient, public loginData: LoginService) { }

  public AllVehicles: Vehicle[] = [];
  public selectedVehicle: Vehicle;

  public addNewVehicle(vehicle): Observable<boolean> {
    return this.http.post("http://localhost:9999/api/vehicle", vehicle, {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map(response => {

        return true;
      })
  }

  public getAllVehicles(): Observable<boolean> {
    return this.http.get("http://localhost:9999/api/vehicle", {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map((data: any[]) => {
        this.AllVehicles = data;
        return true;
      })
  }

  public deleteVehicle(vehicle: Vehicle): Observable<boolean> {
    var url = "http://localhost:9999/api/vehicle/" + vehicle.id.toString();

    return this.http.delete(url, {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map(response => {

        return true;
      })
  }

  public updateVehicle(vehicle: Vehicle): Observable<boolean> {
    var url = "http://localhost:9999/api/vehicle/" + vehicle.id.toString();

    this.selectedVehicle = vehicle;

    console.log(this.selectedVehicle);
    return this.http.put(url, this.selectedVehicle, {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map(response => {

        return true;
      })
  }
}

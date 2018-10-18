import { Injectable } from '@angular/core';
import { Partner } from './partner'
import { LoginService } from '../login/login.service';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { PartnerAgenciesViewModel } from './partnerAgenciesViewModel';
import { PartnerUpdateModel } from './partnerUpdateModel';
import { Vehicle } from '../vehicle/vehicle';
import { partnerAddModel } from './partnerAddModel';

@Injectable()
export class PartnerService {

  constructor(private http: HttpClient, public loginData: LoginService) { }

  public allPartners: Partner[] = [];
  public selectedPartner: Partner;
  public partnerAgencies: PartnerAgenciesViewModel[] = [];
  public updateModel: PartnerUpdateModel = new PartnerUpdateModel();
  public allVehicles: Vehicle[] = [];
  public partnerVehicle: Vehicle = new Vehicle(); // maybe dont need this one
  public partnerVehicleId: number;

  public getAllPartners(): Observable<boolean> {
    return this.http.get("http://localhost:9999/api/partner", {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map((data: any[]) => {
        this.allPartners = data;
        return true;
      })
  }

  public getPartner(): Observable<boolean> {
    var url = "http://localhost:9999/api/partner/" + this.selectedPartner.id.toString();

    return this.http.get(url, {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map((data: any) => {
        console.log("vehicleId from PartnerService.getPartner(): " + data.vehicleId);
        this.partnerVehicleId = data.vehicleId;
        return true;
      })
  }
  

  public addNewPartner(partnerAddModel: partnerAddModel): Observable<boolean> {
    return this.http.post("http://localhost:9999/api/partner", partnerAddModel, {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map(response => {

        return true;
      })
  }

  public updatePartner(partner: Partner, partnerAgencies: PartnerAgenciesViewModel[], vehicleId: number): Observable<boolean> {
    var url = "http://localhost:9999/api/partner/" + partner.id.toString();

    this.updateModel.partner = partner;
    this.updateModel.partnerAgencies = partnerAgencies;
    this.updateModel.vehicleId = vehicleId;

    // console.log(this.updateModel);
    return this.http.put(url, this.updateModel, {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map(response => {

        return true;
      })
  }

  public deletePartner(partner: Partner): Observable<boolean> {
    var url = "http://localhost:9999/api/partner/" + partner.id.toString();

    return this.http.delete(url, {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map(response => {

        return true;
      })
  }

  public getPartnerAgencies(partner: Partner): Observable<boolean> {
    var url = "http://localhost:9999/api/PartnerAgencies/" + partner.id.toString();

    return this.http.get(url, {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map((data: any[]) => {
        this.partnerAgencies = data;
        return true;
      })
  }

  public getVehicles(): Observable<boolean> {
    return this.http.get("http://localhost:9999/api/vehicle", {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map((data: any[]) => {
        this.allVehicles = data;
        return true;
      })
  }
}

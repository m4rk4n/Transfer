import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginService } from '../login/login.service';
import { Transfer } from './transfer';
import { Observable } from 'rxjs/Observable';
import { Agency } from '../agency/agency';
import { Partner } from '../partner/partner';
import { Vehicle } from '../vehicle/vehicle';
import { agencyPartnersViewModel } from '../agency/agencyPartnersViewModel';

@Injectable()
export class TransferService {

  constructor(private http: HttpClient, public loginData: LoginService) { }

  agencies: Agency[] = [];
  allPartners: Partner[] = [];
  agencyPartners: agencyPartnersViewModel[] = [];
  allVehicles: Vehicle[] = [];

  public AddNewTransfer(transfer: Transfer):  Observable<boolean> {
    return this.http.post("http://localhost:9999/api/transfer", transfer, {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map(response => {

        return true;
      })
  }

  GetAllAgencies(): Observable<boolean> {
    return this.http.get("http://localhost:9999/api/agency", {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map((data: any[]) => {
        this.agencies = data;
        return true;
      })
  }

  GetAllPartners(): Observable<boolean> {
    return this.http.get("http://localhost:9999/api/partner", {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map((data: any[]) => {
        this.allPartners = data;
        return true;
      })
  }

  public getAgencyPartners(agency: Agency): Observable<boolean> {
    var url = "http://localhost:9999/api/AgencyPartners/" + agency.id.toString();

    return this.http.get(url, {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map((data: any[]) => {
        this.agencyPartners = data;
        return true;
      })
  }

  public GetAllVehicles(): Observable<boolean> {
    return this.http.get("http://localhost:9999/api/vehicle", {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map((data: any[]) => {
        this.allVehicles = data;
        return true;
      })
  }
}

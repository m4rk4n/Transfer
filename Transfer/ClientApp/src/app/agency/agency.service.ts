import { Injectable } from '@angular/core';
import { Agency } from './agency';
import { LoginService } from '../login/login.service';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { agencyPartnersViewModel } from './agencyPartnersViewModel';
import { AgencyUpdateModel } from './update-agency/agencyUpdateModel';

@Injectable()
export class AgencyService {

  constructor(private http: HttpClient, public loginData: LoginService) { }

  public AllAgencies: Agency[] = [];
  public agencyPartners: agencyPartnersViewModel[] = [];
  public selectedAgency: Agency;
  public updateModel: AgencyUpdateModel = new AgencyUpdateModel();

  public getAllAgencies(): Observable<boolean> {
    return this.http.get("http://localhost:9999/api/agency", {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map((data: any[]) => {
        this.AllAgencies = data;
        return true;
      })
  }

  public addNewAgency(agency: Agency) : Observable<boolean> {
    return this.http.post("http://localhost:9999/api/agency", agency, {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map(response => {

        return true;
      })
  }

  public updateAgency(agency: Agency, agencyPartners: agencyPartnersViewModel[]): Observable<boolean> {
    var url = "http://localhost:9999/api/agency/" + agency.id.toString();

    this.updateModel.agency = agency;
    this.updateModel.agencyPartners = agencyPartners;

    console.log(this.updateModel);
    return this.http.put(url, this.updateModel, {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map(response => {

        return true;
      })
  }

  public deleteAgency(agency: Agency): Observable<boolean> {
    var url = "http://localhost:9999/api/agency/" + agency.id.toString();

    return this.http.delete(url, {
      headers: new HttpHeaders().set("Authorization", "Bearer " + this.loginData.token)
    })
      .map(response => {

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
}

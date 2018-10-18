import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
// import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import { RouterModule } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient, HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { PartnerComponent } from './partner/partner.component';
import { HomeComponent } from './home/home.component';
import { LoginService } from './login/login.service';
import { PartnerService } from './partner/partner.service';
import { AgencyComponent } from './agency/agency.component';
import { AgencyService } from './agency/agency.service';
import { VehicleComponent } from './vehicle/vehicle.component';
import { VehicleService } from './vehicle/vehicle.service';
import { AddVehicleComponent } from './vehicle/add-vehicle/add-vehicle.component';
import { AddAgencyComponent } from './agency/add-agency/add-agency.component';
import { UpdateAgencyComponent } from './agency/update-agency/update-agency.component';
import { AddPartnerComponent } from './partner/add-partner/add-partner.component';
import { UpdatePartnerComponent } from './partner/update-partner/update-partner.component';
import { UpdateVehicleComponent } from './vehicle/update-vehicle/update-vehicle.component';
import { TransferComponent } from './transfer/transfer.component';
import { TransferService } from './transfer/transfer.service';
import { RegisterComponent } from './register/register.component';
import { RegisterService } from './register/register.service';

let routes = [
  { path: "", component: HomeComponent },
  { path: "home", component: HomeComponent },
  { path: "login", component: LoginComponent },
  { path: "partner", component: PartnerComponent },
  { path: "partner/Add", component: AddPartnerComponent },
  { path: "partner/Update", component: UpdatePartnerComponent },
  { path: "agency", component: AgencyComponent },
  { path: "agency/Add", component: AddAgencyComponent },
  { path: "agency/Update", component: UpdateAgencyComponent },
  { path: "vehicle", component: VehicleComponent },
  { path: "vehicle/Add", component: AddVehicleComponent },
  { path: "vehicle/Update", component: UpdateVehicleComponent },
  { path: "transfer", component: TransferComponent },
  { path: "register", component: RegisterComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    PartnerComponent,
    HomeComponent,
    AgencyComponent,
    VehicleComponent,
    AddVehicleComponent,
    AddAgencyComponent,
    UpdateAgencyComponent,
    AddPartnerComponent,
    UpdatePartnerComponent,
    UpdateVehicleComponent,
    TransferComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes, { useHash: true }),
    BsDatepickerModule.forRoot(),
    TimepickerModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    TabsModule.forRoot(),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [ 
    LoginService,
    PartnerService,
    AgencyService,
    VehicleService,
    TransferService,
    RegisterService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, "http://localhost:9999/api/languages/");
}

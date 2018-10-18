import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs//add/operator/map';


@Injectable()
export class LoginService {


  public token: string = ""; //should be private
  private tokenExpiration: Date;

  constructor(private http: HttpClient) { }


  public get loginRequired(): boolean {
    return this.token.length == 0 || this.tokenExpiration > new Date();
  }

  login(creds): Observable<boolean> {
    return this.http
      .post("http://localhost:9999/api/account", creds)
      .map((data: any) => {
        this.token = data.token;
        this.tokenExpiration = data.expiration;
        return true;
      });
  }
}

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class RegisterService {

  constructor(private http: HttpClient) { }

  public register(userInfo): Observable<boolean> {
    console.log(userInfo);
    return this.http
      .post("http://localhost:9999/api/register", userInfo)
      .map((data: any) => {
        return true;
      });
  }
}

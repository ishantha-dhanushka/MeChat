import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { LoggedUser } from '../_model/loggedUser';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  constructor(private http: HttpClient) { }

  // getToken
  getToken = () => {
    return localStorage.getItem("access_token");
  }
  
  // Is Authenticated
  isAuthenticated = (): Observable<boolean> => {
    return new Observable(obs => {
      obs.next(this.getToken() != null);
      obs.complete();
    });
  }
  
  // Login
  Login = (loginObject: any): Observable<boolean> => {
    return new Observable(obs => {
      
      this.http.post(environment.api.url + "/api/auth/login", loginObject).subscribe((response: {expiration : SVGAnimatedString, token: string}) => {
        localStorage.setItem("access_token", response.token);
        obs.next(true);
        obs.complete();
      }, error => {
        obs.error(false);
        obs.complete();
      });
    });
  }
  
  // Get Logged User
  GetLoggedUser = (): Observable<any> => {
    return new Observable(obs => {
      this.http.get(environment.api.url + "/api/auth/user", {
        headers : {
          Authorization : "Bearer " + this.getToken()
        }
      }).subscribe((loggedUser: LoggedUser) => {
        obs.next(loggedUser);
        obs.complete();
      }, error => {
        obs.error(false);
        obs.complete();
      });
    });
  }

  // register user
  RegisterUser = (registerData: any): Observable<LoggedUser> => {
    return new Observable(obs => {
      this.http.post(environment.api.url + "/api/auth/register", registerData).subscribe((user: any) => {
        obs.next(user);
        obs.complete();
      }, error => {
        obs.error(error);
        obs.complete();
      });
    });
  }
}

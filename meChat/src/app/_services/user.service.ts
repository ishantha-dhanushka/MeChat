import { Injectable } from '@angular/core';
import { LoggedUser } from '../_model/loggedUser';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  private logged_user: LoggedUser = null;
  
  constructor() {}

  // set logged User
  setLoggedUser = (logged_user:LoggedUser) => {
    this.logged_user = logged_user;
  }

  // get logged user
  getLoggedUser = ():LoggedUser => {
    return this.logged_user;
  }
}

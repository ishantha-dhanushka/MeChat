import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import { LoggedUser } from '../_model/loggedUser';
import { UserService } from '../_services/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  
  constructor(private router: Router, private authService: AuthService,  private userService: UserService){
    UserService
  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
      return new Observable(obs => {
        this.authService.isAuthenticated().subscribe((isLoggedIn: boolean) => {
          if(isLoggedIn){
            this.authService.GetLoggedUser().subscribe((loggedUser: any) => {
              if(loggedUser){
                this.userService.setLoggedUser(loggedUser);
                obs.next(true);
                obs.complete();
              }else{
                this.router.navigate(['login']);
                obs.next(false);
                obs.complete();
              }
            }, error => {
              this.router.navigate(['login']);
              obs.error(error);
              obs.complete();
            });
          }else{
            this.router.navigate(['login']);
            obs.next(false);
            obs.complete();
          }
        });
      });
    }
  }
  
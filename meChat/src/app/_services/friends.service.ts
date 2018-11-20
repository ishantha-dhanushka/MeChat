import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Friends } from '../_model/friend';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FriendsService {
  
  allFriends: Friends[] = [];
  notFriends: Friends[] = [];
  
  constructor(private http: HttpClient) { }
  
  /// Set All Friends
  SetAllFriends = () => {
    return new Observable(obs => {
      this.http.get(environment.api.url + "/api/friend", {
        headers : {
          Authorization : "Bearer " + localStorage.getItem("access_token")
        }
      }).subscribe((friends: Friends[]) => {
        this.allFriends = friends;
        obs.next(friends);
        obs.complete();
      }, error => {
        obs.error(false);
        obs.complete();
      });
    });
  }
  
  /// set Not Friends
  SetNotFriends = () => {
    return new Observable(obs => {
      this.http.get(environment.api.url + "/api/friend/notfriends", {
        headers : {
          Authorization : "Bearer " + localStorage.getItem("access_token")
        }
      }).subscribe((notfriends: Friends[]) => {
        this.notFriends = notfriends;
        obs.next(notfriends);
        obs.complete();
      }, error => {
        obs.error(false);
        obs.complete();
      });
    });
  }

  // make friend
  makeFriend = (friendId : string ) => {
    return new Observable(obs => {
      this.http.post(environment.api.url + "/api/friend/create", {
        friendId : friendId
      }, {
        headers : {
          Authorization : "Bearer " + localStorage.getItem("access_token")
        }
      }).subscribe((notfriends: Friends[]) => {
        this.notFriends = notfriends;
        obs.next(notfriends);
        obs.complete();
      }, error => {
        obs.error(false);
        obs.complete();
      });
    });
  }
}

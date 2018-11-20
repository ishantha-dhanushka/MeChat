import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { LoggedUser } from '../_model/loggedUser';
import { Router } from '@angular/router';
import { FriendsService } from '../_services/friends.service';
import { HubService } from '../_services/hub.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
  
  loggedUser: LoggedUser = null;
  
  constructor(private userService: UserService, private router: Router, private friendsService: FriendsService, private hubService: HubService) {
    this.loggedUser = this.userService.getLoggedUser(); 
  }
  
  ngOnInit() {
    this.init_metadata();
		this.hubService.initConnection();
  }

  
	// initialize metadata
	init_metadata = () => {

		// set friends list
		this.friendsService.SetAllFriends().subscribe(res => {
		}, error => {
			console.log("Failed to load friends!");
		});

		// set not friends list
		this.friendsService.SetNotFriends().subscribe(res => {
		}, error => {
			console.log("Failed to load not friends!");
		});
	}

  // logout
  logout = () => {
    localStorage.removeItem('access_token');
    this.router.navigate(['login']);
  }
  
}

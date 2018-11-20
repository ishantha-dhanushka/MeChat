import { Component, OnInit } from '@angular/core';
import { Friends } from '../../_model/friend';
import { FriendsService } from '../../_services/friends.service';
import { AlertService } from 'ngx-alerts';

@Component({
  selector: 'find-friends',
  templateUrl: './find-friends.component.html',
  styleUrls: ['./find-friends.component.scss']
})
export class FindFriendsComponent implements OnInit {
  
  not_friends: Friends[] = [];
  
  constructor(public friendsService: FriendsService, private alertService: AlertService) { }
  
  ngOnInit() {
  }
  
  // make friend
  memberCreating: boolean = false;
  makeFriend = (friendId: string) => {
    this.memberCreating = true;
    this.friendsService.makeFriend(friendId).subscribe((response: any) => {
      this.init_metadata();
      this.memberCreating = false;
      this.alertService.success("You are successfully created friend!");
    }, error => {
      this.memberCreating = false;
      this.alertService.danger("Failed to create friend!");
    });
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
}

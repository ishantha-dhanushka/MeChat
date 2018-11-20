import { Component, OnInit, Input } from '@angular/core';
import { FriendsService } from '../../_services/friends.service';
import { Friends } from '../../_model/friend';

@Component({
  selector: 'chat-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.scss']
})
export class FriendListComponent implements OnInit {

  @Input() CHAT_ID = null;

  online_friends: any[] = [];
  friends: Friends[] = [];

  constructor(public friendsService: FriendsService) { }

  ngOnInit() {
  }

}

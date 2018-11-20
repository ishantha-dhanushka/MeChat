import { Component, OnInit } from '@angular/core';
import { FriendsService } from '../../_services/friends.service';
import { Friends } from '../../_model/friend';

@Component({
  selector: 'my-friends',
  templateUrl: './my-friends.component.html',
  styleUrls: ['./my-friends.component.scss']
})
export class MyFriendsComponent implements OnInit {

  friends: Friends[] = [];

  constructor(public friendsService: FriendsService) { }

  ngOnInit() {
   
  }

}

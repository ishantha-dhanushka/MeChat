import { Component, OnInit, Input } from '@angular/core';
import { UserService } from '../../../_services/user.service';
import { LoggedUser } from '../../../_model/loggedUser';
import { MessageDTO } from '../../../_model/message';
import { trigger, transition, useAnimation } from '@angular/animations';
import { bounceIn } from 'ngx-animate';

@Component({
  selector: 'chat-text',
  templateUrl: './chat-text.component.html',
  styleUrls: ['./chat-text.component.scss'],
  animations: [
    trigger('bounceIn', [transition('* => *', useAnimation(bounceIn, {
      // Set the duration to 5seconds and delay to 2seconds
      params: { timing: 1 }
    }))])
  ]
})
export class ChatTextComponent implements OnInit {

  @Input() MESSAGE: MessageDTO = null;
  
  loggedUser: LoggedUser = null;
  incommingMsg: boolean = false;

  bounceIn: any;

  constructor(private userService: UserService) {
    this.loggedUser = this.userService.getLoggedUser(); 
   }

  ngOnInit() {
    this.incommingMsg = this.MESSAGE.userId != this.loggedUser.id;
  }

}

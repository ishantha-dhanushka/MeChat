import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

  chatId: any = null;

  constructor(private route: ActivatedRoute) { 
    this.route.params.subscribe( params => {
      this.chatId = params.chat_id;
    });
  }

  ngOnInit() {
  }

}
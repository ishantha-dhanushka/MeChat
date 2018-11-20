import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ChatRoutingModule } from './chat-routing.module';
import { ChatComponent } from './chat.component';
import { FriendListComponent } from './friend-list/friend-list.component';
import { ChatBoxComponent } from './chat-box/chat-box.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ChatTextComponent } from './chat-box/chat-text/chat-text.component';
import { MomentModule } from 'angular2-moment';

@NgModule({
  declarations: [ChatComponent, FriendListComponent, ChatBoxComponent, ChatTextComponent],
  imports: [
    CommonModule,
    ChatRoutingModule,
    ReactiveFormsModule,
    MomentModule
  ]
})
export class ChatModule { }

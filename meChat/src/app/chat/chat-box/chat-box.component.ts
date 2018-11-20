import { Component, OnInit, Input, OnChanges, SimpleChanges, ViewChild, ElementRef } from '@angular/core';
import { LoggedUser } from '../../_model/loggedUser';
import { UserService } from '../../_services/user.service';
import { HubService } from '../../_services/hub.service';
import { HubStatus } from '../../_enum/hub-status.enum';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageDTO } from '../../_model/message';
import { MessageService } from '../../_services/message.service';

@Component({
  selector: 'chat-box',
  templateUrl: './chat-box.component.html',
  styleUrls: ['./chat-box.component.scss']
})
export class ChatBoxComponent implements OnInit, OnChanges {
  
  @Input() CHAT_ID = null;
  @ViewChild('scrollMe') private myScrollContainer: ElementRef;
  
  loggedUser: LoggedUser = null;
  HubStatus = HubStatus;
  
  messages: MessageDTO[] = [];
  
  messageForm: FormGroup;
  
  constructor(private userService: UserService, private hubService: HubService, private messageService: MessageService) {
    this.loggedUser = this.userService.getLoggedUser(); 
  }
  
  ngOnInit() {
    // on message received
    this.hubService.hubConnection.on("message", (message: MessageDTO) => {
      this.onMessage(message);
    });
    
    this.initForm();
  }
  
  // on chat id changed
  ngOnChanges(changes: SimpleChanges) {
    if(changes.CHAT_ID && changes.CHAT_ID.currentValue){
      this.getPreviosMessages(this.CHAT_ID);
    }
  }
  
  // Scroll to bottom
  scrollToBottom(): void {
    try {
      setTimeout(() => {
        this.myScrollContainer.nativeElement.scrollTop = this.myScrollContainer.nativeElement.scrollHeight;
      });
    } catch(err) { }                 
  }
  
  // get previos messages
  loadingPreviousMessages: boolean = false;
  getPreviosMessages = (chatId: string) => {
    this.loadingPreviousMessages = true;
    this.messages = [];
    this.messageService.getPreviosMessages(chatId).subscribe((messages: MessageDTO[]) => {
      this.loadingPreviousMessages = false;
      this.messages = messages;
      this.scrollToBottom();
    }, error => {
      this.loadingPreviousMessages = false;
      console.log("Failed to load previous messages");
      console.log(error);
    });
  }
  
  // init chat form
  initForm = () => {
    this.messageForm = new FormGroup({
      chatId : new FormControl(this.CHAT_ID),
      userId : new FormControl(this.loggedUser.id),
      messageText : new FormControl(null, Validators.compose([Validators.required])),
    });
  }
  
  // send message
  sendMessage = () => {
    this.hubService.hubConnection.invoke("Send" , this.messageForm.value);
    this.messageForm.reset();
    this.initForm();
  }
  
  // on message
  onMessage = (message: MessageDTO) => {
    
    if(message.chatId == this.CHAT_ID){
      this.messages.push(message);
      this.scrollToBottom();
    }
  }
  
}
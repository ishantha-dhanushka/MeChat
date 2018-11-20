import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { environment } from '../../environments/environment';
import { MessageDTO } from '../_model/message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private http: HttpClient, private authService:AuthService) { }

  // get previous messages
  getPreviosMessages = (chatId: string): Observable<MessageDTO[]> => {
    return new Observable(obs => {
      this.http.get(environment.api.url + "/api/message/ByChatId/" + chatId, {
        headers : {
          Authorization : "Bearer " + this.authService.getToken()
        }
      }).subscribe((messages: MessageDTO[]) => {
        obs.next(messages);
        obs.complete();
      }, error => {
        obs.error(error);
        obs.complete();
      });
    });
  }
}

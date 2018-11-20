import { Injectable } from '@angular/core';
import {HubConnection, HubConnectionBuilder, IHttpConnectionOptions } from '@aspnet/signalr';
import { environment } from '../../environments/environment';
import { HubStatus } from '../_enum/hub-status.enum';
import { AuthService } from './auth.service';

@Injectable({
	providedIn: 'root'
})
export class HubService {
	
	public hubConnection:HubConnection;
	public hubStatus: string = HubStatus.DISCONNECTED;
	
	constructor(private authService: AuthService) { 
	}
	
	// init connection
	initConnection = () => {
		
		this.hubConnection = new HubConnectionBuilder()
		.withUrl(environment.api.url + "/hub", {
			accessTokenFactory: () => this.authService.getToken()
		})
		.build();
		
		this.startConnection();
		
		this.hubConnection.onclose(async () => {
			this.hubStatus = HubStatus.DISCONNECTED;
			console.log("Connection closed!");
			await this.startConnection();
		})
	}
	
	// start connection
	startConnection = async () => {
		this.hubConnection.start()
		.then((res) => {
			this.hubStatus = HubStatus.CONNECTED;
			console.log("Connection Started");
		})
		.catch(async (error) => {
			this.hubStatus = HubStatus.DISCONNECTED;
			console.log("Failed to connect to hub!");
			await this.startConnection();
		});
	}
}

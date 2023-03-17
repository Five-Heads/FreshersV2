import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subscription } from 'rxjs';
import { AuthService } from './auth/auth.service';
import { IDisposable } from './utils/disposable';
import { environment } from 'src/environment/environment';

@Injectable({ 
    providedIn: 'root' 
})
export class SignalRService implements IDisposable {
    private connection!: signalR.HubConnection;
    private apiUrl = environment.apiUrl;
    private userSubscription: Subscription;

    constructor(
        private authService: AuthService
    ) {
        this.userSubscription = authService.user.subscribe(user => {
            user ? this.initConnection() : this.closeConnection();
        });
    }

    send(eventName: string, data: any) {
        this.connection.send(eventName, data);
    }

    initConnection() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${this.apiUrl}/hubs/test`, {
                accessTokenFactory: () => this.authService.user.value!.token
            })
            .build();

        this.connection.start()
            .then(() => this.initEvents())
            .catch(console.log);
    }

    closeConnection() {
        if(this.connection){
            this.connection.stop()
                .then(console.log)
                .catch(console.log);
        }
    }

    dispose() {
        this.userSubscription.unsubscribe();
        this.closeConnection();
    }

    private initEvents() {
        this.connection.on("NewMessage", (data: any) => {
            // work with global state
        })
    }
}
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { AuthService } from './auth/auth.service';
import { IDisposable } from './utils/disposable';
import { environment } from 'src/environment/environment';



@Injectable({
    providedIn: 'root'
})
export class SignalRService implements IDisposable {
    contestData = new BehaviorSubject<ContestsUpdateResponseModel[] | null>(null);
    private connection!: signalR.HubConnection;
    private apiUrl = environment.apiUrl;

    constructor(
        private authService: AuthService
    ) {
        // this.userSubscription = authService.user.subscribe(user => {
        //     user ? this.initConnection() : this.closeConnection();
        // });
    }

    send(eventName: string, data: any) {
        this.connection.send(eventName, data);
    }

    initConnection() {
        debugger;
        
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${this.apiUrl}/hubs/VoteImage`, {
                accessTokenFactory: () => this.authService.user.value!.token,
            })
            .withAutomaticReconnect()
            .build();

        this.connection.start()
            .then(() => this.initEvents())
            .catch(console.log);
    }

    closeConnection() {
        if (this.connection) {
            this.connection.stop()
                .then(console.log)
                .catch(console.log);
        }
    }

    dispose() {
        this.closeConnection();
    }

    private initEvents() {
        this.connection.on("CheckpointReached", (userId: any) => {
            // update
            debugger;
        })

        this.connection.on("NextCheckpoint", (newNext: any) => {
            // update
            debugger;
        })
    }
}

import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { AuthService } from './auth/auth.service';
import { IDisposable } from './utils/disposable';
import { environment } from 'src/environment/environment';
import { TreasureHuntDataService } from './events/treasure-hunt/treasure-hunt-data.service';

@Injectable({
    providedIn: 'root'
})
export class SignalRService implements IDisposable {
    public connection!: signalR.HubConnection;
    private apiUrl = environment.apiUrl;

    constructor(
        private authService: AuthService,
        private treasureHuntDataService: TreasureHuntDataService
    ) {  }

    send(eventName: string, data: any) {
        this.connection.send(eventName, data);
    }

    initConnection() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${this.apiUrl}/hubs/TreasureHunt`, {
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
            this.treasureHuntDataService.setReachedBy(userId);
        })
  
        this.connection.on("NextCheckpoint", (newNext: any) => {
            // update
            debugger;
            this.treasureHuntDataService.resetReachedBy();
            this.treasureHuntDataService.setSelectedCheckpoint(newNext);
        })
    }
}

import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable, Subscription, of, BehaviorSubject } from 'rxjs';
import { AuthService } from './auth/auth.service';
import { IDisposable } from './utils/disposable';
import { environment } from 'src/environment/environment';
import { CheckpointInputModel } from './events/models/TreasureHuntStartInputModel';
import { ContestsUpdateResponseModel } from './events/models/ContestsUpdateResponseModel';



@Injectable({
    providedIn: 'root'
})
export class SignalRService implements IDisposable {
    contestData = new BehaviorSubject<ContestsUpdateResponseModel[] | null>(null);
    private connection!: signalR.HubConnection;
    private apiUrl = environment.apiUrl;
    private userSubscription: Subscription;

    constructor(
        private authService: AuthService
    ) {
        // this.userSubscription = authService.user.subscribe(user => {
        //     user ? this.initConnection() : this.closeConnection();
        // });
    }

    send(eventName: string, data: any[]) {
        console.log(this.connection);
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
        this.userSubscription.unsubscribe();
        this.closeConnection();

    }

    private initEvents() {
        console.log("Logged");
        this.connection.on("StartRound", (obj: any) => {
            console.log(obj);
            this.connection.send("SendImage", { contestId: 1, roundId: 1, imageBase64: "test" });
        })

        this.connection.on("StartVote", (obj:any) => {
            console.log(obj);
            this.connection.send("CastVote", {contestId: 1, roundId: 1, imageId: 1});
        })

        this.connection.on("EndRound", (obj:any) => {
            console.log(obj)
        })

        this.connection.on("Finish", (obj:any) => {
            console.log(obj)
        })

        this.connection.on("ContestsUpdateData", (obj: ContestsUpdateResponseModel[]) => {
            this.contestData.next(obj);
            console.log(obj);
        })
    }
}

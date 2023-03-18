import { Injectable } from '@angular/core';
import { environment } from 'src/environment/environment';
import { TreasureHuntStartInputModel } from '../../models/TreasureHuntStartInputModel';
import { HttpClient } from '@angular/common/http';
import { ValidateCheckpointModel } from '../../models/ValidateCheckpointModel';
import { take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TreasureHuntService {
    private apiUrl = environment.apiUrl;

    constructor(
        private http: HttpClient,
    ) { }

    public getMyTreasureHunt() {
        // return this.http.get<AuthResponseModel>(`${this.apiUrl}/treasureHunt/my`)
        //     .pipe(
        //         // tap(model => {
        //         //     this.handleAuthSuccess(new User('1', userName, model.token, facultyNumber));
        //         // })
        //     )
    }

    public startTreasureHunt(treasureHuntId: number) {
        return this.http.post<TreasureHuntStartInputModel>(`${this.apiUrl}/treasureHunt/start/${treasureHuntId}`, {})
            .pipe(take(1));
    }

    public validateCheckpoint(requestModel: ValidateCheckpointModel) {
        return this.http.post<boolean>(`${this.apiUrl}/treasureHunt/validate/`, requestModel)
            .pipe(take(1));
    }
}

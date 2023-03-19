import { Injectable } from '@angular/core';
import { environment } from '../../environment/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, of, take } from 'rxjs';
import { UserCreateInputModel } from '../events/models/UserCreateInputModel';
import { TreasureHuntStartInputModel } from '../events/models/TreasureHuntStartInputModel';
@Injectable({
  providedIn: 'root',
})
export class GuessTheImageService {
  private apiUrl = environment.apiUrl;
  private blurredImageContest = 'blurredImageContest';

  constructor(private http: HttpClient) {}

  getEvent() {
    return this.http.get<any>(
      `${this.apiUrl}/${this.blurredImageContest}/upcoming`
    );
  }

  postChangeStatus(data: any): Observable<boolean> {
    return this.http
      .post<boolean>(
        `${this.apiUrl}/${this.blurredImageContest}/change-status`,
        data
      )
      .pipe(take(1));
  }

  createBlurredGame(maxParticipants: number, secondsPerRound: number) {
    return this.http.post<any>(
      `${this.apiUrl}/${this.blurredImageContest}/create`,
      {
        maxParticipants: maxParticipants,
        secondsPerRound: secondsPerRound,
      }
    );
  }

  addScore(round: number) {
    return this.http.post<any>(
      `${this.apiUrl}/${this.blurredImageContest}/user-rankings`,
      {
        round: round,
      }
    );
  }
}

import { Injectable } from '@angular/core';
import { VoteImageContestData } from '../models/VoteImageContestData';
import { Observable, of, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environment/environment';

export class ContestResponseModel {
  constructor(public contest: any) { }
}

@Injectable({
  providedIn: 'root'
})
export class VotingGameService {
  private apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient
  ) { }

  getAllVotingGames(): Observable<ContestResponseModel> {
    return this.http.get<ContestResponseModel>(`${this.apiUrl}/voteImage/all`);
  }

  createVotingGame(name: string, maxParticipants: number, voteTime: number, drawTime: number, words: string[]) {
    return this.http.post<any>(`${this.apiUrl}/voteImage/createContest`, {
      name: name,
      maxParticipants: maxParticipants,
      voteTime: voteTime,
      drawTime: drawTime,
      words: words
    });
  }
}

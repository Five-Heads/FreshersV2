import { Injectable } from '@angular/core';
import {environment} from "../../environment/environment";
import {HttpClient} from "@angular/common/http";
import {Observable, of, take} from "rxjs";
import {UserCreateInputModel} from "../events/models/UserCreateInputModel";
import {TreasureHuntStartInputModel} from "../events/models/TreasureHuntStartInputModel";

@Injectable({
  providedIn: 'root'
})
export class GuessTheImageService {

  private apiUrl = environment.apiUrl;
  private blurredImageContest = 'blurredImageContest';

  constructor(
    private http: HttpClient,
  ) { }

  getEvent(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/api/${this.blurredImageContest}/upcoming`)
      .pipe(take(1));
  }

  getImage(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/api/${this.blurredImageContest}/upcoming`)
      .pipe(take(1));
  }

  postChangeStatus(data: any): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}/api/${this.blurredImageContest}/change-status`, data)
      .pipe(take(1));
  }
  //postCreateGame

  postCreateGame(data: any): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}/api/${this.blurredImageContest}/create`, data)
      .pipe(take(1));
  }
}

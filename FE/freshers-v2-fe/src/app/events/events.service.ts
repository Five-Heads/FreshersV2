import { Injectable } from '@angular/core';
import {Observable, of, take} from "rxjs";
import {TreasureHuntCreateInputModel} from "./models/TreasureHuntCreateInputModel";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environment/environment";
import {TreasureHuntAll} from "./models/TreasureHuntAll";

@Injectable({
  providedIn: 'root'
})
export class EventsService {

  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getMyTreasureHunts(): Observable<TreasureHuntAll[]> {
    return this.http.get<TreasureHuntAll[]>(`${this.apiUrl}/treasureHunt/my`)
      .pipe(take(1));
  }

  getAllTreasureHunts(): Observable<TreasureHuntAll[]> {
    return this.http.get<TreasureHuntAll[]>(`${this.apiUrl}/treasureHunt/all`)
      .pipe(take(1));
  }
}

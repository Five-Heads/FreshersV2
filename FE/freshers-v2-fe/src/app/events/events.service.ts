import { Injectable } from '@angular/core';
import {Observable, of, take} from "rxjs";
import {TreasureHuntCreateInputModel} from "./models/TreasureHuntCreateInputModel";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environment/environment";

@Injectable({
  providedIn: 'root'
})
export class EventsService {

  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getAllTreasureHunts(): Observable<TreasureHuntCreateInputModel[]> {
    // const url = '';
    // const model: TreasureHuntCreateInputModel[]= [
    //   {
    //     id: 1,
    //     name: 'Freshers',
    //   },]
    // return of(model);
    return this.http.get<TreasureHuntCreateInputModel[]>(`${this.apiUrl}/treasureHunt/my`)
      .pipe(take(1));
  }
}

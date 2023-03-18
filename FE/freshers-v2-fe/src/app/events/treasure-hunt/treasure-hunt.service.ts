import { Injectable } from '@angular/core';
import {Observable, of, take, tap} from "rxjs";

import {UserCreateInputModel} from "../models/UserCreateInputModel";
import {TreasureHuntCreateInputModel} from "../models/TreasureHuntCreateInputModel";
import {CheckpointInputModel, TreasureHuntStartInputModel} from "../models/TreasureHuntStartInputModel";
import { environment } from 'src/environment/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TreasureHuntService {
  private apiUrl = environment.apiUrl;

  constructor(
      private http: HttpClient,
  ) { }

  getAllUsers(): Observable<UserCreateInputModel[]> {
    return this.http.get<UserCreateInputModel[]>(`${this.apiUrl}/users/withoutGroup`)
      .pipe(take(1), tap(console.log));

  }

  getAllTreasureHunts(): Observable<TreasureHuntCreateInputModel[]> {
    const url = '';
    const model: TreasureHuntCreateInputModel[]= [
      {
        id: 1,
        name: 'Freshers',
      },]
    return of(model);
  }

  getTreasureHuntStart(): Observable<TreasureHuntStartInputModel> {
    const url = '';
    const model: TreasureHuntStartInputModel= {
       Id: 1,
       TotalCheckpoints: 3,
       GroupId: 1,
       GroupMembers: [1, 2],
       Next: {
         Id: 23,
         Question: 'Затъен в обръза на безличието?',
         IsFinal: false,
         AssignPerson: "Иван Иванов",
         OrderNumber: 1,
         NextReachedBy: [1, 2],
       },}
    return of(model);
  }
}

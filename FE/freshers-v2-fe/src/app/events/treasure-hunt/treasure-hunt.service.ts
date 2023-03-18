import { Injectable } from '@angular/core';
import {Observable, of, take, tap} from "rxjs";

import {UserCreateInputModel} from "../models/UserCreateInputModel";
import {TreasureHuntCreateInputModel} from "../models/TreasureHuntCreateInputModel";
import { TreasureHuntStartInputModel} from "../models/TreasureHuntStartInputModel";
import { environment } from 'src/environment/environment';
import { HttpClient } from '@angular/common/http';
import { EventCreateOutput } from '../models/EventCreateOutput';
import { ValidateCheckpointModel } from '../models/ValidateCheckpointModel';

@Injectable({
  providedIn: 'root'
})
export class TreasureHuntService {
  private apiUrl = environment.apiUrl;

  constructor(
      private http: HttpClient,
  ) { }

  createTreasureHunt(data: EventCreateOutput) {
    return this.http.post(`${this.apiUrl}/treasureHunt/create`, data)
      .pipe(take(1));
  }

  createGroup(data: any) {
    return this.http.post(`${this.apiUrl}/groups/create`, data)
      .pipe(take(1));
  }

  getAllUsers(): Observable<UserCreateInputModel[]> {
    return this.http.get<UserCreateInputModel[]>(`${this.apiUrl}/users/withoutGroup`)
      .pipe(take(1));

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

  getTreasureHuntStart(treasureHuntId: number): Observable<TreasureHuntStartInputModel> {
    return this.http.post<TreasureHuntStartInputModel>(`${this.apiUrl}/treasureHunt/start/${treasureHuntId}`, {})
      .pipe(take(1));
  }

  validate(model: ValidateCheckpointModel) {
    return this.http.post<boolean>(`${this.apiUrl}/treasureHunt/validate/`, model)
      .pipe(take(1));
  }

  // getTreasureHuntStart(trId: number): Observable<TreasureHuntStartInputModel> {
  //   const url = '';
  //   const model: TreasureHuntStartInputModel= {
  //      Id: 1,
  //      TotalCheckpoints: 3,
  //      GroupId: 1,
  //      GroupMembers: [1, 2],
  //      Next: {
  //        Id: 23,
  //        Question: 'Затъен в обръза на безличието?',
  //        IsFinal: false,
  //        AssignPerson: "Иван Иванов",
  //        OrderNumber: 1,
  //        NextReachedBy: [1, 2],
  //      },}
  //   return of(model);
  // }
}

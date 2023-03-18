import { Injectable } from '@angular/core';
import {Observable, of} from "rxjs";

import {UserCreateInputModel} from "../models/UserCreateInputModel";
import {TreasureHuntCreateInputModel} from "../models/TreasureHuntCreateInputModel";
import {CheckpointInputModel, TreasureHuntStartInputModel} from "../models/TreasureHuntStartInputModel";

@Injectable({
  providedIn: 'root'
})
export class TreasureHuntService {

  constructor() { }

  getAllUsers(): Observable<UserCreateInputModel[]> {
    const url = ``;
    // return this.httpClient.get<UserCreateInputModel[]>(url);
    const model: UserCreateInputModel[]= [
      {
        id: 1,
        name: 'Pesho',
        fn: "2MI0600001"
      },
      {
        id: 2,
        name: 'Ivan',
        fn: "2MI0600002"
      },
      {
        id: 3,
        name: 'Gosho',
        fn: "2MI0600003"
      }];
    return of(model);
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

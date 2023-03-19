import { Injectable } from '@angular/core';
import {BehaviorSubject, filter, Observable} from "rxjs";
import {TreasureHuntData} from "../models/TreasureHuntData";
import {CheckpointInputModel} from "../models/TreasureHuntStartInputModel";
import { GroupResponseModel } from '../models/GroupResponseModel';

@Injectable({
  providedIn: 'root'
})
export class TreasureHuntDataService {
  private reachedBy: BehaviorSubject<string[]>;
  private selectedTreasureHunt: BehaviorSubject<TreasureHuntData>;
  private selectedNextCheckpoint: BehaviorSubject<CheckpointInputModel>;
  private userGroup: BehaviorSubject<GroupResponseModel>;

  constructor() {
    this.selectedTreasureHunt = new BehaviorSubject<TreasureHuntData>(null as any);
    this.selectedNextCheckpoint = new BehaviorSubject<CheckpointInputModel>(null as any);
    this.reachedBy = new BehaviorSubject<string[]>([]);
    this.userGroup = new BehaviorSubject<GroupResponseModel>(null as any);
  }

  getSelectedTreasureHunt(): Observable<TreasureHuntData> {
    return this.selectedTreasureHunt.asObservable().pipe(
      filter(val => !!val)
    );
  }

  setSelectedTreasureHunt(selectedTreasureHunt: TreasureHuntData) {
    this.selectedTreasureHunt.next(selectedTreasureHunt);
  }

  getSelectedCheckpoint(): Observable<CheckpointInputModel> {
    return this.selectedNextCheckpoint.asObservable().pipe(
      filter(val => !!val)
    );
  }

  setSelectedCheckpoint(selectedCheckpoint: CheckpointInputModel) {
    this.selectedNextCheckpoint.next(selectedCheckpoint);
  }

  getReachedBy(): Observable<string[]> {
    return this.reachedBy;
  }

  setReachedBy(userId: string) {
    this.reachedBy.next([...this.reachedBy.value, userId]);
  }

  resetReachedBy() {
    this.reachedBy.next([]);
  }


  getUserGroup() : Observable<GroupResponseModel> {
    return this.userGroup;
  }

  setUserGroup(data: GroupResponseModel) {
    this.userGroup.next(data);
  }

  resetData() {
    this.selectedTreasureHunt.next(null as any);
    this.selectedNextCheckpoint.next(null as any);
  }
}

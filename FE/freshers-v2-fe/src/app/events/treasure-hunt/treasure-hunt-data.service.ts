import { Injectable } from '@angular/core';
import {BehaviorSubject, filter, Observable} from "rxjs";
import {TreasureHuntData} from "../models/TreasureHuntData";
import {CheckpointInputModel} from "../models/TreasureHuntStartInputModel";

@Injectable({
  providedIn: 'root'
})
export class TreasureHuntDataService {

  private selectedTreasureHunt: BehaviorSubject<TreasureHuntData>;
  private selectedNextCheckpoint: BehaviorSubject<CheckpointInputModel>;

  constructor() {
    this.selectedTreasureHunt = new BehaviorSubject<TreasureHuntData>(null as any);
    this.selectedNextCheckpoint = new BehaviorSubject<CheckpointInputModel>(null as any);
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

  resetData() {
    this.selectedTreasureHunt.next(null as any);
    this.selectedNextCheckpoint.next(null as any);
  }
}

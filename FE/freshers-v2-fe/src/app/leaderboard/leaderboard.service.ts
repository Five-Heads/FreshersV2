import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, of, take} from "rxjs";
import {UserLeaderboardModel} from "./models/UserLeaderboardModel";
import { environment } from 'src/environment/environment';

@Injectable({
  providedIn: 'root'
})
export class LeaderboardService {

  constructor(private http: HttpClient) { }

  private apiUrl = environment.apiUrl;
  getLeaderboard(): Observable<UserLeaderboardModel[]> {
    // const model =[
    //   {
    //     id: 1,
    //     name: "Ivan Ivanov",
    //     facultyNumber: "2mi",
    //     points: 124
    //   },
    //   {
    //     id: 2,
    //     name: "Ivan Petrov",
    //     facultyNumber: "2mi3412512",
    //     points: 120
    //   },
    //   {
    //     id: 3,
    //     name: "Ivan Jalkov",
    //     facultyNumber: "2mi412",
    //     points: 100
    //   },
    //   {
    //     id: 4,
    //     name: "Ivan Grozni",
    //     facultyNumber: "2mi",
    //     points: 56
    //   },
    //   {
    //     id: 5,
    //     name: "Ivan Stran6ni",
    //     facultyNumber: "2mi",
    //     points: 25
    //   },
    //   {
    //     id: 6,
    //     name: "Ivan",
    //     facultyNumber: "2mi",
    //     points: 12
    //   },
    //   {
    //     id: 7,
    //     name: "Ivan Velikolepni",
    //     facultyNumber: "2mi",
    //     points: 1
    //   },
    //   {
    //     id: 1,
    //     name: "Ivan Ivanov",
    //     facultyNumber: "2mi",
    //     points: 124
    //   },
    //   {
    //     id: 2,
    //     name: "Ivan Petrov",
    //     facultyNumber: "2mi3412512",
    //     points: 120
    //   },
    //   {
    //     id: 3,
    //     name: "Ivan Jalkov",
    //     facultyNumber: "2mi412",
    //     points: 100
    //   },
    //   {
    //     id: 4,
    //     name: "Ivan Grozni",
    //     facultyNumber: "2mi",
    //     points: 56
    //   },
    //   {
    //     id: 5,
    //     name: "Ivan Stran6ni",
    //     facultyNumber: "2mi",
    //     points: 25
    //   },
    //   {
    //     id: 6,
    //     name: "Ivan",
    //     facultyNumber: "2mi",
    //     points: 12
    //   },
    //   {
    //     id: 7,
    //     name: "Ivan Velikolepni",
    //     facultyNumber: "2mi",
    //     points: 1
    //   },
    // ];

    // return of(model);

    return this.http.get<UserLeaderboardModel[]>(`${this.apiUrl}/leaderboard/all`)
      .pipe(take(1));
  }
}

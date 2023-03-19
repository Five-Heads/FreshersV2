import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, of} from "rxjs";
import {UserLeaderboardModel} from "./models/UserLeaderboardModel";

@Injectable({
  providedIn: 'root'
})
export class LeaderboardService {

  constructor(http: HttpClient) { }

  getLeaderboard(): Observable<UserLeaderboardModel[]> {
    const model =[
      {
        Id: 1,
        Name: "Ivan Ivanov",
        FacultyNumber: "2mi",
        Points: 124
      },
      {
        Id: 2,
        Name: "Ivan Petrov",
        FacultyNumber: "2mi3412512",
        Points: 120
      },
      {
        Id: 3,
        Name: "Ivan Jalkov",
        FacultyNumber: "2mi412",
        Points: 100
      },
      {
        Id: 4,
        Name: "Ivan Grozni",
        FacultyNumber: "2mi",
        Points: 56
      },
      {
        Id: 5,
        Name: "Ivan Stran6ni",
        FacultyNumber: "2mi",
        Points: 25
      },
      {
        Id: 6,
        Name: "Ivan",
        FacultyNumber: "2mi",
        Points: 12
      },
      {
        Id: 7,
        Name: "Ivan Velikolepni",
        FacultyNumber: "2mi",
        Points: 1
      },
      {
        Id: 1,
        Name: "Ivan Ivanov",
        FacultyNumber: "2mi",
        Points: 124
      },
      {
        Id: 2,
        Name: "Ivan Petrov",
        FacultyNumber: "2mi3412512",
        Points: 120
      },
      {
        Id: 3,
        Name: "Ivan Jalkov",
        FacultyNumber: "2mi412",
        Points: 100
      },
      {
        Id: 4,
        Name: "Ivan Grozni",
        FacultyNumber: "2mi",
        Points: 56
      },
      {
        Id: 5,
        Name: "Ivan Stran6ni",
        FacultyNumber: "2mi",
        Points: 25
      },
      {
        Id: 6,
        Name: "Ivan",
        FacultyNumber: "2mi",
        Points: 12
      },
      {
        Id: 7,
        Name: "Ivan Velikolepni",
        FacultyNumber: "2mi",
        Points: 1
      },
    ];

    return of(model);
  }
}

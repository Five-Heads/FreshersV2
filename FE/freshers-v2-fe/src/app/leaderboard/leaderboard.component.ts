import {Component, OnDestroy, OnInit} from '@angular/core';
import {faCaretUp, faCrown } from '@fortawesome/free-solid-svg-icons';
import {LeaderboardService} from "./leaderboard.service";
import {UserLeaderboardModel} from "./models/UserLeaderboardModel";

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.scss']
})
export class LeaderboardComponent implements OnInit, OnDestroy{

  faCrown = faCrown;
  faCaretUp = faCaretUp;
  users: UserLeaderboardModel[] = [];
  constructor(
    private leaderboardService: LeaderboardService
  ) {
  }

  ngOnInit(): void {
    this.leaderboardService.getLeaderboard().subscribe(res => {
      this.users = res;
    });
  }

  ngOnDestroy(): void {
  }

}

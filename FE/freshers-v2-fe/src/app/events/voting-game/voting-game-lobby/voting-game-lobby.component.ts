import { Component } from '@angular/core';
import { VoteImageSignalRService } from 'src/app/VoteImageSignalR.service';

@Component({
  selector: 'app-voting-game-lobby',
  templateUrl: './voting-game-lobby.component.html',
  styleUrls: ['./voting-game-lobby.component.scss']
})
export class VotingGameLobbyComponent {
  constructor(
    private signalRService: VoteImageSignalRService
  ) {
  }

  ngOnInit(): void {
    this.signalRService.send("JoinContest", {});
  }
}

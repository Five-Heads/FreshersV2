import { Component } from '@angular/core';
import { SignalRService } from 'src/app/signalR.service';

@Component({
  selector: 'app-voting-game-lobby',
  templateUrl: './voting-game-lobby.component.html',
  styleUrls: ['./voting-game-lobby.component.scss']
})
export class VotingGameLobbyComponent {
  constructor(
    private signalRService: SignalRService
  ) {
  }

  ngOnInit(): void {
    this.signalRService.send("JoinContest", {});
  }
}

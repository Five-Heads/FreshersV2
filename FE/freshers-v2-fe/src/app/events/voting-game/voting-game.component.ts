import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SignalRService } from 'src/app/signalR.service';
import { ContestsUpdateResponseModel } from '../models/ContestsUpdateResponseModel';
import { VoteImageContestData } from '../models/VoteImageContestData';
import { CreateContestModalComponent } from './create-contest-modal/create-contest-modal.component';
import { VotingGameService } from './voting-game.service';

@Component({
  selector: 'app-voting-game',
  templateUrl: './voting-game.component.html',
  styleUrls: ['./voting-game.component.scss']
})

export class VotingGameComponent {
  contests?: VoteImageContestData[];
  contestData?: ContestsUpdateResponseModel[] | null;

  constructor(
    private votingGameService: VotingGameService,
    private signalRService: SignalRService,
    private modalService: NgbModal
  ) {
  }

  createContest() {
    const ref = this.modalService.open(CreateContestModalComponent, { size: "lg" });
  }

  getUserCount(id: number) {
    return this.contestData?.find(x => x.ContestId === id)?.UsersCount;
  }

  ngOnInit(): void {
    this.votingGameService.getAllVotingGames()
        .subscribe(x => this.contests = x.contest);

    this.signalRService.contestData
        .subscribe(x => this.contestData = x)
  }
}

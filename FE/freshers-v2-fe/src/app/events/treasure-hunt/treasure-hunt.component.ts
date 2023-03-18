import {Component, OnDestroy, OnInit} from '@angular/core';
import {CreateTeamModalComponent} from "./create-team-modal/create-team-modal.component";
import {EventsComponent} from "../events.component";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {TreasureHuntService} from "./treasure-hunt.service";
import {TreasureHuntDataService} from "./treasure-hunt-data.service";
import {Subscription, debounce} from "rxjs";
import {Router} from "@angular/router";


@Component({
  selector: 'app-treasure-hunt',
  templateUrl: './treasure-hunt.component.html',
  styleUrls: ['./treasure-hunt.component.scss']
})
export class TreasureHuntComponent implements OnInit, OnDestroy{

  subs: Subscription;
  constructor(
    private modalService: NgbModal,
    private treasureHuntService: TreasureHuntService,
    private treasureHuntDataService: TreasureHuntDataService,

    private router: Router
  ) {
    this.subs = new Subscription();
  }
  ngOnInit(): void {



  }

  createTeam() {
    const ref = this.modalService.open(CreateTeamModalComponent, {size:"lg"});
  }

  startEvent() {
    this.subs.add(
      this.treasureHuntService.getTreasureHuntStart(18).subscribe(res=>{
        this.treasureHuntDataService.setSelectedCheckpoint(res.next);
        this.treasureHuntDataService.setSelectedTreasureHunt(res);
debugger;
        this.router.navigate(['events/treasure-hunt/', res.next.id]);
      })
    )
  }

  continueEvent() {

  }

  ngOnDestroy() {
    this.subs.unsubscribe();
  }

}

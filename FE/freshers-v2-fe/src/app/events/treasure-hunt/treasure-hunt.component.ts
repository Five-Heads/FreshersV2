import {Component, OnDestroy, OnInit} from '@angular/core';
import {CreateTeamModalComponent} from "./create-team-modal/create-team-modal.component";
import {EventsComponent} from "../events.component";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {TreasureHuntService} from "./treasure-hunt.service";
import {TreasureHuntDataService} from "./treasure-hunt-data.service";
import {Subscription, combineLatest, debounce} from "rxjs";
import {Router} from "@angular/router";
import { AuthService } from 'src/app/auth/auth.service';


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
    private authService: AuthService,
    private router: Router
  ) {
    this.subs = new Subscription();
  }

  public userRole: string = "";

  ngOnInit(): void {
    this.subs.add(
      this.authService.user.subscribe(res => {
        this.userRole = res!.role
      })
    )
  }

  createTeam() {
    const ref = this.modalService.open(CreateTeamModalComponent, {size:"lg"});
  }

  startEvent() {
    this.subs.add(
      // TODO: fix
      combineLatest([
        this.treasureHuntService.getTreasureHuntStart(1),
        this.treasureHuntService.getMyGroup()
      ])
      .subscribe(res=>{
        this.treasureHuntDataService.setSelectedCheckpoint(res[0].next);
        this.treasureHuntDataService.setSelectedTreasureHunt(res[0]);
        this.treasureHuntDataService.setUserGroup(res[1]);
        
        this.router.navigate(['events/treasure-hunt/', res[0].next.id]);
      })
    )
  }

  continueEvent() {

  }

  ngOnDestroy() {
    this.subs.unsubscribe();
  }

}

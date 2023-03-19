import {Component, OnInit} from '@angular/core';
import {Subscription} from "rxjs";
import {EventsService} from "../../events.service";
import {TreasureHuntAll} from "../../models/TreasureHuntAll";
import {Router} from "@angular/router";
import { faArrowRight } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-treasure-hunt-all',
  templateUrl: './treasure-hunt-all.component.html',
  styleUrls: ['./treasure-hunt-all.component.scss']
})
export class TreasureHuntAllComponent implements OnInit{

  subs: Subscription;

  faArrowRight = faArrowRight;
  allTreasureHuntData: TreasureHuntAll[] = [];

  constructor(private eventService: EventsService,
              private router: Router) {
    this.subs = new Subscription();
  }
  ngOnInit(): void {
    if(true) {
      this.subs.add(
        this.eventService.getMyTreasureHunts().subscribe(res => {
          this.allTreasureHuntData = res;
        })
      );
    }else {
      this.subs.add(
        this.eventService.getAllTreasureHunts().subscribe(res => {
          this.allTreasureHuntData = res;
        })
      )
    }
  }

  getCurrent(selected: TreasureHuntAll) {
    this.router.navigate(['events/treasure-hunt/main', selected.Id]);
  }



}

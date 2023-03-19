import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from "rxjs";
import {EventsService} from "../../events.service";
import {TreasureHuntAll} from "../../models/TreasureHuntAll";
import {Router} from "@angular/router";
import { faArrowRight } from '@fortawesome/free-solid-svg-icons';
import {AuthService} from "../../../auth/auth.service";
import { CreateEventModalComponent } from '../../create-event-modal/create-event-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-treasure-hunt-all',
  templateUrl: './treasure-hunt-all.component.html',
  styleUrls: ['./treasure-hunt-all.component.scss']
})
export class TreasureHuntAllComponent implements OnInit,OnDestroy{

  subs: Subscription;

  faArrowRight = faArrowRight;
  allTreasureHuntData: TreasureHuntAll[] = [];

  constructor(private eventService: EventsService,
              private router: Router,
              private authService: AuthService,
              private modalService: NgbModal) {
    this.subs = new Subscription();
  }

  ngOnDestroy() {
        this.subs.unsubscribe();
    }

  public userRole: string = "";
  ngOnInit(): void {
    this.subs.add(
      this.authService.user.subscribe(res => {
        this.userRole = res!.role;
        if(res?.role !== 'Admin') {
          this.subs.add(
            this.eventService.getMyTreasureHunts().subscribe(res => {
              this.allTreasureHuntData = res;
              console.log(this.allTreasureHuntData);
            })
          );
        }else {
          this.subs.add(
            this.eventService.getAllTreasureHunts().subscribe(res => {
              this.allTreasureHuntData = res;
              console.log(this.allTreasureHuntData);
            })
          )
        }
      })
    )
  }
  createEvent() {
    const modalRef = this.modalService.open(CreateEventModalComponent, {size: 'lg'});
  }
  // getCurrent(selected: TreasureHuntAll) {
  //   this.router.navigate(['events/treasure-hunt/main', selected.Id]);
  // }



}

import {Component, OnDestroy, OnInit} from '@angular/core';
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {CreateEventModalComponent} from "./create-event-modal/create-event-modal.component";
import {EventsService} from "./events.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit, OnDestroy{


  subs: Subscription;
  constructor(private modalService: NgbModal,
              private eventService: EventsService) {
    this.subs =  new Subscription();
  }

  ngOnInit() {
    console.log("ins");
    this.subs.add(
      this.eventService.getAllTreasureHunts().subscribe(res => {
        console.log(res);
      })
    );
  }

  createEvent() {
    const modalRef = this.modalService.open(CreateEventModalComponent, {size: 'lg'});
  }

  ngOnDestroy() {

  }
}

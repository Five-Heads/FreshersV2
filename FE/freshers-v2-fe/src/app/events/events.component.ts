import { Component } from '@angular/core';
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {CreateEventModalComponent} from "./create-event-modal/create-event-modal.component";

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent {

  constructor(private modalService: NgbModal) {
  }
  createEvent() {
    const modalRef = this.modalService.open(CreateEventModalComponent, {size: 'lg'});
  }
}

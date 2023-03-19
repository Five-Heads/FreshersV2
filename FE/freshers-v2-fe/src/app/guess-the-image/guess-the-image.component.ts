import { Component } from '@angular/core';
import {Subscription} from "rxjs";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {ChangeGameStatusModalComponent} from "./change-game-status-modal/change-game-status-modal.component";
import {CreateGameModalComponent} from "./create-game-modal/create-game-modal.component";
import {GuessTheImageService} from "./guess-the-image.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-guess-the-image',
  templateUrl: './guess-the-image.component.html',
  styleUrls: ['./guess-the-image.component.scss']
})
export class GuessTheImageComponent {

  subs: Subscription;

  constructor(private modalService: NgbModal,
              private guessTheImage: GuessTheImageService,
              private router: Router) {
    this.subs = new Subscription();

  }

  changeGameStatus() {
    const modalRef = this.modalService.open(ChangeGameStatusModalComponent, {size: "lg"});
  }

  createGame() {
    const modalRef = this.modalService.open(CreateGameModalComponent, {size: "lg"});
  }

  joinTheGame() {
    this.guessTheImage.getEvent().subscribe(res => {
      this.router.navigate(['blurry-vision/', 1]);
    })
  }

}


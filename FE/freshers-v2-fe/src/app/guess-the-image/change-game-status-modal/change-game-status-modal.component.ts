import {Component, OnDestroy, OnInit} from '@angular/core';
import {NgbActiveModal, NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {GuessTheImageService} from "../guess-the-image.service";

@Component({
  selector: 'app-change-game-status-modal',
  templateUrl: './change-game-status-modal.component.html',
  styleUrls: ['./change-game-status-modal.component.scss']
})
export class ChangeGameStatusModalComponent implements OnInit{

    // Upcoming = 1,
    // InProgress = 2,
    // Completed = 3,

  readonly statusList = [
    {
      id: 1,
      name: 'Upcoming'
    },
    {
      id: 2,
      name: 'InProgress'
    },
    {
      id: 3,
      name: 'Completed'
    },];
  formGroup: FormGroup;
  constructor(private activeModal: NgbActiveModal,
              private guessTheImageService: GuessTheImageService) {
  }

  ngOnInit() {
    this.formGroup = new FormGroup({
      Status: new FormControl({
        value: null,
        disabled: false
      }, [Validators.required])
    });
  }

  closeModal() {
    this.activeModal.close();
  }


  submitForm() {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid) {
      const data = this.formGroup.getRawValue();
      this.guessTheImageService.postChangeStatus(data).subscribe(res => {
        //TODO connect to BE
      });
    }
  }
}

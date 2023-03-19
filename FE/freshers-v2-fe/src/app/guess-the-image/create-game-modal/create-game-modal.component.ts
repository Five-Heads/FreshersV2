import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {NgbActiveModal} from "@ng-bootstrap/ng-bootstrap";
import {GuessTheImageService} from "../guess-the-image.service";
import {faCamera} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-create-game-modal',
  templateUrl: './create-game-modal.component.html',
  styleUrls: ['./create-game-modal.component.scss']
})
export class CreateGameModalComponent implements OnInit{
  faCamera = faCamera;

  formGroup: FormGroup = new FormGroup({
    Name: new FormControl({
      value: null,
      disabled: false
    }, [Validators.required]),
    MaxParticipants: new FormControl({
      value: 0,
      disabled: false
    }, [Validators.required, Validators.min(1)]),
    SecondsPerRound: new FormControl({
      value: 0,
      disabled: false
    }, [Validators.required, Validators.min(1)]),
  });

  constructor(private activeModal: NgbActiveModal,
              private guessTheImageService: GuessTheImageService) {
  }

  ngOnInit() {
    this.formGroup = 
  }

  closeModal() {
    this.activeModal.close();
  }


  submitForm() {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid) {
      const data = this.formGroup.getRawValue();
      this.guessTheImageService.postCreateGame(data).subscribe(res => {
        //TODO connect to BE
      });
    }
  }
}

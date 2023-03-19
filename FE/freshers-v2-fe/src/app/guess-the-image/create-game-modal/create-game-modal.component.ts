import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GuessTheImageService } from '../guess-the-image.service';
import { faCamera } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-create-game-modal',
  templateUrl: './create-game-modal.component.html',
  styleUrls: ['./create-game-modal.component.scss'],
})
export class CreateGameModalComponent implements OnInit {
  faCamera = faCamera;

  formGroup: FormGroup = new FormGroup({
    name: new FormControl(
      {
        value: null,
        disabled: false,
      },
      [Validators.required]
    ),
    maxParticipants: new FormControl(
      {
        value: 0,
        disabled: false,
      },
      [Validators.required, Validators.min(1)]
    ),
    secondsPerRound: new FormControl(
      {
        value: 0,
        disabled: false,
      },
      [Validators.required, Validators.min(1)]
    ),
  });

  constructor(
    private activeModal: NgbActiveModal,
    private guessTheImageService: GuessTheImageService
  ) {}

  ngOnInit() {}

  closeModal() {
    this.activeModal.close();
  }

  submitForm() {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid) {
      const data = this.formGroup.getRawValue();

      this.guessTheImageService
        .createBlurredGame(data.maxParticipants, data.secondsPerRound)
        .subscribe({
          next: () => {
            this.closeModal();
            window.location.reload();
          },
          error: (error) => {
            console.log(error);
          },
        });
    }
  }
}

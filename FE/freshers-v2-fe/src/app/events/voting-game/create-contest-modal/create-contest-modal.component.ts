import { Component } from '@angular/core';
import { VotingGameService } from '../voting-game.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-contest-modal',
  templateUrl: './create-contest-modal.component.html',
  styleUrls: ['./create-contest-modal.component.scss']
})
export class CreateContestModalComponent {
  formGroup: FormGroup = new FormGroup({
    name : new FormControl({
      value: null,
      disabled: false
    }, [Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
    maxParticipants : new FormControl({
      value: null,
      disabled: false
    }, [Validators.required]),
    voteTime : new FormControl({
      value: null,
      disabled: false
    }, [Validators.required]),
    drawTime : new FormControl({
      value: null,
      disabled: false
    }, [Validators.required]),
    words : new FormControl({
      value: null,
      disabled: false
    }, [Validators.required])
  });

  constructor(
    private activeModal: NgbActiveModal,
    private votingGameService: VotingGameService) {
  }

  closeModal() {
    this.activeModal.close();
  }

  submitForm() {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid) {
      const data = this.formGroup.getRawValue();

      this.votingGameService.createVotingGame(data.name, data.maxParticipants, data.voteTime, data.drawTime, data.words.split(/\r?\n/))
        .subscribe({
          next: () => {
            this.closeModal();
            window.location.reload();
          },
          error: error => {
            console.log(error);
          }
        });
    }
  }
}

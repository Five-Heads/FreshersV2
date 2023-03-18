import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CheckpointCreateOutput} from "../../models/CheckpointCreateOutput";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {NgbActiveModal} from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: 'app-create-checkpoint-modal',
  templateUrl: './create-checkpoint-modal.component.html',
  styleUrls: ['./create-checkpoint-modal.component.scss']
})
export class CreateCheckpointModalComponent implements OnInit{

  @Output() reloadData = new EventEmitter<CheckpointCreateOutput>();

  constructor(private activeModal: NgbActiveModal) {
  }
  formGroup: FormGroup;

  ngOnInit(): void {
    this.formGroup = new FormGroup({
      Name: new FormControl({
        value: null,
        disabled: false
      }, [Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
      Question: new FormControl({
        value: null,
        disabled: false
      }, [Validators.required]),
      OrderNumber: new FormControl({
        value: 0,
        disabled: false
      }, [Validators.required, Validators.min(1), Validators.max(10000)]),
      AssignPerson: new FormControl({
        value: null,
        disabled: false
      }, [Validators.required]),
    })
  }

  submitForm() {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid) {
      const data = this.formGroup.getRawValue();
      this.reloadData.emit(data);
      this.closeModal();
    }
  }

  closeModal() {
    this.activeModal.close();
  }
}

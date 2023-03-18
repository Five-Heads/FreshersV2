import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Subscription} from "rxjs";
import {UserCreateInputModel} from "../models/UserCreateInputModel";
import {
  NgbActiveModal,
  NgbDateAdapter,
  NgbDateParserFormatter,
  NgbDatepickerI18n,
  NgbDateStruct, NgbModal, NgbTimeStruct
} from "@ng-bootstrap/ng-bootstrap";
import {TreasureHuntService} from "../treasure-hunt/treasure-hunt.service";
import {DatePipe} from "@angular/common";
import {CustomAdapter, CustomDateParserFormatter, CustomDatepickerI18n} from "../datapicker.service";
import {faCalendar} from "@fortawesome/free-solid-svg-icons";
import {CreateCheckpointModalComponent} from "./create-checkpoint-modal/create-checkpoint-modal.component";
import {CheckpointCreateOutput} from "../models/CheckpointCreateOutput";

@Component({
  selector: 'app-create-event-modal',
  templateUrl: './create-event-modal.component.html',
  styleUrls: ['./create-event-modal.component.scss'],

})
export class CreateEventModalComponent implements OnInit, OnDestroy{

  formGroup: FormGroup;
  faCalendar = faCalendar;
  crrCheckpointsForEvent: CheckpointCreateOutput[] = [];

  // selectedDate: any;
  // selectedTime: any;
  subs: Subscription;
  selectedDate: NgbDateStruct;

  startDate: NgbDateStruct;
  endDate: NgbDateStruct;

  constructor(private activeModal: NgbActiveModal,
              private treasureHuntService: TreasureHuntService,
              private modalService: NgbModal) {
    this.subs = new Subscription();
  }

  ngOnInit(): void {
    this.formGroup = new FormGroup({
      Name: new FormControl({
        value: null,
        disabled: false
      }, [Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
      StartTimeDate: new FormControl({
        value: null,
        disabled: false
      }, [Validators.required]),
      StartTimeHour: new FormControl({
        value: null,
        disabled: false
      },),
      EndTimeDate: new FormControl({
        value: null,
        disabled: false
      }, [Validators.required]),
      EndTimeHour: new FormControl({
        value: null,
        disabled: false
      },),
      CheckpointsIds: new FormControl({
        value: null,
        disabled: false
      }, [Validators.required]),
    })
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  closeModal() {
    this.activeModal.close();
  }

  submitForm() {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid) {
      const data = this.formGroup.getRawValue();

      data['Checkpoints'] = this.crrCheckpointsForEvent;
      //data['StartTime'] =(this.startDate.day<10 ? '0':'')+this.startDate.day +'-' +(this.startDate.month<10 ? '0':'') + this.startDate.month
      //    + '-' + this.startDate.year + 'T' + (!!data['StartTimeHour'] ? data['StartTimeHour']: '00:00') + ":00";
      //data['EndTime'] = (this.endDate.day<10 ? '0':'')+this.endDate.day +'-' +(this.endDate.month<10 ? '0':'') + this.endDate.month+'-' + this.endDate.year + 'T' + (!!data['EndTimeHour'] ? data['EndTimeHour']: '00:00')  + ":00";
      data['StartTime'] = new Date().toISOString();
      data['EndTime'] = new Date().toISOString();

      console.log(data);
      console.log(this.startDate);
      console.log(this.endDate);
      //TODO connwct
      this.treasureHuntService.createTreasureHunt(data).subscribe();
    }
  }
  addCheckpoint() {
    const modalRef = this.modalService.open(CreateCheckpointModalComponent, {size: "lg"})
    modalRef.componentInstance.reloadData.subscribe((res: CheckpointCreateOutput)=> {
      this.crrCheckpointsForEvent.push(res);
      this.crrCheckpointsForEvent = [...this.crrCheckpointsForEvent];
    });
  }

}

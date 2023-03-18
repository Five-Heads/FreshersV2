import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import {NgbActiveModal} from "@ng-bootstrap/ng-bootstrap";
import {TreasureHuntService} from "../treasure-hunt.service";
import {combineLatest, Subscription} from "rxjs";
import {UserCreateInputModel} from "../../models/UserCreateInputModel";
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-create-team-modal',
  templateUrl: './create-team-modal.component.html',
  styleUrls: ['./create-team-modal.component.scss']
})
export class CreateTeamModalComponent implements OnInit, OnDestroy {

  // @Input() id: number;

  formGroup: FormGroup;
  selectedUserIds: number[];
  subs: Subscription;
  users: UserCreateInputModel[];

  constructor(private activeModal: NgbActiveModal,
              private treasureHuntService: TreasureHuntService) {
    this.subs = new Subscription();
  }

  ngOnInit(): void {
    this.subs = combineLatest([
      this.treasureHuntService.getAllUsers(),
      this.treasureHuntService.getAllTreasureHunts()
    ]).subscribe(([
                    users,
                    treasureHunts
                  ]) => {
      this.users = users;
    })

    this.formGroup = new FormGroup({
      name: new FormControl({
        value: null,
        disabled: false
      }, [Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
      userIds: new FormControl({
        value: null,
        disabled: false
      }, [Validators.required]),
      treasureHuntId: new FormControl({
        value: 20, // TODO: fix
        disabled: false
      }, [Validators.required]),
    })
  }

  closeModal() {
    this.activeModal.close();
  }

  submitForm() {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid) {
      const data = this.formGroup.getRawValue();
      console.log(data);
      //TODO create a team with members
      this.treasureHuntService.createGroup(data).subscribe();
      }

  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

}

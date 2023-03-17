import {Component, OnInit} from '@angular/core';
import { faUser, faLock } from '@fortawesome/free-solid-svg-icons';
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{
  faUser = faUser;
  faLock = faLock;

  formGroup: FormGroup;
  constructor() {
  }

  ngOnInit(): void {
    this.formGroup = new FormGroup({
      username : new FormControl({
        value: null,
        disabled: false
      },[Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
      password : new FormControl({
        value: null,
        disabled: false
      },[Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
    })
  }

  submitForm() {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid) {
      const data = this.formGroup.getRawValue();
      console.log(data);
      //TODO connect to login service
    }
  }


}

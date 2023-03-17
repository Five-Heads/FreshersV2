import {Component, OnInit} from '@angular/core';
import {faEnvelope, faGraduationCap, faLock, faUser} from '@fortawesome/free-solid-svg-icons';
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit{
    faUser = faUser;
    faLock = faLock;
    faGraduationCap = faGraduationCap;
    faEnvelope = faEnvelope;
    formGroup: FormGroup;

  ngOnInit(): void {
    this.formGroup = new FormGroup({
      username : new FormControl({
        value: null,
        disabled: false
      },[Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
      fn : new FormControl({
        value: null,
        disabled: false
      },[Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
      email : new FormControl({
        value: null,
        disabled: false
      },),
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
      //TODO connect to register service
    }
  }
}

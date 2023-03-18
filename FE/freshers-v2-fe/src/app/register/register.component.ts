import { Component, OnInit } from '@angular/core';
import { faEnvelope, faGraduationCap, faLock, faUser } from '@fortawesome/free-solid-svg-icons';
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

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
  
  formGroup: FormGroup = new FormGroup({
    username : new FormControl({
      value: null,
      disabled: false
    }, [Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
    fn : new FormControl({
      value: null,
      disabled: false
    }, [Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
    email : new FormControl({
      value: null,
      disabled: false
    }),
    password : new FormControl({
      value: null,
      disabled: false
    }, [Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
  });

  constructor(
    private authService: AuthService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
  }

  submitForm() {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid) {
      const data = this.formGroup.getRawValue();

      this.authService.register(data.username, data.fn, data.email, data.password)
        .subscribe({
          next: () => {
            this.router.navigate(['/']);
          },
          error: error => {
            console.log(error);
          }
        });
    }
  }
}

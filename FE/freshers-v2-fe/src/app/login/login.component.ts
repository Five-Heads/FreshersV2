import { Component, OnInit } from '@angular/core';
import { faUser, faLock } from '@fortawesome/free-solid-svg-icons';
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { AuthService } from '../auth/auth.service';
import { first } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{
  faUser = faUser;
  faLock = faLock;

  formGroup: FormGroup = new FormGroup({
    username : new FormControl({
      value: null,
      disabled: false
    }, [Validators.required, Validators.minLength(3), Validators.maxLength(30)]),
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

  submitForm(): void {
    this.formGroup.markAllAsTouched();
    
    if (this.formGroup.valid) {
      const data = this.formGroup.getRawValue();
      
      this.authService.login(data.username, data.password)
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

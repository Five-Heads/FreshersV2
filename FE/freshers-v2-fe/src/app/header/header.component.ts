import { Component, Input } from '@angular/core';
import { User } from '../auth/models/user.model';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  @Input() user?: User | null;

  constructor(
    private authService: AuthService,
  ) {
  }

  logOut() {
    this.authService.logOut();
  }
}

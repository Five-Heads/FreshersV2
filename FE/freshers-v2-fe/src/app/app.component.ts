import { Component } from '@angular/core';
import { AuthService } from './auth/auth.service';
import { SignalRService } from './signalR.service';
import { User } from './auth/models/user.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  user?: User | null;

  constructor(
    private authService: AuthService,
    private signalRService: SignalRService
  ) {
    this.authService.user.subscribe(user => this.user = user)
  }

  title = 'freshers-v2-fe';

  ngOnInit() {
    this.authService.checkIsUserAuthenticatedOnStart();
  }
}

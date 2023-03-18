import { Component } from '@angular/core';
import { AuthService } from './auth/auth.service';
import { SignalRService } from './signalR.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(
    private authService: AuthService,
    private signalRService: SignalRService
  ) {

  }

  title = 'freshers-v2-fe';

  ngOnInit() {
    this.authService.login("a", "a").subscribe((data)=>{
      this.signalRService.initConnection();
    })

  }
}

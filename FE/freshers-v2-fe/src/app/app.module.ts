import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HeaderComponent } from './header/header.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CommonModule, LocationStrategy, PathLocationStrategy } from "@angular/common";
import { RouterModule } from "@angular/router";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { EventsComponent } from './events/events.component';
import { TreasureHuntComponent } from "./events/treasure-hunt/treasure-hunt.component";
import { CreateTeamModalComponent } from './events/treasure-hunt/create-team-modal/create-team-modal.component';
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { NgSelectModule } from '@ng-select/ng-select';
import { CheckpointComponent } from './events/treasure-hunt/checkpoint/checkpoint.component';
import { NgxScannerQrcodeModule } from "ngx-scanner-qrcode";
import { SafePipe } from './events/treasure-hunt/checkpoint/safe.pipe';
import { FooterComponent } from './footer/footer.component';
import { AuthInterceptorService } from './auth/auth-interceptor.service';
import { CreateEventModalComponent } from './events/create-event-modal/create-event-modal.component';
import {NgxMaterialTimepickerModule} from 'ngx-material-timepicker';
import { CreateCheckpointModalComponent } from './events/create-event-modal/create-checkpoint-modal/create-checkpoint-modal.component';
import { GuessTheImageComponent } from './guess-the-image/guess-the-image.component';
import { ChangeGameStatusModalComponent } from './guess-the-image/change-game-status-modal/change-game-status-modal.component';
import { CreateGameModalComponent } from './guess-the-image/create-game-modal/create-game-modal.component';
import { VotingGameComponent } from './events/voting-game/voting-game.component';
import { CreateContestModalComponent } from './events/voting-game/create-contest-modal/create-contest-modal.component';
import { VotingGameLobbyComponent } from './events/voting-game/voting-game-lobby/voting-game-lobby.component';
import { BlurredImageComponent } from './guess-the-image/blurreder-image/blurred-image.component';
import { LeaderboardComponent } from './leaderboard/leaderboard.component';
import { TreasureHuntAllComponent } from './events/treasure-hunt/treasure-hunt-all/treasure-hunt-all.component';
import { MiniGamesComponent } from './mini-games/mini-games.component';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginComponent,
    RegisterComponent,
    TreasureHuntComponent,
    SafePipe,
    EventsComponent,
    CreateTeamModalComponent,
    CheckpointComponent,
    FooterComponent,
    CreateEventModalComponent,
    CreateCheckpointModalComponent,
    VotingGameComponent,
    CreateContestModalComponent,
    VotingGameLobbyComponent,
    GuessTheImageComponent,
    ChangeGameStatusModalComponent,
    CreateGameModalComponent,
    BlurredImageComponent,
    LeaderboardComponent,
    TreasureHuntAllComponent,
    MiniGamesComponent,
    HomeComponent,
    ]
  ,
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    FontAwesomeModule,
    NgxScannerQrcodeModule,
    NgbModule,
    CommonModule,
    NgSelectModule,
    NgxMaterialTimepickerModule,
    NgbModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    },
    {
      provide: LocationStrategy,
      useClass: PathLocationStrategy
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

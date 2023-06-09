import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { JwtModule } from '@auth0/angular-jwt';
import { EventsComponent } from './events/events.component';
import { TreasureHuntComponent } from './events/treasure-hunt/treasure-hunt.component';
import { CheckpointComponent } from './events/treasure-hunt/checkpoint/checkpoint.component';
import { GuessTheImageComponent } from './guess-the-image/guess-the-image.component';
import { VotingGameComponent } from './events/voting-game/voting-game.component';
import { VotingGameLobbyComponent } from './events/voting-game/voting-game-lobby/voting-game-lobby.component';
import {BlurredImageComponent} from "./guess-the-image/blurreder-image/blurred-image.component";
import {LeaderboardComponent} from "./leaderboard/leaderboard.component";
import {TreasureHuntAllComponent} from "./events/treasure-hunt/treasure-hunt-all/treasure-hunt-all.component";
import {MiniGamesComponent} from "./mini-games/mini-games.component";
import { HomeComponent } from './home/home.component';
const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: 'events',
    component: EventsComponent,
  },
  {
    path: 'events/treasure-hunt/main/:id',
    component: TreasureHuntComponent,
  },
  {
    path: 'events/treasure-hunt/all',
    component: TreasureHuntAllComponent,
  },
  {
    path: 'events/treasure-hunt/:id',
    component: CheckpointComponent,
  },
  {
    path: 'events/voting-game',
    component: VotingGameComponent,
  },
  {
    path: 'events/voting-game/:id',
    component: VotingGameLobbyComponent,
  },
  {
    path: 'blurry-vision',
    component: GuessTheImageComponent,
  },
  {
    path: 'blurry-vision/:id',
    component: BlurredImageComponent,
  },
  {
    path: 'leaderboard',
    component: LeaderboardComponent,
  },
  {
    path: 'mini-games',
    component: MiniGamesComponent,
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      preloadingStrategy: PreloadAllModules,
      useHash: true,
    }),
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          const data = JSON.parse(localStorage.getItem('userData') || '{}');
          return data.token;
        },
      },
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { JwtModule } from '@auth0/angular-jwt';
import { EventsComponent } from "./events/events.component";
import { TreasureHuntComponent } from "./events/treasure-hunt/treasure-hunt.component";
import { CheckpointComponent } from "./events/treasure-hunt/checkpoint/checkpoint.component";

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'events',
    component: EventsComponent,
  },
  {
    path: 'events/treasure-hunt',
    component: TreasureHuntComponent,
  },
  {
    path: 'events/treasure-hunt/:id',
    component: CheckpointComponent,
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          const data = JSON.parse(localStorage.getItem("userData") || '{}');
          return data.token;
        }
      },
    }),
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }

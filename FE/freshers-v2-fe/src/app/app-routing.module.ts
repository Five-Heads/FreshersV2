import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { TreasureHuntComponent } from "./treasure-hunt/treasure-hunt.component";
import { QrReaderComponent } from "./treasure-hunt/qr-reader/qr-reader.component";
import { JwtModule } from '@auth0/angular-jwt';

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
    path: 'treasure-hunt',
    component: QrReaderComponent
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

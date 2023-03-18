import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HeaderComponent } from './header/header.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
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
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './header/header.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import {FormGroup, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {RouterModule} from "@angular/router";
import { TreasureHuntComponent } from './treasure-hunt/treasure-hunt.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { QrReaderComponent } from './treasure-hunt/qr-reader/qr-reader.component';
import {NgxScannerQrcodeModule} from "ngx-scanner-qrcode";
import { SafePipe } from './treasure-hunt/qr-reader/safe.pipe';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    LoginComponent,
    RegisterComponent,
    TreasureHuntComponent,
    QrReaderComponent,
    SafePipe,
      ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    FontAwesomeModule,
    NgxScannerQrcodeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

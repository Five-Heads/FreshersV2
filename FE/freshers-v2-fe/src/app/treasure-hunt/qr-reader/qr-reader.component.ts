import { Component } from '@angular/core';
import { NgxScannerQrcodeService } from 'ngx-scanner-qrcode';
import {ScannerQRCodeSelectedFiles} from "ngx-scanner-qrcode/lib/ngx-scanner-qrcode.options";
import {Observable} from "rxjs";

@Component({
  selector: 'app-qr-reader',
  templateUrl: './qr-reader.component.html',
  styleUrls: ['./qr-reader.component.scss']
})
export class QrReaderComponent {
   config: any = {
    isAuto: true,
    text: { font: '25px serif' }, // Hiden { font: '0px' },
    frame: { lineWidth: 8 },
    medias: {
      audio: false,
      video: {
        facingMode: 'environment', // To require the rear camera https://developer.mozilla.org/en-US/docs/Web/API/MediaDevices/getUserMedia
        width: { ideal: 1280 },
        height: { ideal: 720 }
      }
    }
  };

   currentUrl: string = '';
  public selectedFiles: ScannerQRCodeSelectedFiles[] = [];

  constructor(private qrcode: NgxScannerQrcodeService) { }

  public onError(e: any): void {
    alert(e);
  }

  public handle(action: any, fn: string): void {
    console.log(fn);
    const result = action[fn]();

    if (result instanceof Observable) {
      result.subscribe(res =>{
        console.log(res);
      });
    } else if (typeof result === 'function') {
      result();
    }
  }

  showData(data: any) {
    if(data.data._value.length > 0) {
      this.currentUrl = data.data._value[0].value;
    }
  }

  public onSelects(files: any) {
    console.log("enemy");
    this.qrcode.loadFilesToScan(files, this.config).subscribe(res => {
      this.selectedFiles = res.filter(f => f.url);
      console.log(this.selectedFiles);
      console.log(res); // v1.0.25 Fixed issue https://stackoverflow.com/questions/74245551/ngx-scanner-qrcode-reading-data-in-ts
    });
  }
}

import {Component, OnDestroy, OnInit} from '@angular/core';
import {TreasureHuntService} from "../treasure-hunt.service";
import {Observable, Subscription} from "rxjs";
import {CheckpointInputModel, TreasureHuntStartInputModel} from "../../models/TreasureHuntStartInputModel";
import {ScannerQRCodeSelectedFiles} from "ngx-scanner-qrcode/lib/ngx-scanner-qrcode.options";
import {NgxScannerQrcodeService} from "ngx-scanner-qrcode";
import {faCamera, faFlagCheckered, faQrcode, faStar} from '@fortawesome/free-solid-svg-icons';
import {TreasureHuntDataService} from "../treasure-hunt-data.service";
import {TreasureHuntData} from "../../models/TreasureHuntData";

@Component({
  selector: 'app-checkpoint',
  templateUrl: './checkpoint.component.html',
  styleUrls: ['./checkpoint.component.scss']
})
export class CheckpointComponent implements OnInit, OnDestroy {

  faCamera = faCamera;
  faQrcode = faQrcode;
  faStar= faStar;
  faFlagCheckered= faFlagCheckered;

  status: string = 'Pending';

  config: any = {
    isAuto: true,
    resize: false,
    text: {font: '25px serif'}, // Hiden { font: '0px' },
    frame: {lineWidth: 8},
    isBeep: false,
    medias: {
      audio: false,
      video: {
        facingMode: 'environment', // To require the rear camera https://developer.mozilla.org/en-US/docs/Web/API/MediaDevices/getUserMedia
        width: {ideal: 1280},
        height: {ideal: 720}
      }
    }
  };

  currentUrl: string = '';
  public selectedFiles: ScannerQRCodeSelectedFiles[] = [];

  selectedCheckPoint: CheckpointInputModel;
  selectedTreasureHust: TreasureHuntData;
  isCameraOn: boolean = false;
  subs: Subscription;

  constructor(private qrcode: NgxScannerQrcodeService,
              private treasureHuntDataService: TreasureHuntDataService) {
    this.subs = new Subscription();
  }

  ngOnInit(): void {
    this.subs.add(
      this.treasureHuntDataService.getSelectedCheckpoint().subscribe(res => {
        this.selectedCheckPoint = res;
        console.log(res);
      })
    )
    this.subs.add(
      this.treasureHuntDataService.getSelectedTreasureHunt().subscribe(res => {
        this.selectedTreasureHust = res;
        console.log(res);
      })
    )
  }

  public onError(e: any): void {
    alert(e);
  }

  public handle(action: any, fn: string): void {
    console.log(fn);
    const result = action[fn]();

    if (result instanceof Observable) {
      result.subscribe(res => {
        console.log(res);
      });
    } else if (typeof result === 'function') {
      result();
    }
  }

  showData(data: any) {
    if (data.data._value.length > 0) {
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

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

}


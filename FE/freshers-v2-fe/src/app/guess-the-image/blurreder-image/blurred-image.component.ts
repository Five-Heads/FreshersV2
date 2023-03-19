import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-blurreder-image',
  templateUrl: './blurred-image.component.html',
  styleUrls: ['./blurred-image.component.scss']
})
export class BlurredImageComponent implements OnInit{
  countdown: number = 10;
  ngOnInit(): void {
    setInterval(() => {
      this.countdown--;
      if(this.countdown > 0) {
        this.changeImage();
      }
    }, 1000);
  }

  changeImage() {

  }

}

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BlurredImageComponent } from './blurred-image.component';

describe('BlurrederImageComponent', () => {
  let component: BlurredImageComponent;
  let fixture: ComponentFixture<BlurredImageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BlurredImageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BlurredImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

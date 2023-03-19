import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuessTheImageComponent } from './guess-the-image.component';

describe('GuessTheImageComponent', () => {
  let component: GuessTheImageComponent;
  let fixture: ComponentFixture<GuessTheImageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GuessTheImageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GuessTheImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

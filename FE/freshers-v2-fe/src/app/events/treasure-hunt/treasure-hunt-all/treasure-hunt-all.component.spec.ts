import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TreasureHuntAllComponent } from './treasure-hunt-all.component';

describe('TreasureHuntAllComponent', () => {
  let component: TreasureHuntAllComponent;
  let fixture: ComponentFixture<TreasureHuntAllComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TreasureHuntAllComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TreasureHuntAllComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

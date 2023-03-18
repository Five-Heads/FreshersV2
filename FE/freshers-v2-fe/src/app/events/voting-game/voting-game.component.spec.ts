import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VotingGameComponent } from './voting-game.component';

describe('VotingGameComponent', () => {
  let component: VotingGameComponent;
  let fixture: ComponentFixture<VotingGameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VotingGameComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VotingGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

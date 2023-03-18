import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VotingGameLobbyComponent } from './voting-game-lobby.component';

describe('VotingGameLobbyComponent', () => {
  let component: VotingGameLobbyComponent;
  let fixture: ComponentFixture<VotingGameLobbyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VotingGameLobbyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VotingGameLobbyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

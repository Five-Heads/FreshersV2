import { TestBed } from '@angular/core/testing';

import { VotingGameService } from './voting-game.service';

describe('VotingGameService', () => {
  let service: VotingGameService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VotingGameService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

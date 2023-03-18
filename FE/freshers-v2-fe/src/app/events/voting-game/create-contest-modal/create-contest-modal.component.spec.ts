import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateContestModalComponent } from './create-contest-modal.component';

describe('CreateContestModalComponent', () => {
  let component: CreateContestModalComponent;
  let fixture: ComponentFixture<CreateContestModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateContestModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateContestModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

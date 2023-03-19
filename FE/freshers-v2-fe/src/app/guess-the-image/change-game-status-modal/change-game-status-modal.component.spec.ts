import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeGameStatusModalComponent } from './change-game-status-modal.component';

describe('ChangeGameStatusModalComponent', () => {
  let component: ChangeGameStatusModalComponent;
  let fixture: ComponentFixture<ChangeGameStatusModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChangeGameStatusModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChangeGameStatusModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

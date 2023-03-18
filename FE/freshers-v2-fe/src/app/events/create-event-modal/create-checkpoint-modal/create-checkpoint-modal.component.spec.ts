import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateCheckpointModalComponent } from './create-checkpoint-modal.component';

describe('CreateCheckpointModalComponent', () => {
  let component: CreateCheckpointModalComponent;
  let fixture: ComponentFixture<CreateCheckpointModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateCheckpointModalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateCheckpointModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

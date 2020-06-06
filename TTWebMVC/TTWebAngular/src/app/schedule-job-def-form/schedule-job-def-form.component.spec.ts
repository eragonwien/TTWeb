import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleJobDefFormComponent } from './schedule-job-def-form.component';

describe('ScheduleJobDefFormComponent', () => {
  let component: ScheduleJobDefFormComponent;
  let fixture: ComponentFixture<ScheduleJobDefFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ScheduleJobDefFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScheduleJobDefFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

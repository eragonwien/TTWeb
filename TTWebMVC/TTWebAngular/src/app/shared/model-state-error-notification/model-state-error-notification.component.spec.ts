import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModelStateErrorNotificationComponent } from './model-state-error-notification.component';

describe('ModelStateErrorNotificationComponent', () => {
  let component: ModelStateErrorNotificationComponent;
  let fixture: ComponentFixture<ModelStateErrorNotificationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModelStateErrorNotificationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModelStateErrorNotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

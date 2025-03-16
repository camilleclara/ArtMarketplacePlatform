import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardDeliveryComponent } from './dashboard-delivery.component';

describe('DashboardDeliveryComponent', () => {
  let component: DashboardDeliveryComponent;
  let fixture: ComponentFixture<DashboardDeliveryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DashboardDeliveryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DashboardDeliveryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

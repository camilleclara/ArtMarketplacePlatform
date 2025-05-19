import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardArtisanComponent } from './dashboard-artisan.component';

describe('DashboardComponent', () => {
  let component: DashboardArtisanComponent;
  let fixture: ComponentFixture<DashboardArtisanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DashboardArtisanComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DashboardArtisanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

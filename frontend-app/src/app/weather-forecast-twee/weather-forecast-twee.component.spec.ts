import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeatherForecastTweeComponent } from './weather-forecast-twee.component';

describe('WeatherForecastTweeComponent', () => {
  let component: WeatherForecastTweeComponent;
  let fixture: ComponentFixture<WeatherForecastTweeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WeatherForecastTweeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WeatherForecastTweeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

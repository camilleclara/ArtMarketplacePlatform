import { Component } from '@angular/core';
import { ForcastService } from './forcast.service';

@Component({
  selector: 'app-weatherforcast',
  standalone: true,
  imports: [],
  templateUrl: './weatherforcast.component.html',
  styleUrl: './weatherforcast.component.css'
})
export class WeatherForecastComponent {
  weatherForecast: any;
  /**
   *
   */
  constructor(private weatherService: ForcastService) {
    this.weatherService.GetForecast()
        .subscribe(response => {
          console.log("response", response)
          this.weatherForecast=response
  
        });
    
  }
}

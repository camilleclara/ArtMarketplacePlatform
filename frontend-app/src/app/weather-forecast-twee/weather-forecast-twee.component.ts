import { Component } from '@angular/core';
import { ForcastService } from '../weatherforcast/forcast.service';

@Component({
  selector: 'app-weather-forecast-twee',
  standalone: true,
  imports: [],
  templateUrl: './weather-forecast-twee.component.html',
  styleUrl: './weather-forecast-twee.component.css'
})
export class WeatherForecastTweeComponent {
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

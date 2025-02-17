import { Component } from '@angular/core';
import { ForcastService } from '../weatherforcast/forcast.service';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {
  weatherForecast: any;
    /**
     *
     */
    constructor(private weatherService: ForcastService) {
      this.weatherService.GetForecast()
          .subscribe(response => {
            this.weatherForecast=response
          });
      
    }
}

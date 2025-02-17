import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { WeatherForecastComponent } from './weatherforcast/weatherforcast.component';
import { authGuard } from './login/auth.guard';
import { admGuard } from './login/adm.guard';
import { userGuard } from './login/user.guard';
import { WeatherForecastTweeComponent } from './weather-forecast-twee/weather-forecast-twee.component';

export const routes: Routes = [
    {path: 'login', component: LoginComponent},
    {path: 'weather', component: WeatherForecastComponent, canActivate: [authGuard, userGuard]},
    {path: 'twee', component: WeatherForecastTweeComponent, canActivate: [authGuard, admGuard]}
];

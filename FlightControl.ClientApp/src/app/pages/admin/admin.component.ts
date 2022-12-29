import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
})
export class AdminComponent {
  public forecasts?: WeatherForecast[];

  constructor(authService: AuthService, http: HttpClient) {
    http
      .get<WeatherForecast[]>('api/weatherforecast', {
        headers: { Authorization: 'bearer ' + authService.token },
        withCredentials: true,
      })
      .subscribe(
        (result) => {
          this.forecasts = result;
        },
        (error) => console.error(error)
      );
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

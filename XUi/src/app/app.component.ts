import { Component } from '@angular/core';
import { AppService } from './services/app.service';
import {Country, City, WeatherResponse } from './models/models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent {
  selectedCountryId: number = 0;
  selectedCity: string = "";
  countries: Country[] = [];
  cities: City[] = [];
  weatherData: WeatherResponse = new WeatherResponse;

  constructor(private appService: AppService) { }

  ngOnInit(){
    this.appService.getCountries().subscribe(res => {
      this.countries = res;
    });
  }

  onCountrySelected() {
    this.appService.getCities(this.selectedCountryId).subscribe(res => {
      this.selectedCity = "";
      this.cities = res;
    });
  }

  onCitySelected() {
    this.appService.getWeatherInfo(this.selectedCity).subscribe(res => {
      this.weatherData = res;
    });
  }
}

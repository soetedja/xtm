import { Component, Input } from '@angular/core';
import { WeatherResponse } from 'src/app/models/models';

@Component({
    selector: 'weather-info',
    templateUrl: './weather-info.component.html',
    styleUrls: ['./weather-info.component.sass']
})
export class WeatherInfoComponent {
    @Input()
    weatherResponse!: WeatherResponse;
}

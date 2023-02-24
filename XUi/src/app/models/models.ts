export class Country {
    id: string = "";
    name: string = "";
    code: string = "";
}

export class City {
    name: string = "";
    countryId: number = 0;
}
export class WeatherResponse {
    location: string = '';
    time: Date = new Date();
    windSpeed: number = 0;
    windDegree: number = 0;
    visibility: number = 0;
    skyConditions: string = '';
    temperatureCelsius: number = 0;
    temperatureFahrenheit: number = 0;
    dewPoint: number = 0;
    relativeHumidity: number = 0;
    pressure: number = 0;
}

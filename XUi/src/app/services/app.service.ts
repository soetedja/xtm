import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AppService {
    private apiUrl = 'https://localhost:44358/api/v1';

    constructor(private http: HttpClient) { }

    getCountries(): Observable<any> {
        return this.http.get<any>(`${this.apiUrl}/location/countries`);
    }

    getCities(countryId: number): Observable<any> {
        return this.http.get<any>(`${this.apiUrl}/location/cities/${countryId}`);
    }

    getWeatherInfo(city: string): Observable<any> {
        return this.http.get<any>(`${this.apiUrl}/weatherforecast/city/${city}`);
    }
}
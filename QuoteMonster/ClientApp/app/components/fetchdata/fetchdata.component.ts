import { Component } from '@angular/core';
import { Http, Headers } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    public forecasts: WeatherForecast[];

	constructor(http: Http) {
		var jwt = localStorage.getItem('id_token');
		var authHeader = new Headers();
		if (jwt) {
			authHeader.append('Authorization', 'Bearer ' + jwt);
		}

		http.get('/api/SampleData/WeatherForecasts', {
			headers: authHeader
		}).subscribe(result => {
            this.forecasts = result.json() as WeatherForecast[];
        });
    }
}

interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

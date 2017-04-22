import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

@Injectable()
export class QuoteService {

	constructor(
		private http: Http
	) {	}


	getRandomQuote() {

		var jwt = localStorage.getItem('id_token');
		var authHeader = new Headers();
		if (jwt) {
			authHeader.append('Authorization', 'Bearer ' + jwt);
		}

		return this.http.get('/api/RandomQuote', {
			headers: authHeader
		});

	}

	search() {
		var jwt = localStorage.getItem('id_token');
		var authHeader = new Headers();
		if (jwt) {
			authHeader.append('Authorization', 'Bearer ' + jwt);
		}

		return this.http.get('/api/Search', {
			headers: authHeader
		});
	}
}

export interface Quote {
	id: number;
	text: string;
}

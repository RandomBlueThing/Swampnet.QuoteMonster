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

		return this.http.get('/api/Quotes/Random', {
			headers: authHeader
		});

	}

	getQuote(id: number) {
		var jwt = localStorage.getItem('id_token');
		var authHeader = new Headers();
		if (jwt) {
			authHeader.append('Authorization', 'Bearer ' + jwt);
		}

		return this.http.get('/api/Quotes/' + id, {
			headers: authHeader
		});
	}

	search(text: string, author: string) {
		var jwt = localStorage.getItem('id_token');
		var authHeader = new Headers();
		if (jwt) {
			authHeader.append('Authorization', 'Bearer ' + jwt);
		}

		return this.http.get('/api/Quotes?text=' + text + '&author=' + author, {
			headers: authHeader
		});
	}

	save(quote: Quote) {
		console.log('quoteService.save: ' + quote.id);
		var jwt = localStorage.getItem('id_token');
		var authHeader = new Headers();
		authHeader.append('Content-Type', 'application/json');
		if (jwt) {
			authHeader.append('Authorization', 'Bearer ' + jwt);
		}

		return this.http.post('/api/Quotes',
			JSON.stringify(quote),
			{ headers: authHeader });
	}

}

export interface Author {
	id: number;
	name: string;
}

export interface Quote {
	id: number;
	text: string;
	canEdit: boolean;
	author: Author;
}

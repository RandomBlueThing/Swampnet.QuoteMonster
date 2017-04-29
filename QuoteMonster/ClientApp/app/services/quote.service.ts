import { Injectable } from '@angular/core';
import { AuthenticatedHttp } from '../services/authenticated-http.service'
import { Http, Headers } from '@angular/http';

@Injectable()
export class QuoteService {

	constructor(
		private http: AuthenticatedHttp
	) {	}


	getRandomQuote() {
		return this.http.get('/api/Quotes/Random');
	}

	getQuote(id: number) {
		return this.http.get('/api/Quotes/' + id);
	}

	getAuthors() {
		return this.http.get('/api/Authors');
	}

	search(text: string, author: string) {
		return this.http.get('/api/Quotes?text=' + text + '&author=' + author);
	}

	save(quote: Quote) {
		return this.http.post(
			'/api/Quotes',
			JSON.stringify(quote));
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
	author: string;
}

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Quote, QuoteService } from '../../services/quote.service'
import { EditQuoteComponent } from './edit-quote.component';

@Component({
	selector: 'search',
	templateUrl: './search.component.html',
	styleUrls: ['./search.component.css'],
	providers: [QuoteService]
})
export class SearchComponent {

	public data: Quote[];

	constructor(
		private quoteService: QuoteService,
		private router: Router) {

		this.quoteService.search()
				.subscribe(result => {
					this.data = result.json() as Quote[];
					console.log(result.json());
				});
	}

	newQuote() {
		this.router.navigate(['/edit', 0]);
	}
}


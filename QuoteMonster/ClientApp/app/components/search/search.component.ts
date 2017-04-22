import { Component } from '@angular/core';
import { Quote, QuoteService } from '../../services/quote.service'

@Component({
	selector: 'search',
	templateUrl: './search.component.html',
	providers: [QuoteService]
})
export class SearchComponent {

	public data: Quote[];

	constructor(private quoteService: QuoteService) {

		this.quoteService.search()
				.subscribe(result => {
					this.data = result.json() as Quote[];
					console.log(result.json());
				});
	}
}


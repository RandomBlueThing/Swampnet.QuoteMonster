import { Component, OnInit } from '@angular/core';
import { Quote, QuoteService } from '../../services/quote.service'

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
	styleUrls: ['./home.component.css'],
	providers: [QuoteService]
})
export class HomeComponent implements OnInit  {

	public quote: Quote;

	constructor(private quoteService: QuoteService) {
	}

	ngOnInit(): void {
		this.refreshQuote();
	}


	public refreshQuote() {
		this.quoteService.getRandomQuote()
			.subscribe(result => {
				this.quote = result.json() as Quote;
			});		
	}
}

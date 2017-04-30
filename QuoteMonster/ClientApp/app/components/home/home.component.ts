import { Component, OnInit, OnDestroy } from '@angular/core';
import { Quote, QuoteService } from '../../services/quote.service'
import { Observable } from 'rxjs/Rx';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
	styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy  {

	public quote: Quote;

	constructor(private quoteService: QuoteService) {
	}

	ngOnInit(): void {
		console.log("ngOnInit");
		this.refreshQuote();
		Observable.interval(1000 * 20).subscribe(x => {
			this.refreshQuote();
		});
	}

	ngOnDestroy(): void {
		console.log("ngOnDestroy");
	}


	public refreshQuote() {
		this.quoteService.getRandomQuote()
			.subscribe(result => {
				this.quote = result.json() as Quote;
			});		
	}
}

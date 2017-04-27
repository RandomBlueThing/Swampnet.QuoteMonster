import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Quote, QuoteService } from '../../services/quote.service'
import { AuthService } from '../../services/auth.service'
import { EditQuoteComponent } from './edit-quote.component';

@Component({
	selector: 'search',
	templateUrl: './search.component.html',
	styleUrls: ['./search.component.css']
})
export class SearchComponent {

	public data: Quote[];

	@Input() text: string;
	@Input() author: string;

	constructor(
		private quoteService: QuoteService,
		private auth: AuthService,
		private router: Router) {
		this.text = '';
		this.author = '';
		this.search();
	}

	search() {
		this.quoteService.search(this.text, this.author)
			.subscribe(result => {
				this.data = result.json() as Quote[];
				console.log(result.json());
			});
	}

	newQuote() {
		this.router.navigate(['/edit', 0]);
	}
}


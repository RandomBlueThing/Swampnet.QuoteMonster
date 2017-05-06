import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Quote, QuoteService } from '../../services/quote.service'
import { AuthService } from '../../services/auth.service'

@Component({
	selector: 'search',
	templateUrl: './search.component.html',
	styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

	public data: Quote[];
	public authors: string[];
	private page: number;
	private pageSize: number;
	private busy: boolean;

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

	ngOnInit(): void {
		this.quoteService.getAuthors()
			.subscribe(result => this.authors = result.json() as string[]);
	}


	search() {
		this.page = 0;
		this.pageSize = 10;
		this.refresh();
	}

	private refresh() {
		this.busy = true;
		this.quoteService.search(this.text, this.author, this.page, this.pageSize)
			.subscribe(result => {
				this.data = result.json() as Quote[];
				this.busy = false;
			});
	}


	newQuote() {
		this.router.navigate(['/edit', 0]);
	}

	nextPage() {
		this.page++;
		this.refresh();
	}

	previousPage() {
		if (this.page > 0) {
			this.page--;
			this.refresh();
		}
	}
}


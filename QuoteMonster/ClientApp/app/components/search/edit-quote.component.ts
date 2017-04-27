import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { Quote, QuoteService } from '../../services/quote.service'
import 'rxjs/add/operator/switchMap';

@Component({
	selector: 'edit-quote',
	templateUrl: './edit-quote.component.html',
	styleUrls: ['./edit-quote.component.css']
})
export class EditQuoteComponent implements OnInit {

	@Input() quote: Quote;
	public authors: string[];

	constructor(
		private quoteService: QuoteService,
		private route: ActivatedRoute,
		private location: Location
	) { }


	ngOnInit(): void {
		this.route.params
			.switchMap((params: Params) => this.quoteService.getQuote(+params['id']))
			.subscribe(result => {
				this.quote = result.json() as Quote;
			});

		this.quoteService.getAuthors()
			.subscribe(result => this.authors = result.json() as string[]);
	}


	save(): void {
		this.quoteService.save(this.quote).subscribe(result => {
			this.goBack();
		});
		
	}


	goBack(): void {
		this.location.back();
	}
}
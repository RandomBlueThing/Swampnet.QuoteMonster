import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { Quote, QuoteService } from '../../services/quote.service'
import 'rxjs/add/operator/switchMap';

@Component({
	selector: 'edit-quote',
	templateUrl: './edit-quote.component.html',
	styleUrls: ['./edit-quote.component.css'],
	providers: [QuoteService]
})
export class EditQuoteComponent implements OnInit {

	@Input() quote: Quote;

	constructor(
		private quoteService: QuoteService,
		private route: ActivatedRoute,
		private location: Location
	) { }


	ngOnInit(): void {
		this.route.params
			.switchMap((params: Params) => this.quoteService.getQuote(+params['id']))
			.subscribe(result => {
				console.log(result.json());
				this.quote = result.json() as Quote;
			});
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
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import 'rxjs/add/operator/switchMap';
import { QuoteService, Profile } from '../../services/quote.service';

@Component({
	selector: 'edit-profile',
	templateUrl: './edit-profile.component.html'
})
export class EditProfileComponent implements OnInit {

	@Input() profile: Profile;


	constructor(
		private route: ActivatedRoute,
		private location: Location,
		private quoteService: QuoteService
	) {
	}


	ngOnInit(): void {
		this.quoteService.loadProfile()
			.subscribe(result => {
				console.log(result.json());
				this.profile = result.json() as Profile;
			});
	}


	save(): void {
		this.quoteService.saveProfile(this.profile).subscribe(result => {
			this.goBack();
		});
	}


	goBack(): void {
		this.location.back();
	}
}



import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import 'rxjs/add/operator/switchMap';

@Component({
	selector: 'edit-profile',
	templateUrl: './edit-profile.component.html'
})
export class EditProfileComponent implements OnInit {

	constructor(
		private route: ActivatedRoute,
		private location: Location
	) { }


	ngOnInit(): void {
	}


	save(): void {
	}


	goBack(): void {
		this.location.back();
	}
}
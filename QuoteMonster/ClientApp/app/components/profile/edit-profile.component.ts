import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import 'rxjs/add/operator/switchMap';
import { AuthService, Profile } from '../../services/auth.service';

@Component({
	selector: 'edit-profile',
	templateUrl: './edit-profile.component.html'
})
export class EditProfileComponent implements OnInit {

	@Input() profile: Profile;


	constructor(
		private route: ActivatedRoute,
		private location: Location,
		private auth: AuthService
	) {
	}


	ngOnInit(): void {
		this.auth.loadProfile()
			.subscribe(result => {
				console.log(result.json());
				this.profile = result.json() as Profile;
			});
	}


	save(): void {
		this.auth.saveProfile(this.profile).subscribe(result => {
			this.goBack();
		});
	}


	goBack(): void {
		this.location.back();
	}
}



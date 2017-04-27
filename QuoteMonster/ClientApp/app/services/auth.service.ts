import { Injectable } from '@angular/core';
import { tokenNotExpired } from 'angular2-jwt';
import { Router } from '@angular/router';
import { Http, Headers } from '@angular/http';

// Avoid name not found warnings
declare var Auth0Lock: any;

@Injectable()
export class AuthService {
	// Configure Auth0
	lock = new Auth0Lock('qICeCpL8xVCEjTHv1CYhZpRUM7csAGIy', 'swampnet.eu.auth0.com', {});

	constructor(
		private router: Router,
		private http: Http
		) {
		// Add callback for lock `authenticated` event
		this.lock.on('authenticated', (authResult) => {
			localStorage.setItem('id_token', authResult.idToken);

			this.onLogin(authResult.accessToken).subscribe(result => {
				// @TODO: Check result and potentialy navigate to /profile (or even log us out!)
				var profile = result.json() as Profile;

				if (!profile.isActive) {
					console.log("user is inactive - logging out.")
					this.logout();
				}

				if (profile.isNew) {
					console.log("user is new - redirecting to profile.")
					this.router.navigate(['/profile']);
				}
			});
		});
	}

	public login() {
		// Call the show method to display the widget.
		this.lock.show();
	};

	public authenticated() {
		// Check if there's an unexpired JWT
		// It searches for an item in localStorage with key == 'id_token'
		return tokenNotExpired('id_token');
	};

	public logout() {
		console.log("logging out - redirecting to home.")
		// Remove token from localStorage
		localStorage.removeItem('id_token');
		this.router.navigate(['/home']);
	};


	onLogin(access_token: string) {
		var jwt = localStorage.getItem('id_token');
		var authHeader = new Headers();
		if (jwt) {
			authHeader.append('Authorization', 'Bearer ' + jwt);
		}

		return this.http.get('/api/Users/OnLogin?access_token=' + access_token, {
			headers: authHeader
		});
	}

	loadProfile() {
		var jwt = localStorage.getItem('id_token');
		var authHeader = new Headers();
		if (jwt) {
			authHeader.append('Authorization', 'Bearer ' + jwt);
		}

		return this.http.get('/api/Users/GetCurrent', {
			headers: authHeader
		});
	}


	saveProfile(profile: Profile) {
		console.log('quoteService.saveProfile: ' + profile.id);
		var jwt = localStorage.getItem('id_token');
		var authHeader = new Headers();
		authHeader.append('Content-Type', 'application/json');
		if (jwt) {
			authHeader.append('Authorization', 'Bearer ' + jwt);
		}

		return this.http.put('/api/Users/' + profile.id,
			JSON.stringify(profile),
			{ headers: authHeader });
	}

}

export interface Profile {
	id: string;
	displayName: string;
	email: string;
	isNew: boolean;
	isActive: boolean;
	avatarUrl: string;
}

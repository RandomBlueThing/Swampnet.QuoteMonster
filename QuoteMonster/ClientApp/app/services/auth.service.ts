import { Injectable } from '@angular/core';
import { tokenNotExpired } from 'angular2-jwt';
import { Router } from '@angular/router';
import { QuoteService, Profile } from './quote.service';

// Avoid name not found warnings
declare var Auth0Lock: any;

@Injectable()
export class Auth {
	// Configure Auth0
	lock = new Auth0Lock('qICeCpL8xVCEjTHv1CYhZpRUM7csAGIy', 'swampnet.eu.auth0.com', {});

	constructor(
		private router: Router,
		private quoteService: QuoteService) {
		// Add callback for lock `authenticated` event
		this.lock.on('authenticated', (authResult) => {
			localStorage.setItem('id_token', authResult.idToken);

			this.quoteService.onLogin(authResult.accessToken).subscribe(result => {
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
}

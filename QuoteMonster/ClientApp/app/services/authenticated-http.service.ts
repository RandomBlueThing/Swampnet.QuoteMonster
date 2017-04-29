import {Injectable} from '@angular/core';
import {Http, Headers} from '@angular/http';

@Injectable()
export class AuthenticatedHttp {

  constructor(private http: Http) {}

  get(url) {
	let jwt = localStorage.getItem('id_token');
	let authHeader = new Headers();
	if (jwt) {
		authHeader.append('Authorization', 'Bearer ' + jwt);
	}

	return this.http.get(url, {
		headers: authHeader
	});
  }

  post(url, data) {
	let jwt = localStorage.getItem('id_token');
	let authHeader = new Headers();
	authHeader.append('Content-Type', 'application/json');
	if (jwt) {
		authHeader.append('Authorization', 'Bearer ' + jwt);
	}

	return this.http.post(url, data,
		{ headers: authHeader });
  }

  put(url, data) {
	  let jwt = localStorage.getItem('id_token');
	  let authHeader = new Headers();
	  authHeader.append('Content-Type', 'application/json');
	  if (jwt) {
		  authHeader.append('Authorization', 'Bearer ' + jwt);
	  }

	  return this.http.put(url, data,
		  { headers: authHeader });
  }

}
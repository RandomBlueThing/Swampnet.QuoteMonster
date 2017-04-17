import { Component } from '@angular/core';
import { Auth } from '../../services/auth.service';

@Component({
    selector: 'app',
	providers: [Auth],
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
	constructor(private auth: Auth) { }
}

import { Component, OnInit } from '@angular/core';
import { AuthService, Profile } from '../../services/auth.service';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent implements OnInit {
	constructor(private auth: AuthService) { }

	ngOnInit(): void {
		console.log("ngOnInit");
	}
}

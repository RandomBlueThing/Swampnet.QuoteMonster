import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { SearchComponent } from './components/search/search.component';
import { EditQuoteComponent } from './components/edit/edit-quote.component';
import { EditProfileComponent } from './components/profile/edit-profile.component';
import { AboutComponent } from './components/about/about.component';
import { AUTH_PROVIDERS } from 'angular2-jwt';
import { QuoteService } from './services/quote.service';
import { AuthService } from './services/auth.service';
import { AuthenticatedHttp } from './services/authenticated-http.service';
import { NguiAutoCompleteModule } from '@ngui/auto-complete';

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
		HomeComponent,
		SearchComponent,
		EditQuoteComponent,
		EditProfileComponent,
		AboutComponent
	],
	providers: [
		AUTH_PROVIDERS,
		QuoteService,
		AuthService,
		AuthenticatedHttp
	],
    imports: [
		UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
		FormsModule,
		NguiAutoCompleteModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
			{ path: 'search', component: SearchComponent },
			{ path: 'edit/:id', component: EditQuoteComponent },
			{ path: 'profile', component: EditProfileComponent },
            { path: '**', redirectTo: 'home' }
        ])
	]
})
export class AppModule {
}

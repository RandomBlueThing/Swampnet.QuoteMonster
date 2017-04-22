import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { SearchComponent } from './components/search/search.component';
import { EditQuoteComponent } from './components/search/edit-quote.component';
import { AUTH_PROVIDERS } from 'angular2-jwt';

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
		HomeComponent,
		SearchComponent,
		EditQuoteComponent
	],
	providers: [AUTH_PROVIDERS],
    imports: [
		UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
		FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
			{ path: 'search', component: SearchComponent },
			{ path: 'edit/:id', component: EditQuoteComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ])
	]
})
export class AppModule {
}

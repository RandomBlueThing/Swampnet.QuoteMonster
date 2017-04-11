import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { HelloService } from '../helloworld.service';

@Component({
    selector: 'counter',
	templateUrl: './counter.component.html',
	providers: [HelloService]
})
export class CounterComponent implements OnInit {
	public currentCount = 0;
	public greeting = '';

	constructor(private helloService: HelloService) { }

	ngOnInit(): void {
		this.helloService.getGreetingSlowly()
			.then(g => this.greeting = g);

		this.helloService.getGreeting()
			.then(g => this.greeting = g);
	}

    public incrementCounter() {
        this.currentCount++;
    }
}

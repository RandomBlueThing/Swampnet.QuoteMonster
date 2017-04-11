import { Injectable } from '@angular/core';

@Injectable()
export class HelloService {

	getGreeting(): Promise<string> {
		return Promise.resolve('Hello, world!');
	}

	getGreetingSlowly(): Promise<string> {

		return new Promise(r => {
			setTimeout(() => r('Hello, slowly!'), 5000);
		});

	}
}
# Swampnet.QuoteMonster

- Create empty solution
- In solution root:

> \>dotnet new angular -n QuoteMonster

- Add QuoteMonster.csproj to solution.
- F5!

Adding user secrets:
https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets

---

Auth0

npm install --save angular-jwt

Index.cshtml
	Get rid of the  asp-prerender-module stuff on app
	<script src="https://cdn.auth0.com/js/lock/10.14/lock.min.js"></script>

Add auth.service service & inject anywhere we need a reference to it.

*Note, I got a Auth0Lock not defined errors all over the place if I tried to npm install it. Referencing the cdn seems to solve this, but I don't know why.

See: https://auth0.com/docs/quickstart/spa/angular2


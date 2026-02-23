# IdP - Identity Provider

This project allows me and you to authenticate users accross your applications.
For now i use this project for Plantcare application but in the future it will allows me to use it generatively for different kinds of my apps.

### How it works ?
My Identity Provider allows me to register once and login accross my systems, it delivers session management and user data provider, thanks to which i can easily authorize user on frontend and backend side.

### Client packages
For know there is only small IdentityProviderSystem.Client package that allows user to provide authorization via http requests to IdP by calling the method ValidateToken from TokenService. In the future i will add middleware auth to relieve the user from unnecessary work.

Also i plan to add npm package based on typescript to provide backend methods based on RTK and ready to go request types.

### Safety
I use BCrypt to create hash from user passwords with random salt which prevents attackers from creating rainbow tables, salt is randomly generated via BCrypt algorithm and stored in database, i use job schedulers to exchange salt in every five minutes. Of course passwords are stored as hashes, in response user get JWT token with userId data in payload

### JWT
In my IdP service i use JWT to check user session and UserId ( user retrieve userId via IdentityProviderSystem.Client package ), data about session is stored in database and users don't have direct access to it due to security features.

### How to use IdP for my projects ?
You can start IdP system via docker-compose command and by installing IdentityProviderSystem.Client package on your backend service

## How to setup Idp-Client ?
There are two versions of Idp-Client

Idp-Client provided via Nuget package which you can add to your project via :


>dotnet add package IdpClient


Now in your program.cs you should add Authorization middleware from IdentityProviderSystem:


```
using IdentityProviderSystem.Client;
using IdentityProviderSystem.Client.Middleware;

app.UseMiddleware<Authorization>();
```

Now your application will automatically check if user has valid token
remember to put the auth token in requests header
In future versions i will provide correct Idp URLs configuration

There is also Idp-Client provided via NPM to serve user authorization on your
Frontend App, to install it just run :

> npm i identity-provider-client

After successfull installation you are able configure your react + redux frontend app
In your index.tsx add IdpClient provider by importing :

> import IdpClient from 'identity-provider-client'

Next you should wrap your application with IdpClient for example :

```
<BrowserRouter>
 <IdpClient
   clientApi={plantcareApi}
   authBaseRoute={RoutingConstants.authBasic}
   dashboardRoute={RoutingConstants.root}>
        <App />
  </IdpClient>
</BrowserRouter>
```

ClientApi are your RTKs IdpClient will automatically add them to Redux Context,
authBaseRoute is page on your application on which user land if not authenticated
and dashboardRoute is a page on which user should land after authentication

Idp-Client will take care of monitoring user token life and refreshing if token will expire, user will be automatically redirected to authBaseRoute, also if not authenticated and will try to go on dashboard or any other side which requires authentication, he will be blocked and redirected back to authBaseRoute

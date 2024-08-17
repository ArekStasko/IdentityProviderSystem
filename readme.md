# IdP - Identity Provider

This project allows me to authenticate users accross my applications.
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
You can start IdP system via docker-compose command and by installing IdentityProviderSystem.Client package on your backend service, i will provide more details near in the feature due to development of this app.

## How to setup Idp-Client ?
-- in progress
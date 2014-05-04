Burgerama
=========
Bust the best burger in London!

Contributing
------------
You want to get involved? Awesome!
First, read through the getting started section of this document to get up and running with your local environment. Then choose the area you want to work on: You can either find yourself an open ticket (if there are any) or come up with a new use case and implement a completely new service. We are not bound to any technology or language, use whatever you fancy as long as it integrates with the rest of the system.
Just remember, this is a playground project. It has no actual business requirements and is totally over-architectured. Yagni? Who cares.


Getting started
---------------
In order to run the Burgerama web frontend on your local machine, you additionally have to run all the API micro-services. Hosting the services is currently only supported on Windows with local IIS and requires some set up, as outlined below. The current goal is to deploy everything to [Azure], but we are also working on support for different platforms on top of [OWIN] / [Mono].
Confidential configuration data like credentials should be stored in the `Config` folder of the relevant project. Files with the ending `.confidential.config` are ignored by git and thus can't be committed unintentionally. Currently there is no proper configuration management, you'll have to copy confidential configuration files around manually. 

### IIS
The URLs http://dev.burgerama.co.uk and http://api.dev.burgerama.co.uk redirect to 127.0.0.1, i.e. to your local machine. You have to manually set up a web site in IIS for each URL. After that, let Visual Studio create the proper directory mapping by clicking the "Create Virtual Directory" button in the web settings in each UI and API project.

### Authorization
Burgerama uses [auth0] as identity and authorization infrastructure. You can either contact @dstockhammer to get a copy of the dev credentials, or you can create your own auth0 account and use that one. 
Each API service expects a file `Config\Auth0.confidential.config` in the following format:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<auth0 issuer="" audience="" secret="" />
```

### Database
Currently, all services use [MongoDb] as database. Again, you can either contact @dstockhammer to get a copy of the dev credentials, or you can provide your own MongoDb instance(s).
Each API service expects a file `Config\MongoDb.confidential.config` in the following format:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<mongoDb database="" connectionString="" />
```

### Service Bus
This area is work in progress. For now, go read up on [NServiceBus].
To install the required infrastructure, run the following commands in the NuGet Package Manager Console:
```
PM> Install-NServiceBusDtc 
PM> Install-NServiceBusMsmq
PM> Install-NServiceBusRavenDB
PM> Install-NServiceBusPerformanceCounters
```

[Azure]:http://azure.microsoft.com/
[OWIN]:http://owin.org/
[Mono]:http://www.mono-project.com/
[auth0]:https://auth0.com/
[MongoDb]:https://www.mongodb.org/
[NServiceBus]:http://docs.particular.net/

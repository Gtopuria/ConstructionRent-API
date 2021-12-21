# ConstructionRent-API

## Project

Project is using .NET 5 and C#

## RUN
Project can be run by command `docker-compose up` it runs on port 80

## Assumptions
For demonstration purposes I have not used persistance/db. currently data is coming from in memory, there is a project `.Data` where persistance layer could be implemented,
in that case repository pattern can be used and injected into the service. Right now service methods are synchronous, this would cause it to become async.

## Future improvements
- Caching - in this case we can cache equipment list.
- Storing data - When it comes to storing orders, it will be more beneficial if we store rent orders in NoSQL db. By that we will avoid heavy joins when order will get bigger eventually.
- Logging - basic logging is enabled. Would be better if we keep logs in files.


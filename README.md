# sample-api

## How to run it locally
* Make sure Docker has been installed and running
* Run the command to start a SQL server container
```cmd
docker run -d --rm --name test-mssql -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password01!" -p 61234:1433 microsoft/mssql-server-linux:2017-latest
```
* Start the service
```
dotnet run
```
* Verify the service is running. Open your favorite browser and navigate to these two links
```
https://localhost:5001/swagger
https://localhost:5001/healthcheck
```

# Our Coding Convention (WIP)

#### Use DateTimeOffset instead of DateTime
https://blogs.msdn.microsoft.com/davidrickard/2012/04/06/datetime-and-datetimeoffset-in-net-good-practices-and-common-pitfalls/

## Database design
#### Makes columns(as more as possible) be non-nullable
#### Do not set default value for the column

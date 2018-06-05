# SimpleWebApiClient

A simple Web API client

if has Json.NET, then [JsonFormatter](/SimpleWebApiClient/Formatters/JsonFormatter.cs) use it.

Nuget package:

```
Install-Package SimpleWebApiClient
```


Sample:
```c#

var client = new WebApiClient("http://localhost/");

//get object
var resultObj = client.Get<ReturnObj>("api/Values/123");

//get list
var resultObjs = client.Get<List<ReturnObj>>("api/Values");

//get string
result = client.Get("api/Token?userName=Simple");

//add token
client.Header.Add("Authorization", "Bearer " + result);
//or
client.Header.AddToken(result);

result = client.Post("api/Values", resultObj);

result = client.Put("api/Values/123", "test");

result = client.Delete("api/Values/123");

```

Get original result string (or use custom formmater)

```c#

var client = new WebApiClient("http://localhost/");

client.Formatter = new Formatters.StringFormatter();

var jsonString = client.Get("api/Value");

```

Request cookie to web server

```C#

var client = new WebApiClient("http://localhost/");

client.Cookies.Add(new System.Net.Cookie("test", "12345"));

```

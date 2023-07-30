### Azure Function App - dotnet(isolated process)로 만들어보기
- 닷넷 버전 7.0
- 새로운 HttpTrigger를 만들기

<br>


### 설치 패키지


```c#
  <ItemGroup>
    <PackageReference Include="Microsoft.Azure.Functions.Worker" Version="1.18.0" />
    <PackageReference Include="Microsoft.Azure.Functions.Worker.Extensions.Http" Version="3.0.13" />
    <PackageReference Include="Microsoft.Azure.Functions.Worker.Extensions.OpenApi" Version="1.5.1" />
    <PackageReference Include="Microsoft.Azure.Functions.Worker.Sdk" Version="1.13.0" />
  </ItemGroup>
```


---
### 추가한 코드
- Opreation, Parameter, ReponseWithBody 추가


```c#
[OpenApiOperation(operationId: "Run", tags: new[] { "name" }, Summary = "The name of the person to use in the greeting.", Description = "This HTTP triggered function returns a person's name.", Visibility = OpenApiVisibilityType.Important)]

[OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "The name of the person to use in the greeting.", Description = "The name of the person to use in the greeting.", Visibility = OpenApiVisibilityType.Important)]

[OpenApiResponseWithBody(statusCode: System.Net.HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "The response", Description = "This returns the response")]

[OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Summary = "The response", Description = "This returns the response")]
```

<br>

- query 값이 null 인 경우, 아닌 경우 응답 메시지 추가


```c#
var name = req.Query["name"];

// ... 

var resMessage = string.IsNullOrEmpty(name) 
? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
: $"Hello, {name}. This HTTP triggered function executed successfully.";

response.WriteString(resMessage);
```


---
### 결과 화면


<img width="727" alt="스크린샷 2023-07-30 오후 7 23 37" src="https://github.com/hhiyeon/hhiyeon/assets/52193680/4bd27d1c-3c17-484a-aad0-fafd5188a25a">


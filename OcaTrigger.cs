using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace azure_prac_httptrigger
{
    public class OcaTrigger
    {
        private readonly ILogger _logger;

        public OcaTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<OcaTrigger>();
        }

        [OpenApiOperation(operationId: "Run", tags: new[] { "name" }, Summary = "The name of the person to use in the greeting.", Description = "This HTTP triggered function returns a person's name.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "The name of the person to use in the greeting.", Description = "The name of the person to use in the greeting.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: System.Net.HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "The response", Description = "This returns the response")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "text/plain", bodyType: typeof(string), Summary = "The response", Description = "This returns the response")]

        [Function("greetings")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var name = req.Query["name"];
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            var resMessage = string.IsNullOrEmpty(name) 
            ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            : $"Hello, {name}. This HTTP triggered function executed successfully.";

            response.WriteString(resMessage);

            return response;
        }
    }
}

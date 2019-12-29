using System;
using System.Net.Http;
using DiagnosticSourceDemo.Attributes;
using DiagnosticSourceDemo.Processors;

namespace DiagnosticSourceDemo.DiagnosticProcessors
{
    public class HttpClientDiagnosticProcessor : IDiagnosticProcessor
    {
        public string ListenerName { get; } = "HttpHandlerDiagnosticListener";


        [DiagnosticName("System.Net.Http.Request")]
        public void HttpRequest([Property(Name = "Request")]HttpRequestMessage request)
        {
        }

        [DiagnosticName("System.Net.Http.Response")]
        public void HttpResponse([Property(Name = "Response")] HttpResponseMessage response)
        {
        }

        [DiagnosticName("System.Net.Http.Exception")]
        public void HttpException([Property(Name = "Request")] HttpRequestMessage request, [Property(Name = "Exception")] Exception exception)
        {
        }
    }
}

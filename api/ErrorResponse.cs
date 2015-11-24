using System;
using System.Net;
using System.Net.Sockets;
using Nancy.Responses;
using Newtonsoft.Json;
using HttpStatusCode = Nancy.HttpStatusCode;

namespace api
{
    public class ErrorResponse : JsonResponse
    {
        readonly Error error;

        private ErrorResponse(Error error)
            : base(error, new DefaultJsonSerializer())
        {
            this.error = error;
        }

        public string ErrorMessage { get { return error.ErrorMessage; } }
        public string FullException { get { return error.FullException; } }
        public string[] Errors { get { return error.Errors; } }

        public static ErrorResponse FromMessage(string message)
        {
            return new ErrorResponse(new Error { ErrorMessage = message });
        }

        public static ErrorResponse FromException(Exception ex)
        {
            var exception = ex;

            var summary = exception.Message;
            //if (exception is WebException || exception is SocketException)
            //{
            //    // Commonly returned when connections to RavenDB fail
            //    summary = "The Octopus windows service may not be running: " + summary;
            //}

            var statusCode = HttpStatusCode.InternalServerError;

            var error = new Error { ErrorMessage = summary, FullException = exception.ToString() };

            // Special cases
            //if (exception is ResourceNotFoundException)
            //{
            //    statusCode = HttpStatusCode.NotFound;
            //    error.FullException = null;
            //}

            //if (exception is OctopusSecurityException)
            //{
            //    statusCode = HttpStatusCode.Forbidden;
            //    error.FullException = null;
            //}

            var response = new ErrorResponse(error);
            response.StatusCode = statusCode;
            return response;
        }

        class Error
        {
            public string ErrorMessage { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string FullException { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string[] Errors { get; set; }
        }
    }
}
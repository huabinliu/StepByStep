using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LocalApi.Routing;

namespace LocalApi
{
    public class HttpServer : HttpMessageHandler
    {
        #region Please implement the following method to pass the test

        /*
         * An http server is an HttpMessageHandler that accept request and create response.
         * You can add non-public fields and members for help but you should not modify
         * the public interfaces.
         */

        readonly HttpConfiguration configuration;

        public HttpServer(HttpConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var matchedRoute = configuration.Routes.GetRouteData(request);
            var response = matchedRoute == null
                ? new HttpResponseMessage(HttpStatusCode.NotFound)
                : ControllerActionInvoker.InvokeAction(matchedRoute, configuration.CachedControllerTypes,
                    configuration.DependencyResolver, configuration.ControllerFactory);
            return Task.FromResult(response);

        }

        #endregion
    }
}
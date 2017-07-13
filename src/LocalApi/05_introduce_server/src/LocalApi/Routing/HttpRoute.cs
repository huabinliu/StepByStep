using System;
using System.CodeDom.Compiler;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LocalApi.Routing
{
    public class HttpRoute
    {
        public HttpRoute(string controllerName, string actionName, HttpMethod methodConstraint) : 
            this(controllerName, actionName, methodConstraint, null)
        {
        }

        #region Please modifies the following code to pass the test

        /*
         * You can add non-public helper method for help, but you cannot change public
         * interfaces. identifier
         */

        public HttpRoute(string controllerName, string actionName, HttpMethod methodConstraint, string uriTemplate)
        {
            Verify(controllerName, actionName, methodConstraint);
            ControllerName = controllerName;
            ActionName = actionName;
            MethodConstraint = methodConstraint;
            UriTemplate = uriTemplate;
        }

        private void Verify(string controllerName, string actionName, HttpMethod methodConstraint)
        {
            if (controllerName == null)
            {
                throw new ArgumentNullException(nameof(controllerName));
            }
            if (actionName == null)
            {
                throw new ArgumentNullException(nameof(actionName));
            }
            if (methodConstraint == null)
            {
                throw new ArgumentNullException(nameof(methodConstraint));
            }
            using (var provider = CodeDomProvider.CreateProvider("C#"))
            {
                if (!provider.IsValidIdentifier(controllerName) || !provider.IsValidIdentifier(actionName))
                {
                    throw new ArgumentException();
                }
            }  
        }

        #endregion

        public string ControllerName { get; }
        public string ActionName { get; }
        public HttpMethod MethodConstraint { get; }
        public string UriTemplate { get; }

        public bool IsMatch(Uri uri, HttpMethod method)
        {
            if (uri == null) { throw new ArgumentNullException(nameof(uri)); }
            if (method == null) { throw new ArgumentNullException(nameof(method)); }
            string path = uri.AbsolutePath.TrimStart('/');
            return path.Equals(UriTemplate, StringComparison.OrdinalIgnoreCase) &&
                   method == MethodConstraint;
        }
    }
}
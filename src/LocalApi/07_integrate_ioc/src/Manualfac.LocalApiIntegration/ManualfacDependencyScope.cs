using System;
using LocalApi;

namespace Manualfac.LocalApiIntegration
{
    class ManualfacDependencyScope : IDependencyScope
    {
        #region Please implement the class

        /*
         * We should create a manualfac dependency scope so that we can integrate it
         * to LocalApi.
         * 
         * You can add a public/internal constructor and non-public fields if needed.
         */
        readonly ILifetimeScope scope;

        public ManualfacDependencyScope(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public void Dispose()
        {
            scope.Dispose();
        }

        public object GetService(Type type)
        {
            object service;
            scope.TryResolve(type, out service);
            return service;
        }

        #endregion
    }
}
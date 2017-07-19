using System;
using System.Linq;
using System.Reflection;
using Manualfac.Services;

namespace Manualfac.Activators
{
    class ReflectiveActivator : IInstanceActivator
    {
        readonly Type serviceType;

        public ReflectiveActivator(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        #region

        /*
         * This method create instance via reflection. Try evaluating its parameters and
         * inject them using componentContext.
         * 
         * No public members are allowed to add.
         */

        public object Activate(IComponentContext componentContext)
        {
            var constructors = serviceType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (constructors.Length != 1)
            {
                throw new DependencyResolutionException(nameof(serviceType));
            }
            var constructor = constructors[0];
            var parameters = constructor.GetParameters()
                .Select(p => componentContext.ResolveComponent(new TypedService(p.ParameterType))).ToArray();
            return constructor.Invoke(parameters);
        }

        #endregion
    }
}
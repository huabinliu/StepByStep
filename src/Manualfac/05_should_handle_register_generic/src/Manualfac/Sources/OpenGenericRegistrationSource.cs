using System;
using Manualfac.Activators;

namespace Manualfac.Sources
{
    class OpenGenericRegistrationSource : IRegistrationSource
    {
        readonly IServiceWithType genericService;
        readonly Type implementorType;

        public OpenGenericRegistrationSource(IServiceWithType genericService, Type implementorType)
        {
            this.genericService = genericService;
            this.implementorType = implementorType;
        }

        public ComponentRegistration RegistrationFor(Service service)
        {
            #region

            /*
             * This source has 2 properties: the genericService is used as the key to match the
             * service argument. And the implementorType is used to record the actual type used
             * to create instances.
             * 
             * This method will try matching the constructed service to an non-constructed 
             * generic type of genericService. If it is matched, then an concrete component
             * registration needed will be invoked.
             */

            var swt = service as IServiceWithType;
            if (swt == null)
            {
                return null;
            }
            var serviceType = swt.ServiceType;
            if (!serviceType.IsGenericType || !serviceType.IsConstructedGenericType || !swt
                    .ChangeType(serviceType.GetGenericTypeDefinition()).Equals(genericService))
            {
                return null;
            }
            var implementor = implementorType.MakeGenericType(serviceType.GenericTypeArguments);
            return new ComponentRegistration(service, new ReflectiveActivator(implementor));

            #endregion
        }
    }
}
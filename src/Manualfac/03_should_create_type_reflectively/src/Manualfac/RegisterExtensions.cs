using System;
using Manualfac.Activators;
using Manualfac.Services;

namespace Manualfac
{
    public static class RegisterExtensions
    {
        public static IRegistrationBuilder Register<T>(
            this ContainerBuilder cb,
            Func<IComponentContext, T> func)
        {
            #region

            /*
             * Since we have changed the definition of activator. Please re-implement
             * the register extension method.
             */

            if (cb == null)
            {
                throw new ArgumentNullException(nameof(cb));
            }
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }
            var service = new TypedService(typeof(T));
            var activator = new DelegatedInstanceActivator(c => func(c));
            return cb.RegisterComponent(new ComponentRegistration(service, activator));

            #endregion
        }

        public static IRegistrationBuilder RegisterType<T>(
            this ContainerBuilder cb)
        {
            #region

            /*
             * Since you have re-implement Register method, I am sure you can also
             * implement RegisterType method.
             */

            if (cb == null)
            {
                throw new ArgumentNullException(nameof(cb));
            }
            var service = new TypedService(typeof(T));
            var activator = new ReflectiveActivator(typeof(T));
            return cb.RegisterComponent(new ComponentRegistration(service, activator));

            #endregion
        }
    }
}
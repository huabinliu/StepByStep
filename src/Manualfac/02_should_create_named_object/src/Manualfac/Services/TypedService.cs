using System;

namespace Manualfac.Services
{
    class TypedService : Service, IEquatable<TypedService>
    {
        #region Please modify the following code to pass the test

        /*
         * This class is used as a key for registration by serviceType.
         */
        private readonly Type serviceType;

        public TypedService(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        public bool Equals(TypedService other)
        {
            if (other == null)
            {
                return false;
            }
            return Equals(other.serviceType, serviceType);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((TypedService)obj);
        }

        public override int GetHashCode()
        {
            return serviceType.GetHashCode();
        }

        #endregion
    }
}
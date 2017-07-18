using System;

namespace Manualfac.Services
{
    class TypedNameService : Service, IEquatable<TypedNameService>
    {
        #region Please modify the following code to pass the test

        /*
         * This class is used as a key for registration by both type and name.
         */

        private readonly Type serviceType;
        private readonly string name;

        public TypedNameService(Type serviceType, string name)
        {
            this.serviceType = serviceType;
            this.name = name;
        }

        public bool Equals(TypedNameService other)
        {
            if (other == null)
            {
                return false;
            }
            return Equals(other.serviceType, serviceType) && string.Equals(other.name, name);
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
            return Equals((TypedNameService)obj);
        }

        public override int GetHashCode()
        {
            return serviceType.GetHashCode() ^ name.GetHashCode();
        }

    #endregion
}
}
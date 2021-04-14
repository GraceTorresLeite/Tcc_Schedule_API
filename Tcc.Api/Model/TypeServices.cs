using System;

namespace Tcc.Api.Model
{
    public enum TypeServices
    {
        SOBRANCELHA,
        BUÇO,
        VIRILHA,
        PERNA,
        AXILA,
        BARBA,
        PEITO
    }

    public class ServiceTypeWrapper
    {
        public static TypeServices? ServiceTypeById(int service)
        {

            foreach (var serviceType in Enum.GetValues(typeof(TypeServices)))
            {

                if (serviceType.GetHashCode() == service)
                {
                    return (TypeServices)serviceType;
                }
            }
            return null;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tcc.Api.Model
{
    public enum ServiceTypes
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
        public static ServiceTypes? ServiceTypeById(int service)
        {

            foreach (var serviceType in Enum.GetValues(typeof(ServiceTypes)))
            {

                if (serviceType.GetHashCode() == service)
                {
                    return (ServiceTypes)serviceType;
                }
            }
            return null;
        }
    }
}

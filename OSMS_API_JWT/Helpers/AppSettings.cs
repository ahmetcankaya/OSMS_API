using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSMS_API_JWT.Helpers
{
    public class AppSettings
    {
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }
    }
}

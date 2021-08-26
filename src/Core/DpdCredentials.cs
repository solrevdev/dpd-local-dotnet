using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Solrevdev.DpdLocalDotnet.Core
{
    /// <summary>
    /// A typed version of appsettings used for configuration, It is common to have development
    /// and production credentials when consuming the basic display api
    /// </summary>
    public class DpdCredentials
    {
        /*
        "Name": "Production - DPD API Credentials",
        "ApiUrl" : "api.dpdlocal.co.uk",
        "Username": "DSmith",
        "Password": "MYPassWd66",
        "AccountNumber": "830567"
        */
        /// <summary>
        /// The name for the configuration of this appsettings credentials.
        /// </summary>
        public string Name { get; set; } = "Production - DPD API Credentials";

        /// <summary>
        /// The base hostname and url used for the HTTP requests. Allows for separate urls for staging and production
        /// </summary>
        /// <value></value>
        public string ApiUrl { get; set; } = "api.dpdlocal.co.uk";

        /// <summary>
        /// The DPD username normally used to access DPD Local
        /// </summary>
        public string Username { get; set; } = "DSmith";

        /// <summary>
        /// The DPD password normally used to access DPD Local
        /// </summary>
        /// <value></value>
        public string Password { get; set; } = "MYPassWd66";

        /// <summary>
        /// The DPD account number normally used to access DPD Local
        /// </summary>
        public string AccountNumber { get; set; } = "830567";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farbank.Pattern.Nester.SageSpecs.Server.Services.D365
{
    public class D365OAuthConfiguration
    {
        //public Dictionary<string, OAuthSettings> Environments { get; set; } = new Dictionary<string, OAuthSettings>();
        //public string ActiveEnvironment { get; set; }

        public string BaseAddress_BlankScheduler { get; set; }
        public string Tenant { get; set; }
        public string ClientAppId { get; set; }
        public string ClientAppSecret { get; set; }


        //public Dictionary<string, OAuthSettings> Environments { get; set; } = new Dictionary<string, OAuthSettings>();
        //public string ActiveEnvironment { get; set; }

        //public string BaseAddress => Environments[ActiveEnvironment].BaseAddress;
        //public string Tenant => Environments[ActiveEnvironment].Tenant;
        //public string ClientAppId => Environments[ActiveEnvironment].ClientAppId;
        //public string ClientAppSecret => Environments[ActiveEnvironment].ClientAppSecret;

        //public class OAuthSettings
        //{

        //    public string BaseAddress { get; set; }
        //    public string Tenant { get; set; }
        //    public string ClientAppId { get; set; }
        //    public string ClientAppSecret { get; set; }
        //}


    }
}


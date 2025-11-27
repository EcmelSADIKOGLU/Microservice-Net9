using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice_Net9_.Shared.Options
{
    public class IdentityOption
    {
        public required string Address { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
    }
}

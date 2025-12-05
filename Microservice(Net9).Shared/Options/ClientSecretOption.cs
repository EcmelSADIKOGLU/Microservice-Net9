using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice_Net9_.Shared.Options
{
    public class ClientSecretOption
    {
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice_Net9_.Bus
{
    public class BusOption
    {
        public required string Address { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Port { get; set; }
    }
}

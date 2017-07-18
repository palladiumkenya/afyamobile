using System;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Model
{
    public class Device:Entity<Guid>
    {
        public string Serial { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
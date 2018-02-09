using System;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Presentation.DTO
{
    public class ProviderDTO
    {
        public Guid Id { get; set; }
        public string ProviderTypeId { get; set; }
        public string Code { get; set; }
        public Guid PracticeId { get; set; }
        public Guid PersonId { get; set; }
    }
}
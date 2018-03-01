using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Subject
{
    public class ClientSummary : Entity<Guid>
    {
        [MaxLength(100)]
        public string Area { get; set; }
        [MaxLength(100)]
        public string Report { get; set; }
        public DateTime? ReportDate { get; set; }
        public Decimal Rank { get; set; }
        public Guid ClientId { get; set; }

        public ClientSummary()
        {
            Id = LiveGuid.NewGuid();
        }

        public ClientSummary(string area, string report, DateTime? reportDate, decimal rank,Guid clientId) : this()
        {
            Area = area;
            Report = report;
            ReportDate = reportDate;
            Rank = rank;
            ClientId = clientId;
        }

        public override string ToString()
        {
            return $"{Area} | {Report} | {ReportDate?.Date} |{Rank}";
        }
    }
}
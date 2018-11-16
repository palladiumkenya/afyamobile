using System;
using LiveHTS.Core.Event;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Meta
{
    public class Region : Entity<int>
    {
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public int SubCountyId { get; set; }
        public string SubCountyName { get; set; }
        public int WardId { get; set; }
        public string WardName { get; set; }

        public Region()
        {
        }



        public string County()
        {
            return $"{CountyName}";
        }

        public string SubCounty()
        {
            return $"{SubCountyName}";
        }

        public string Ward()
        {
            return $"{WardName}";
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(CountyName))
                return CountyName;
            if (!string.IsNullOrWhiteSpace(SubCountyName))
                return SubCountyName;
            if (!string.IsNullOrWhiteSpace(WardName))
                return WardName;

            return string.Empty;
        }

       
    }
}
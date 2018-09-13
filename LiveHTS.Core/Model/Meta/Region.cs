using LiveHTS.SharedKernel.Model;

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
            return $"{WardName},{SubCountyName},{CountyName}";
        }
    }
}
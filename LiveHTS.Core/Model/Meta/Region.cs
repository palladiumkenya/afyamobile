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
            if (!string.IsNullOrWhiteSpace(CountyName))
                return CountyName;
            if (!string.IsNullOrWhiteSpace(SubCountyName))
                return SubCountyName;
            if (!string.IsNullOrWhiteSpace(WardName))
                return WardName;

            return $"{CountyName} {SubCountyName} {WardName}";
        }

        public static Region CreateCountyInitial(string display="Select County")
        {
            return new Region
            {
                CountyId = 0,
                CountyName = display
            };
        }
        public static Region CreateSubCountyInitial(string display = "Select SubCounty")
        {
            return new Region
            {
                SubCountyId = 0,
                SubCountyName = display
            };
        }

        public static Region CreateWardInitial(string display = "Select Ward")
        {
            return new Region
            {
                WardId = 0,
                WardName = display
            };
        }
    }
}
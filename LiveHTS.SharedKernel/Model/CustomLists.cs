using System.Collections.Generic;

namespace LiveHTS.SharedKernel.Model
{
    public class CustomLists
    {
        private static readonly List<CustomItem> _genderList = new List<CustomItem>
        {
            new CustomItem("M", "Male"),
            new CustomItem("F", "Female")
        };

        private static readonly List<CustomItem> _ageUnitList = new List<CustomItem>
        {
            new CustomItem("Y", "Years"),
            new CustomItem("M", "Months"),
            new CustomItem("D", "Days")
        };

        public static List<CustomItem> GenderList
        {
            get { return _genderList; }
        }
        public static List<CustomItem> AgeUnitList
        {
            get { return _ageUnitList; }
        }
    }
}

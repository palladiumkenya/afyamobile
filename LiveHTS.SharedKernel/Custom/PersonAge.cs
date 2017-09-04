namespace LiveHTS.SharedKernel.Custom
{
    public class PersonAge
    {
        private string _ageUnitDisplay = "Years";

        public decimal Age { get; set; }
        public string AgeUnit { get; set; }
        public string AgeUnitDisplay
        {
            get
            {
                if (AgeUnit == "Y")
                {
                    _ageUnitDisplay = "Years";
                }
                if (AgeUnit == "M")
                {
                    _ageUnitDisplay = "Months";
                }
                if (AgeUnit == "D")
                {
                    _ageUnitDisplay = "Days";
                }

                return _ageUnitDisplay;
            }
        }
        
        public decimal Years { get; set; }
        public decimal Months { get; set; }
        public decimal Days { get; set; }

        public PersonAge(decimal age) : this(age, "Y")
        {
        }
        private PersonAge(decimal age, string ageUnit)
        {
             Age = Years = age;
            AgeUnit = ageUnit;
        }

        public static PersonAge Create(decimal age, string ageUnit)
        {
            if (ageUnit == "Y")
            {
                return CreateFromYears(age);
            }

            if (ageUnit == "M")
            {
                return CreateFromMonths(age);
            }

            if (ageUnit == "D")
            {
                return CreateFromDays(age);
            }

            return new PersonAge(age);
        }
        public static PersonAge CreateFromYears(decimal years)
        {
            return new PersonAge(years, "Y");
        }
        public static PersonAge CreateFromMonths(decimal months)
        {
            return new PersonAge(months, "M");
        }
        public static PersonAge CreateFromDays(decimal days)
        {
            return new PersonAge(days, "D");
        }
        public string ToFullAgeString()
        {

            string ageString = string.Empty;

            if (Years >= 1)
            {
                if (Years == 1)
                {
                    ageString = string.Format("{0} Year", Years);
                }
                else
                {
                    ageString = string.Format("{0} Years", Years);
                }

                if (Months >= 1)
                {
                    if (Months == 1)
                    {
                        ageString += string.Format(" {0} Month", Months);
                    }
                    else
                    {
                        ageString += string.Format(" {0} Months", Months);
                    }

                    if (Days >= 1)
                    {
                        if (Days == 1)
                        {
                            ageString += string.Format(" {0} Day", Days);
                        }
                        else
                        {
                            ageString += string.Format(" {0} Days", Days);
                        }
                    }
                }
            }
            return ageString;
        }

        public override string ToString()
        {
            return $"{Age} {AgeUnitDisplay}";
        }
    }
}
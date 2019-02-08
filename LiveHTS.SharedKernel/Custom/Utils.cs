using System;

namespace LiveHTS.SharedKernel.Custom
{
    public static class Utils
    {
        public static string HasToEndWith(this string value, string end)
        {
            return value.EndsWith(end) ? value : $"{value}{end}";
        }

        public static DateTime CalculateBirthDate(PersonAge personAge)
        {
            var birthDate = DateTime.Today;
            var standardBirthDate = DateTime.Today;

            if (null != personAge && personAge.Age > 0)
            {
                int intAge = (int)Math.Round(personAge.Age, MidpointRounding.ToEven);
                switch (personAge.AgeUnit)
                {
                    case "Y": //Years
                        birthDate = DateTime.Today.AddYears(-intAge);
                        break;
                    case "M": //Months
                        birthDate = DateTime.Today.AddMonths(-intAge);
                        break;
                    case "D": //Days
                        birthDate = DateTime.Today.AddDays(-intAge);
                        break;
                }
                standardBirthDate =  new DateTime(birthDate.Year, 6, 15);
                if (standardBirthDate > DateTime.Today)
                    return birthDate;
            }
            return standardBirthDate.Date;
        }


        public static PersonAge CalculateAge(DateTime Bday)
        {
            return CalculateAge(Bday, DateTime.Today);
        }

        public static PersonAge CalculateAge(DateTime Bday, DateTime Cday)
        {
            var personAge = new PersonAge(0);

            if ((Cday.Year - Bday.Year) > 0 ||
                (((Cday.Year - Bday.Year) == 0) && ((Bday.Month < Cday.Month) ||
                                                    ((Bday.Month == Cday.Month) && (Bday.Day <= Cday.Day)))))
            {
                int DaysInBdayMonth = DateTime.DaysInMonth(Bday.Year, Bday.Month);
                int DaysRemain = Cday.Day + (DaysInBdayMonth - Bday.Day);

                if (Cday.Month > Bday.Month)
                {
                    personAge.Years = Cday.Year - Bday.Year;
                    personAge.Months = Cday.Month - (Bday.Month + 1) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    personAge.Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                }
                else if (Cday.Month == Bday.Month)
                {
                    if (Cday.Day >= Bday.Day)
                    {
                        personAge.Years = Cday.Year - Bday.Year;
                        personAge.Months = 0;
                        personAge.Days = Cday.Day - Bday.Day;
                    }
                    else
                    {
                        personAge.Years = (Cday.Year - 1) - Bday.Year;
                        personAge.Months = 11;
                        personAge.Days = DateTime.DaysInMonth(Bday.Year, Bday.Month) - (Bday.Day - Cday.Day);
                    }
                }
                else
                {
                    personAge.Years = (Cday.Year - 1) - Bday.Year;
                    personAge.Months = Cday.Month + (11 - Bday.Month) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    personAge.Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                }
            }
            else
            {
                throw new ArgumentException("Birthday date must be earlier than current date");
            }

            if (personAge.Years >= 1)
            {
                personAge.Age = personAge.Years;
                personAge.AgeUnit = "Y";
            }
            else
            {
                if (personAge.Months >= 1)
                {
                    personAge.Age = personAge.Months;
                    personAge.AgeUnit = "M";
                }
                else
                {
                    if (personAge.Days >= 1)
                    {
                        personAge.Age = personAge.Days;
                        personAge.AgeUnit = "D";
                    }
                }
            }
            return personAge;
        }

    }
}
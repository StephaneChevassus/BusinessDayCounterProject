using BusinessDayCounterLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessDayCounterLibrary
{
    public static class BusinessDayCounter
    {
        /// <summary>
        /// Caculates the number of weekdays between two dates
        /// </summary>
        public static int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            //Compare supplied dates
            if(ValidateInputDates(firstDate, secondDate))
            {
                return 0;
            }

            //Get the date range between the two dates excluding firstDate and secondDate
            var dateRange = DayCounterHelper.GetDateRangeBetweenTwoDates(firstDate, secondDate);

            //Count the number of dates that are a weekday "Monday, Tuesday, Wednesday, Thursday, Friday"
            var weekdayCount = DayCounterHelper.GetWeekdays(dateRange).Count();

            //Return the count
            return weekdayCount;
        }

        /// <summary>
        /// Caculates the number of business days between two dates given a list of public holidays
        /// </summary>
        public static double BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            //Compare supplied dates
            if(ValidateInputDates(firstDate, secondDate))
            {
                return 0;
            }

            //Get the date range between the two dates excluding firstDate and secondDate
            var dateRange = DayCounterHelper.GetDateRangeBetweenTwoDates(firstDate, secondDate);

            //Count the number of dates that are a business day
            //"Monday, Tuesday, Wednesday, Thursday, Friday" but excluding full public holidays and included the half day portion of a half public holiday

            //When comparing Hour, it uses the 24 format therefore it is safe to assume that 12 means 12pm otherwise it would be 00
            var halfDayPublicHolidays = publicHolidays.Where(d => d.Hour == 12);
            var fullDayPublicHolidays = publicHolidays.Where(d => d.Hour != 12);


            //First get the true full business days (a half day is not a full business day, so we need to pass the half day list)
            var fullBusinessDays = DayCounterHelper.GetFullBusinessDays(dateRange, fullDayPublicHolidays, halfDayPublicHolidays);

            //Then get the true half business days
            var halfBusinessDays = DayCounterHelper.GetHalfBusinessDays(dateRange, fullDayPublicHolidays, halfDayPublicHolidays);

            double fullBusinessDaysCount = fullBusinessDays.Count();

            double halfBusinessDaysCount = ((double)halfBusinessDays.Count() / 2);

            var businessDayCount = fullBusinessDaysCount + halfBusinessDaysCount;

            //Return the count
            return businessDayCount;
        }

        /// <summary>
        /// Caculates the number of business days between two dates given a set of rules that define public holidays
        /// </summary>
        public static double BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHolidaysRules> publicHolidaysRules)
        {
            //Compare supplied dates
            if(ValidateInputDates(firstDate, secondDate))
            {
                return 0;
            }

            List<DateTime> publicHolidays = new List<DateTime>();

            //Given a list of public holidays rules
            foreach(var rule in publicHolidaysRules)
            {
                //Generate a list of public holidays within the date range supplied
                var newPublicHolidays = rule.GetPublicHolidays(firstDate, secondDate);
                publicHolidays.AddRange(newPublicHolidays);
            }

            //Then re-use the method from task 2 to get the business days between the two dates given a list of public holidays
            var businessDayCount = BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);

            //Return the count
            return businessDayCount;
        }

        /// <summary>
        /// Validates input dates.
        /// firstDate must be earlier than secondDate
        /// </summary>
        /// <returns>bool</returns>
        private static bool ValidateInputDates(DateTime firstDate, DateTime secondDate)
        {
            return (secondDate.Date <= firstDate.Date);
        }
    }
}

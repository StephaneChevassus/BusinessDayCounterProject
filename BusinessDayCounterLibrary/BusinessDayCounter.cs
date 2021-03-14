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
        public static int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            //Compare supplied dates
            if(ValidateInputDates(firstDate, secondDate))
            {
                return 0;
            }

            //Get the date range between the two dates excluding firstDate and secondDate
            var dateRange = DayCounterHelper.GetDateRangeBetweenTwoDates(firstDate, secondDate);

            //Count the number of dates that are a business day
            //"Monday, Tuesday, Wednesday, Thursday, Friday" but excluding public holidays
            var businessDayCount = DayCounterHelper.GetBusinessDays(dateRange, publicHolidays.AsEnumerable()).Count();

            //Return the count
            return businessDayCount;
        }

        /// <summary>
        /// Caculates the number of business days between two dates given a set of rules that define public holidays
        /// </summary>
        public static int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHolidaysRules> publicHolidaysRules)
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

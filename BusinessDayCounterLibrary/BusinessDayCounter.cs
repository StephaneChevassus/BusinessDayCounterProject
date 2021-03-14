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
            if(secondDate.Date <= firstDate.Date)
            {
                return 0;
            }

            //Get the date range between the two dates excluding firstDate and secondDate
            var dateRange = DayCounterHelper.GetDateRangeBetweenTwoDates(firstDate, secondDate);

            //Count the number of dates that are a weekday "Monday, Tuesday, Wednesday, Thursday, Friday"
            var weekdayCount = dateRange
                .Where(d => d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday)
                .Count();

            //Return the count
            return weekdayCount;
        }

        /// <summary>
        /// Caculates the number of business days between two dates given a list of public holidays
        /// </summary>
        public static int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            //Compare supplied dates
            if(secondDate.Date <= firstDate.Date)
            {
                return 0;
            }

            //Get the date range between the two dates excluding firstDate and secondDate
            var dateRange = DayCounterHelper.GetDateRangeBetweenTwoDates(firstDate, secondDate);

            //Count the number of dates that are a business day
            //"Monday, Tuesday, Wednesday, Thursday, Friday" but excluding public holidays
            var businessDayCount = dateRange
                .Where(d => d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday)
                .Where(d => !publicHolidays.Any(p => d.Date.Equals(p.Date)))
                .Count();

            //Return the count
            return businessDayCount;
        }
    }
}

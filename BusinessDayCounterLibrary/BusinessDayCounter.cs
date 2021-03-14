using System;
using System.Collections.Generic;

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

            //Get the date range between the two dates excluding firstDate and secondDate

            //Count the number of dates that are a weekday "Monday, Tuesday, Wednesday, Thursday, Friday"

            //Return the count
            return -1;
        }

        /// <summary>
        /// Caculates the number of business days between two dates given a list of public holidays
        /// </summary>
        public static int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            //Compare supplied dates

            //Get the date range between the two dates excluding firstDate and secondDate

            //Count the number of dates that are a business day
            //"Monday, Tuesday, Wednesday, Thursday, Friday" but excluding public holidays

            //Return the count
            return -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessDayCounterLibrary.Helpers
{
    public static class DayCounterHelper
    {
        /// <summary>
        /// Gets a date range between two dates, excluding the start and end dates
        /// </summary>
        ///<exception cref="ArgumentException">Thrown when endDate is not greater than startDate</exception>
        public static IEnumerable<DateTime> GetDateRangeBetweenTwoDates(DateTime startDate, DateTime endDate)
        {
            if(endDate <= startDate)
                throw new ArgumentException("startDate must be prior to endDate");

            List<DateTime> dateRange = new List<DateTime>();

            //Exclude the startDate and endDate
            for(var dateTime = startDate.AddDays(1); dateTime < endDate; dateTime = dateTime.AddDays(1))
            {
                dateRange.Add(dateTime);
            }

            return dateRange.AsEnumerable();
        }

        /// <summary>
        /// Gets a list of weekdays from a list of dates
        /// </summary>
        public static IEnumerable<DateTime> GetWeekdays(IEnumerable<DateTime> dateRange)
        {
            return dateRange.Where(d => !d.IsDayOfWeekSaturday() && !d.IsDayOfWeekSunday());
        }

        /// <summary>
        /// Gets a list of business days from a list of dates excluding public holidays
        /// </summary>
        public static IEnumerable<DateTime> GetBusinessDays(IEnumerable<DateTime> dateRange, IEnumerable<DateTime> publicHolidays)
        {
            return GetWeekdays(dateRange).Where(d => !publicHolidays.Any(p => d.Date.Equals(p.Date)));
        }

        /// <summary>
        /// Moves the public holiday to the following Monday if it falls on a weekend
        /// </summary>
        public static DateTime MovePublicHolidayToMonday(DateTime publicHoliday)
        {
            if(publicHoliday.IsDayOfWeekSaturday())
            {
                publicHoliday = publicHoliday.AddDays(2);
            }
            else if(publicHoliday.IsDayOfWeekSunday())
            {
                publicHoliday = publicHoliday.AddDays(1);
            }
            return publicHoliday;
        }
    }

    public static class DateTimeExtensions
    {
        public static bool IsDayOfWeekSaturday(this DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday;
        }

        public static bool IsDayOfWeekSunday(this DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}

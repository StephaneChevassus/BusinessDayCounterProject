using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Gets a date based on a given occurrence
        /// e.g. 2nd Monday of June 2021
        /// </summary>
        /// <param name="year">2021</param>
        /// <param name="month">6</param>
        /// <param name="dayOfWeek">Monday</param>
        /// <param name="dayOfWeekOccurrence">2 (2nd day of the occurrence)</param>
        /// <returns></returns>
        public static DateTime GetDateByOccurrence(int year, int month, DayOfWeek dayOfWeek, int dayOfWeekOccurrence)
        {
            //Get a date range for the given month
            var dateRange = Enumerable.Range(1, DateTime.DaysInMonth(year, month)).Select(d => new DateTime(year, month, d));

            //Select all the dates matching the given day of the week and select the day corresponding to the occurrence
            var publicHoliday = dateRange.Where(d => d.DayOfWeek == dayOfWeek).ElementAt(dayOfWeekOccurrence - 1);

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

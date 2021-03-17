using BusinessDayCounterLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessDayCounterLibrary
{
    public abstract class PublicHolidaysRules
    {
        /// <summary>
        /// Gets public holidays between two dates based on the public holiday rule
        /// </summary>
        public abstract IEnumerable<DateTime> GetPublicHolidays(DateTime startDate, DateTime endDate);
    }

    /// <summary>
    /// Public holidays which are always on the same day, e.g. Anzac Day on April 25th every year.
    /// </summary>
    public sealed class FixedPublicHolidays : PublicHolidaysRules
    {
        //Anzac Day
        public DateTime AnzacDay(int year)
        {
            return new DateTime(year, 4, 25);
        }

        //Example of a Fixed Public Holiday that is a half day
        //A half public holiday is defined as a date with the time set to 12PM
        public DateTime HalfDay(int year)
        {
            //The 17th of March
            return new DateTime(year, 3, 17, 12, 0, 0);
        }

        public override IEnumerable<DateTime> GetPublicHolidays(DateTime startDate, DateTime endDate)
        {
            List<DateTime> publicHolidays = new List<DateTime>();

            for(var year = startDate.Year; year <= endDate.Year; year++)
            {
                publicHolidays.Add(AnzacDay(year));
                publicHolidays.Add(HalfDay(year));
            }

            return publicHolidays.AsEnumerable();
        }
    }

    /// <summary>
    /// Public holidays which are always on the same day, except when that falls on a weekend.
    /// e.g. New Year's Day on January 1st every year, unless that is a Saturday or Sunday, 
    /// in which case the holiday is the next Monday.
    /// </summary>
    public sealed class MovingPublicHolidays : PublicHolidaysRules
    {
        //New Year's Day
        public DateTime NewYearsDay(int year)
        {
            //Generate the date but move it to the following Monday if it falls on a weekend
            return DayCounterHelper.MovePublicHolidayToMonday(new DateTime(year, 1, 1));
        }

        //Example of a Moving Public Holiday that is a half day
        //A half public holiday is defined as a date with the time set to 12PM
        public DateTime HalfDay(int year)
        {
            //Generate the date but move it to the following Monday if it falls on a weekend
            //The 13th of March
            return DayCounterHelper.MovePublicHolidayToMonday(new DateTime(year, 3, 13, 12, 0, 0));
        }

        public override IEnumerable<DateTime> GetPublicHolidays(DateTime startDate, DateTime endDate)
        {
            List<DateTime> publicHolidays = new List<DateTime>();

            for(var year = startDate.Year; year <= endDate.Year; year++)
            {
                publicHolidays.Add(NewYearsDay(year));
                publicHolidays.Add(HalfDay(year));
            }

            return publicHolidays.AsEnumerable();
        }
    }

    /// <summary>
    /// Public holidays on a certain occurrence of a certain day in a month. 
    /// e.g. Queen's Birthday on the second Monday in June every year.
    /// </summary>
    public sealed class OccurrencePublicHolidays : PublicHolidaysRules
    {
        //Queen's Birthday
        public DateTime QueensBirthday(int year)
        {
            //Generate a date based on an occurrence
            //0 represents 12AM hour
            return DayCounterHelper.GetDateByOccurrence(year, 6, DayOfWeek.Monday, 2, 0);
        }

        //Example of a Occurrence Public Holiday that is a half day
        //A half public holiday is defined as a date with the time set to 12PM
        public DateTime HalfDay(int year)
        {
            //Generate a date based on an occurrence
            //The 4th Monday of March
            return DayCounterHelper.GetDateByOccurrence(year, 3, DayOfWeek.Monday, 4, 12);
        }

        public override IEnumerable<DateTime> GetPublicHolidays(DateTime startDate, DateTime endDate)
        {
            List<DateTime> publicHolidays = new List<DateTime>();

            for(var year = startDate.Year; year <= endDate.Year; year++)
            {
                publicHolidays.Add(QueensBirthday(year));
                publicHolidays.Add(HalfDay(year));
            }

            return publicHolidays.AsEnumerable();
        }
    }
}

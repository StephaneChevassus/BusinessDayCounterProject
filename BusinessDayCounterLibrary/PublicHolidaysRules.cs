using BusinessDayCounterLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessDayCounterLibrary
{
    public abstract class PublicHolidaysRules
    {
    }

    /// <summary>
    /// Public holidays which are always on the same day, e.g. Anzac Day on April 25th every year.
    /// </summary>
    public class FixedPublicHolidays : PublicHolidaysRules
    {
        //Anzac Day
        public DateTime AnzacDay(int year)
        {
            return new DateTime(year, 4, 25);
        }
    }

    /// <summary>
    /// Public holidays which are always on the same day, except when that falls on a weekend.
    /// e.g. New Year's Day on January 1st every year, unless that is a Saturday or Sunday, 
    /// in which case the holiday is the next Monday.
    /// </summary>
    public class MovingPublicHolidays : PublicHolidaysRules
    {
        //New Year's Day
        public DateTime NewYearsDay(int year)
        {
            //Generate the date but move it to the following Monday if it falls on a weekend
            return DateTime.Now;
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
            return DateTime.Now;
        }
    }
}

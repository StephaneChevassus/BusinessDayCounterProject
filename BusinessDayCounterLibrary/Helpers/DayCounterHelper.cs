using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessDayCounterLibrary.Helpers
{
    public static class DayCounterHelper
    {
        /// <summary>
        /// Get a date range between two dates, excluding the start and end dates
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
    }
}

using BusinessDayCounterLibrary;
using BusinessDayCounterLibrary.Helpers;
using System;
using System.Globalization;
using System.Linq;
using Xunit;

namespace BusinessDayCounterTests
{
    public class TestBusinessDayCounter
    {
        /// <summary>
        /// Ensures that the weekdays between two dates are calculated correctly for Task 1
        /// </summary>
        [Theory]
        [InlineData("7/10/2013", "9/10/2013", 1)]
        [InlineData("5/10/2013", "14/10/2013", 5)]
        [InlineData("7/10/2013", "1/01/2014", 61)]
        [InlineData("7/10/2013", "5/10/2013", 0)]
        [InlineData("1/03/2021", "2/03/2021", 0)]
        public void Task1_Calculate_Weekdays_Between_TwoDates(string firstDate, string secondDate, int expectedCount)
        {
            //Given two dates
            var testFirstDate = DateTime.ParseExact(firstDate, "d/M/yyyy", CultureInfo.InvariantCulture);
            var testSecondtDate = DateTime.ParseExact(secondDate, "d/M/yyyy", CultureInfo.InvariantCulture);

            //When I count the weekdays between the two dates
            var actualCount = BusinessDayCounter.WeekdaysBetweenTwoDates(testFirstDate, testSecondtDate);

            //Then the count should exclude weekends
            Assert.Equal(expectedCount, actualCount);
        }

        /// <summary>
        /// Ensures that the business days between two dates are calculated correctly given a list of public holidays for Task 2
        /// </summary>
        [Theory]
        [InlineData("7/10/2013", "9/10/2013", 1)]
        [InlineData("24/12/2013", "27/12/2013", 0)]
        [InlineData("7/10/2013", "1/01/2014", 59)]
        public void Task2_Calculate_BusinessDays_Between_TwoDates(string firstDate, string secondDate, int expectedCount)
        {
            //Given two dates and a list of public holidays
            var publicHolidays = new[]
            {
                    new DateTime(2013, 12, 25),
                    new DateTime(2013, 12, 26),
                    new DateTime(2014, 1, 1)
            };
            var testFirstDate = DateTime.ParseExact(firstDate, "d/M/yyyy", CultureInfo.InvariantCulture);
            var testSecondtDate = DateTime.ParseExact(secondDate, "d/M/yyyy", CultureInfo.InvariantCulture);

            //When I count the business days between the two dates
            var actualCount = BusinessDayCounter.BusinessDaysBetweenTwoDates(testFirstDate, testSecondtDate, publicHolidays.ToList());

            //Then the count should exclude weekends and public holidays
            Assert.Equal(expectedCount, actualCount);
        }

        /// <summary>
        /// Ensures that the business days between two dates are calculated correctly given a list of public holidays rules for Task 3
        /// </summary>
        [Theory]
        [InlineData("31/12/2016", "2/01/2017", 0)]
        [InlineData("1/01/2017", "22/01/2017", 14)]
        [InlineData("22/04/2017", "28/04/2017", 3)]
        [InlineData("1/06/2017", "19/06/2017", 10)]
        public void Task3_Calculate_BusinessDays_Between_TwoDates(string firstDate, string secondDate, int expectedCount)
        {
            //Given two dates and a list of public holidays rules
            var publicHolidaysRules = new PublicHolidaysRules[]
            {
                    new FixedPublicHolidays(),
                    new MovingPublicHolidays(),
                    new OccurrencePublicHolidays()
            };
            var testFirstDate = DateTime.ParseExact(firstDate, "d/M/yyyy", CultureInfo.InvariantCulture);
            var testSecondtDate = DateTime.ParseExact(secondDate, "d/M/yyyy", CultureInfo.InvariantCulture);

            //When I count the business days between the two dates
            var actualCount = BusinessDayCounter.BusinessDaysBetweenTwoDates(testFirstDate, testSecondtDate, publicHolidaysRules.ToList());

            //Then the count should exclude weekends and public holidays
            Assert.Equal(expectedCount, actualCount);
        }

        /// <summary>
        /// Ensures that the generated range between two dates excludes the two dates
        /// </summary>
        [Theory]
        [InlineData("7/10/2013", "8/10/2013", 0)]
        [InlineData("7/10/2013", "9/10/2013", 1)]
        [InlineData("7/10/2013", "14/10/2013", 6)]
        public void GetDateRange_Between_TwoValidDates(string startDate, string endDate, int expectedCount)
        {
            //Given two dates
            var testStartDate = DateTime.ParseExact(startDate, "d/M/yyyy", CultureInfo.InvariantCulture);
            var testEndDate = DateTime.ParseExact(endDate, "d/M/yyyy", CultureInfo.InvariantCulture);

            //When I generate a range of dates between the two dates
            var actualCount = DayCounterHelper.GetDateRangeBetweenTwoDates(testStartDate, testEndDate).Count();

            //Then the count should exclude the two dates
            Assert.Equal(expectedCount, actualCount);
        }

        /// <summary>
        /// Ensures that the start date is prior to the end date when generating a date range
        /// </summary>
        [Theory]
        [InlineData("7/10/2013", "7/10/2013")]
        [InlineData("9/10/2013", "7/10/2013")]
        public void GetDateRange_Between_TwoInvalidDates(string startDate, string endDate)
        {
            //Given two dates with the startDate greater than the endDate
            var testStartDate = DateTime.ParseExact(startDate, "d/M/yyyy", CultureInfo.InvariantCulture);
            var testEndtDate = DateTime.ParseExact(endDate, "d/M/yyyy", CultureInfo.InvariantCulture);

            //When I generate a range of dates 
            Action act = () => DayCounterHelper.GetDateRangeBetweenTwoDates(testStartDate, testEndtDate);

            //Then I should not be able to generate a date range
            Assert.Throws<ArgumentException>(act);
        }
    }
}

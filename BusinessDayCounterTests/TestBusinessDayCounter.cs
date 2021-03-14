using BusinessDayCounterLibrary;
using System;
using System.Globalization;
using Xunit;

namespace BusinessDayCounterTests
{
    public class TestBusinessDayCounter
    {
        /// <summary>
        /// Ensure that the weekdays between two dates are calculated correctly for Task 1
        /// </summary>
        [Theory]
        [InlineData("7/10/2013", "9/10/2013", 1)]
        [InlineData("5/10/2013", "14/10/2013", 5)]
        [InlineData("7/10/2013", "1/10/2013", 61)]
        [InlineData("7/10/2013", "5/10/2013", 0)]
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
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTimeLibrary;
using Xunit;

namespace PMS.Tests
{
    using PMS.DAL;

    public class SumTimeCalculatorTest
    {
        [Fact]
        public void CalculateTimeTest()
        {
            //Arrange

            DateTime date1 = new DateTime(2017, 2, 6);
            DateTime date2 = new DateTime(2017, 2, 7);
            DateTime date3 = new DateTime(2017, 2, 8);
            DateTime date4 = new DateTime(2017, 2, 9);
            DateTime date5 = new DateTime(2017, 2, 10);

            //Act
            SumTimeCalculator sumTimeCalculator = new SumTimeCalculator();
            BusinessCalendarService businessCalendarService = new BusinessCalendarService();
            int sumTimeHours1 = sumTimeCalculator.CalculateTime(date1, date5, businessCalendarService);
            int sumTimeHours2 = sumTimeCalculator.CalculateTime(date1, date4, businessCalendarService);
            int sumTimeHours3 = sumTimeCalculator.CalculateTime(date2, date4, businessCalendarService);
            int sumTimeHours4 = sumTimeCalculator.CalculateTime(date2, date5, businessCalendarService);


            //Assert
            Assert.Equal(28, sumTimeHours1);
            Assert.Equal(21, sumTimeHours2);
            Assert.Equal(14, sumTimeHours3);
            Assert.Equal(21, sumTimeHours4);
        }
    }
}

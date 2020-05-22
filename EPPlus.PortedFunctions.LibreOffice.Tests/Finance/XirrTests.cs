using EPPlus.PortedFunctions.LibreOffice.Finance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace EPPlus.PortedFunctions.LibreOffice.Tests.Finance
{
    [TestClass]
    public class XirrTests
    {
        [TestMethod]
        public void XirrTest_1()
        {
            var values = new List<double>
            {
                -100,
                20,
                40,
                25
            };
            var dates = new List<DateTime>
            {
                new DateTime(2016, 01, 01),
                new DateTime(2016, 04, 01),
                new DateTime(2016, 10, 01),
                new DateTime(2017, 02, 01)
            };
            var result = XirrImpl.GetXirr(values, dates);
            var retVal = Math.Round(result.Result, 4);
            Assert.AreEqual(-0.1967, retVal);
        }

        [TestMethod]
        public void XirrTest_2()
        {
            var values = new List<double>
            {
                -100,
                20,
                40,
                25,
                8,
                15
            };
            var dates = new List<DateTime>
            {
                new DateTime(2016, 01, 01),
                new DateTime(2016, 04, 01),
                new DateTime(2016, 10, 01),
                new DateTime(2017, 02, 01),
                new DateTime(2017, 03, 01),
                new DateTime(2017, 06, 01)
            };
            var result = XirrImpl.GetXirr(values, dates);
            var retVal = Math.Round(result.Result, 4);
            Assert.AreEqual(0.0944, retVal);
        }

        [TestMethod]
        public void XirrTest_3()
        {
            var values = new List<double>
            {
                -10000,
                2750,
                4250,
                3250,
                2750
            };
            var dates = new List<DateTime>
            {
                new DateTime(2008, 01, 01),
                new DateTime(2008, 03, 01),
                new DateTime(2008, 10, 30),
                new DateTime(2009, 02, 15),
                new DateTime(2009, 04, 01)
            };
            var result = XirrImpl.GetXirr(values, dates, 0.1);
            var retVal = Math.Round(result.Result, 4);
            Assert.AreEqual(0.3734, retVal);
        }
    }
}

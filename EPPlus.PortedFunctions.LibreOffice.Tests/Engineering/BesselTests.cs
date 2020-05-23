using EPPlus.PortedFunctions.LibreOffice.Engineering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPlus.PortedFunctions.LibreOffice.Tests.Engineering
{
    [TestClass]
    public class BesselTests
    {
        [TestMethod]
        public void BesselJTest()
        {
            var func = new BesselJImpl();
            var result = func.BesselJ(2.5, 1);
            Assert.AreEqual(0.49709410, Math.Round((double)result.Result, 8));

            result = func.BesselJ(1.9, 2);
            Assert.AreEqual(0.329926, Math.Round((double)result.Result, 6));
        }

        [TestMethod]
        public void BesselITest()
        {
            var func = new BesselIimpl();
            var result = func.BesselI(4.5, 1);
            Assert.AreEqual(15.38922, Math.Round((double)result.Result, 5));

            result = func.BesselI(1.5, 1);
            Assert.AreEqual(0.98166643, Math.Round((double)result.Result, 8));

            result = func.BesselI(1.5, 2);
            Assert.AreEqual(0.3378346, Math.Round((double)result.Result, 7));
        }

        [TestMethod]
        public void BesselKTest()
        {
            var func = new BesselKImpl();
            var result = func.BesselK(0.05, 1);
            Assert.AreEqual(19.90967433, Math.Round((double)result.Result, 8));

            result = func.BesselK(1.5, 1);
            Assert.AreEqual(0.2773878, Math.Round((double)result.Result, 7));
        }

        [TestMethod]
        public void BesselYTest()
        {
            var func = new BesselYImpl();
            var result = func.BesselY(0.05, 1);
            Assert.AreEqual(-12.7898552, Math.Round((double)result.Result, 7));

            result = func.BesselY(2.5, 1);
            Assert.AreEqual(0.1459181, Math.Round((double)result.Result, 7));
        }
    }
}

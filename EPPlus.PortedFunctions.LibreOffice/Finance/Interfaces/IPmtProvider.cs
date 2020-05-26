using System;
using System.Collections.Generic;
using System.Text;

namespace EPPlus.PortedFunctions.LibreOffice.Finance.Interfaces
{
    public interface IPmtProvider
    {
        double GetPmt(double Rate, double NPer, double PV, double FV = 0, PmtDue Due = PmtDue.EndOfPeriod);
    }
}

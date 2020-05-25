﻿/*************************************************************************************************
  * This Source Code Form is subject to the terms of the Mozilla Public
  * License, v. 2.0. If a copy of the MPL was not distributed with this
  * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *************************************************************************************************
  Date               Author                       Change
 *************************************************************************************************
  05/20/2020         EPPlus Software AB       Implemented function
 *************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace EPPlus.PortedFunctions.LibreOffice.Engineering
{
    public class BesselIimpl : BesselBase
    {
        public FinanceCalcResult<double> BesselI(double x, int n)
        {
            const int nMaxIteration = 2000;
            double fXHalf = x / 2.0;
            if (n < 0)
                return new FinanceCalcResult<double>(PfErrorType.Num);

            double fResult = 0.0;

            /*  Start the iteration without TERM(n,0), which is set here.

                    TERM(n,0) = (x/2)^n / n!
             */
            int nK = 0;
            double fTerm = 1.0;
            // avoid overflow in Fak(n)
            for (nK = 1; nK <= n; ++nK)
            {
                fTerm = fTerm / nK * fXHalf;
            }
            fResult = fTerm;    // Start result with TERM(n,0).
            if (fTerm != 0.0)
            {
                nK = 1;
                const double fEpsilon = 1.0E-15;
                do
                {
                    /*  Calculation of TERM(n,k) from TERM(n,k-1):

                                           (x/2)^(n+2k)
                            TERM(n,k)  =  --------------
                                            k! (n+k)!

                                           (x/2)^2 (x/2)^(n+2(k-1))
                                       =  --------------------------
                                           k (k-1)! (n+k) (n+k-1)!

                                           (x/2)^2     (x/2)^(n+2(k-1))
                                       =  --------- * ------------------
                                           k(n+k)      (k-1)! (n+k-1)!

                                           x^2/4
                                       =  -------- TERM(n,k-1)
                                           k(n+k)
                    */
                    fTerm = fTerm * fXHalf / nK * fXHalf / (nK + n);
                    fResult += fTerm;
                    nK++;
                }
                while ((Math.Abs(fTerm) > Math.Abs(fResult) * fEpsilon) && (nK < nMaxIteration));

            }
            return new FinanceCalcResult<double>(fResult);
        }
    }
}

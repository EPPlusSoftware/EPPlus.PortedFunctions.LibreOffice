﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EPPlus.PortedFunctions.LibreOffice.Engineering
{
    public class BesselYImpl : BesselBase
    {
        static PfCalcResult<double> Bessely0(double fX)
        {
            if (fX <= 0)
                return new PfCalcResult<double>(PfErrorType.Num);
            const double fMaxIteration = 9000000.0; // should not be reached
            if (fX > 5.0e+6) // iteration is not considerable better then approximation
                return new PfCalcResult<double>(Math.Sqrt(1 / f_PI / fX)
                        * (Math.Sin(fX) - Math.Cos(fX)));
            const double epsilon = 1.0e-15;
            const double EulerGamma = 0.57721566490153286060;
            double alpha = Math.Log(fX / 2.0) + EulerGamma;
            double u = alpha;

            double k = 1.0;
            double g_bar_delta_u = 0.0;
            double g_bar = -2.0 / fX;
            double delta_u = g_bar_delta_u / g_bar;
            double g = -1.0 / g_bar;
            double f_bar = -1 * g;

            double sign_alpha = 1.0;
            bool bHasFound = false;
            k = k + 1;
            do
            {
                double km1mod2 = (k - 1.0) % 2.0;
                double m_bar = (2.0 * km1mod2) * f_bar;
                if (km1mod2 == 0.0)
                    alpha = 0.0;
                else
                {
                    alpha = sign_alpha * (4.0 / k);
                    sign_alpha = -sign_alpha;
                }
                g_bar_delta_u = f_bar * alpha - g * delta_u - m_bar * u;
                g_bar = m_bar - (2.0 * k) / fX + g;
                delta_u = g_bar_delta_u / g_bar;
                u = u + delta_u;
                g = -1.0 / g_bar;
                f_bar = f_bar * g;
                bHasFound = (Math.Abs(delta_u) <= Math.Abs(u) * epsilon);
                k = k + 1;
            }
            while (!bHasFound && k < fMaxIteration);
            if (!bHasFound)
                return new PfCalcResult<double>(PfErrorType.Num); // not likely to happen
            return new PfCalcResult<double>(u * f_2_DIV_PI);
        }

        // See #i31656# for a commented version of this implementation, attachment #desc6
        // https://bz.apache.org/ooo/attachment.cgi?id=63609
        /// @throws IllegalArgumentException
        /// @throws NoConvergenceException
        static PfCalcResult<double> Bessely1(double fX)
        {
            if (fX <= 0)
                return new PfCalcResult<double>(PfErrorType.Num);
            const double fMaxIteration = 9000000.0; // should not be reached
            if (fX > 5.0e+6) // iteration is not considerable better then approximation
                return new PfCalcResult<double>(-Math.Sqrt(1 / f_PI / fX)
                        * (Math.Sin(fX) + Math.Cos(fX)));
            const double epsilon = 1.0e-15;
            const double EulerGamma = 0.57721566490153286060;
            double alpha = 1.0 / fX;
            double f_bar = -1.0;
            double u = alpha;
            double k = 1.0;
            alpha = 1.0 - EulerGamma - Math.Log(fX / 2.0);
            double g_bar_delta_u = -alpha;
            double g_bar = -2.0 / fX;
            double delta_u = g_bar_delta_u / g_bar;
            u = u + delta_u;
            double g = -1.0 / g_bar;
            f_bar = f_bar * g;
            double sign_alpha = -1.0;
            bool bHasFound = false;
            k = k + 1.0;
            do
            {
                double km1mod2 = (k - 1.0) % 2.0;
                double m_bar = (2.0 * km1mod2) * f_bar;
                double q = (k - 1.0) / 2.0;
                if (km1mod2 == 0.0) // k is odd
                {
                    alpha = sign_alpha * (1.0 / q + 1.0 / (q + 1.0));
                    sign_alpha = -sign_alpha;
                }
                else
                    alpha = 0.0;
                g_bar_delta_u = f_bar * alpha - g * delta_u - m_bar * u;
                g_bar = m_bar - (2.0 * k) / fX + g;
                delta_u = g_bar_delta_u / g_bar;
                u = u + delta_u;
                g = -1.0 / g_bar;
                f_bar = f_bar * g;
                bHasFound = (Math.Abs(delta_u) <= Math.Abs(u) * epsilon);
                k = k + 1;
            }
            while (!bHasFound && k < fMaxIteration);
            if (!bHasFound)
                new PfCalcResult<double>(PfErrorType.Num);
            return new PfCalcResult<double>(-u * 2.0 / f_PI);
        }


        public PfCalcResult<double> BesselY(double fNum, int nOrder)
        {
            switch (nOrder)
            {
                case 0: return Bessely0(fNum);
                case 1: return Bessely1(fNum);
                default:
                    {
                        double fTox = 2.0 / fNum;
                        var y0Result = Bessely0(fNum);
                        if (y0Result.HasError) return y0Result;
                        double fBym = y0Result.Result;
                        var y1Result = Bessely1(fNum);
                        if (y1Result.HasError) return y1Result;
                        double fBy = y1Result.Result;

                        for (int n = 1; n < nOrder; n++)
                        {
                            var fByp = n * fTox * fBy - fBym;
                            fBym = fBy;
                            fBy = fByp;
                        }

                        return new PfCalcResult<double>(fBy);
                    }
            }
        }
    }
}
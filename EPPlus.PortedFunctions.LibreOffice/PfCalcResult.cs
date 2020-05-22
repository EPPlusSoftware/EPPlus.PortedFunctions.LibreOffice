using System;
using System.Collections.Generic;
using System.Text;

namespace EPPlus.PortedFunctions.LibreOffice
{
    public class PfCalcResult<T>
    {
        public PfCalcResult(T result)
        {
            Result = result;
            if (result is double)
            {
                DataType = PfDataType.Decimal;
            }
            else if (result is int)
            {
                DataType = PfDataType.Integer;
            }
            else if (result is System.DateTime)
            {
                DataType = PfDataType.Date;
            }
            else
            {
                DataType = PfDataType.Unknown;
            }
        }
        public PfCalcResult(T result, PfDataType dataType)
        {
            Result = result;
            DataType = dataType;
        }

        public PfCalcResult(PfErrorType error)
        {
            HasError = true;
            ExcelErrorType = error;
        }

        public T Result { get; private set; }

        public PfDataType DataType { get; private set; }

        public bool HasError
        {
            get; private set;
        }

        public PfErrorType ExcelErrorType { get; private set; }
    }
}

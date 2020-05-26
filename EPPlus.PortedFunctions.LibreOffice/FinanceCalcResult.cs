using System;
using System.Collections.Generic;
using System.Text;

namespace EPPlus.PortedFunctions.LibreOffice
{
    public class FinanceCalcResult<T>
    {
        public FinanceCalcResult(T result)
        {
            Result = result;
            if (result is double)
            {
                DataType = DataType.Decimal;
            }
            else if (result is int)
            {
                DataType = DataType.Integer;
            }
            else if (result is System.DateTime)
            {
                DataType = DataType.Date;
            }
            else
            {
                DataType = DataType.Unknown;
            }
        }
        public FinanceCalcResult(T result, DataType dataType)
        {
            Result = result;
            DataType = dataType;
        }

        public FinanceCalcResult(eErrorType error)
        {
            HasError = true;
            ExcelErrorType = error;
        }

        public T Result { get; private set; }

        public DataType DataType { get; private set; }

        public bool HasError
        {
            get; private set;
        }

        public eErrorType ExcelErrorType { get; private set; }
    }
}

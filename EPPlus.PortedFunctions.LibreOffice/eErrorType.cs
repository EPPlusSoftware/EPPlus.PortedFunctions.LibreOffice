using System;
using System.Collections.Generic;
using System.Text;

namespace EPPlus.PortedFunctions.LibreOffice
{
    /// <summary>
    /// Represents the errortypes in excel
    /// </summary>
    public enum eErrorType
    {
        /// <summary>
        /// Division by zero
        /// </summary>
        Div0,
        /// <summary>
        /// Not applicable
        /// </summary>
        NA,
        /// <summary>
        /// Name error
        /// </summary>
        Name,
        /// <summary>
        /// Null error
        /// </summary>
        Null,
        /// <summary>
        /// Num error
        /// </summary>
        Num,
        /// <summary>
        /// Reference error
        /// </summary>
        Ref,
        /// <summary>
        /// Value error
        /// </summary>
        Value
    }
}

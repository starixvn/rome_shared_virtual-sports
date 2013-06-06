namespace Fanex.PTVirtualSports.DataAccess
{
    using System;
    using System.Data;
    using System.Data.Common;
    using Fanex.PTEngine.BusinessObjects;
    using Microsoft.Practices.EnterpriseLibrary.Data;

    /// <summary>
    /// DataAccessBase abstract class
    /// </summary>
    public abstract class DataAccessBase
    {
        /// <summary>
        /// AddResultParams method
        /// </summary>
        /// <param name="db">db param</param>
        /// <param name="command">command param</param>
        public void AddResultParams(Database db, DbCommand command)
        {
            db.AddOutParameter(command, "errcode", DbType.String, 300);
            db.AddOutParameter(command, "errmsg", DbType.String, 0xfa0);
        }

        /// <summary>
        /// GetResultParams method
        /// </summary>
        /// <param name="db">db param</param>
        /// <param name="command">command param</param>
        /// <returns>return Result</returns>
        public Result GetResultParams(Database db, DbCommand command)
        {
            object parameterValue = db.GetParameterValue(command, "errcode");
            parameterValue = (parameterValue is DBNull) ? string.Empty : Convert.ToString(parameterValue);
            object obj3 = db.GetParameterValue(command, "errmsg");
            obj3 = (obj3 is DBNull) ? string.Empty : Convert.ToString(obj3);
            return new Result((string)parameterValue, (string)obj3);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Fanex.PTVirtualSports.Extension
{
    public static class DataRowExtension
    {
        public static bool HasColumn(this DataRow row, string columnName)
        {
            return row.Table.Columns.Contains(columnName);
        }

        public static string ToString(this DataRow row, string columnName)
        {
            if (row.HasColumn(columnName))
            {
                return row[columnName].ToString();
            }

            return string.Empty;
        }

        public static int ToInt32(this DataRow row, string columnName)
        {
            if (row.HasColumn(columnName))
            {
                return Convert.ToInt32(row[columnName]);
            }

            return 0;
        }

        public static decimal ToDecimal(this DataRow row, string columnName)
        {
            if (row.HasColumn(columnName))
            {
                return Convert.ToDecimal(row[columnName]);
            }

            return 0;
        }

        public static bool ToBoolean(this DataRow row, string columnName)
        {
            if (row.HasColumn(columnName))
            {
                return Convert.ToBoolean(row[columnName]);
            }

            return false;
        }

        public static bool? ToNullableBool(this DataRow row, string columnName)
        {
            if (row.HasColumn(columnName))
            {
                return (bool)row[columnName];
            }

            return null;
        }

        public static decimal? ToNullableDecimal(this DataRow row, string columnName)
        {
            if (row.HasColumn(columnName))
            {
                if (row[columnName] == DBNull.Value)
                    return null;
                return (decimal)row[columnName];
            }

            return null;
        }
    }
}
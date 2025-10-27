using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.MISAAttributes
{
    /// <summary>
    /// Attribute dùng để gán tên bảng trong database cho class entity.
    /// Mapping giữa tên class trong code và tên bảng trong DB.
    /// </summary>
    public class TableAttribute : Attribute
    {
        public string TableName { get; set; }
        public TableAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}

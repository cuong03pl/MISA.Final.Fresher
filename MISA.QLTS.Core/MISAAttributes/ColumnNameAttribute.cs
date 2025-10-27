using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.MISAAttributes
{
    /// <summary>
    /// Hiển thị tên cột trong database tương ứng với thuộc tính của class entity
    /// </summary>
    /// CreatedBy: HKC (27/10/2025)
    public class ColumnNameAttribute : Attribute
    {
        public string Name { get; set; }
        public ColumnNameAttribute(string name)
        {
            Name = name;
        }
    }
}

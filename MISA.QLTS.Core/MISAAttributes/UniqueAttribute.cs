using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.MISAAttributes
{
    /// <summary>
    /// Attribute đánh dấu thuộc tính là duy nhất
    /// </summary>
    public class UniqueAttribute : Attribute
    {
        public string Name { get; set; }
        public UniqueAttribute(string name) {
            Name = name;
        }
    }
}

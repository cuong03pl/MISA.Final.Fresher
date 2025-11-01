using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.MISAAttributes
{
    /// <summary>
    /// Dùng để đánh dấu tiền tố của mã tài sản
    /// </summary>
    /// CreatedBy: HKC (01/11/2025)
    public class PrefixAttribute : Attribute
    {
        public string Name { get; set; }
        public PrefixAttribute(string name)
        {
            Name = name;
        }
    }
}

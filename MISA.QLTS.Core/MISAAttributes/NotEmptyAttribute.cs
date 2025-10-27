using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.MISAAttributes
{
    /// <summary>
    /// Custom attribute dùng đánh giấu thuộc tính không được để trống
    /// </summary>
    /// CreatedBy: HKC (27/10/2025)
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmptyAttribute : Attribute
    {
        public string Name { get; set; }
        public NotEmptyAttribute(string name) {
            Name = name;
        }
    }
}

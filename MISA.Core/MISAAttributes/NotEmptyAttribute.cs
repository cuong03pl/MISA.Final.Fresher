using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.MISAAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    //Custom attribute dùng đánh giấu thuộc tính không được để trống
    public class NotEmptyAttribute : Attribute
    {
        public string Name { get; set; }
        public NotEmptyAttribute(string name) {
            Name = name;
        }
    }
}

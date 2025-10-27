using MISA.Core.MISAAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Thông tin bộ phận sử dụng tài sản
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Id bộ phận
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã bộ phận
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên bộ phận
        /// </summary>
        [NotEmptyAttribute("Tên bộ phận")]
        public string DepartmentName { get; set; }
    }
}

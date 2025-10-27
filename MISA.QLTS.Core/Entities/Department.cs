using MISA.Core.MISAAttributes;
using MISA.QLTS.Core.MISAAttributes;
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
        [ColumnNameAttribute("department_id")]
        [PrimaryKey]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã bộ phận
        /// </summary>
        [ColumnNameAttribute("department_code")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên bộ phận
        /// </summary>
        [NotEmptyAttribute("Tên bộ phận")]
        [ColumnNameAttribute("department_name")]
        public string DepartmentName { get; set; }
    }
}

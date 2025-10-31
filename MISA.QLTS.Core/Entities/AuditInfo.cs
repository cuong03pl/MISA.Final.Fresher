using MISA.QLTS.Core.MISAAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Thông tin theo dõi thay đổi bản ghi
    /// </summary>
    public class AuditInfo
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        [ColumnNameAttribute("created_date")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// 
        [ColumnNameAttribute("created_by")]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// 
        [ColumnNameAttribute("modified_date")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// 
        [ColumnNameAttribute("modified_by")]
        public string? ModifiedBy { get; set; }
    }
}

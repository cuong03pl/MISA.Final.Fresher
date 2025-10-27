using MISA.Core.MISAAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Thông tin loại tài sản
    /// </summary>
    public class AssetType : AuditInfo
    {
        /// <summary>
        /// Id loại tài sản
        /// </summary>
        public Guid AssetTypeId { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string AssetTypeCode { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string AssetTypeName { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int LifeTime { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        public decimal DepreciationRate { get; set; }

    }
}

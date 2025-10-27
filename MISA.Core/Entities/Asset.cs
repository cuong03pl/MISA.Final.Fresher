using MISA.Core.MISAAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Thông tin tài sản
    /// </summary>
    [TableAttribute("asset")]
    public class Asset : AuditInfo
    {
        /// <summary>
        /// Id tài sản
        /// </summary>
        public Guid AssetId { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string AssetCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string AssetName { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// Số lượng (số nguyên dương)
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Nguyên giá (VNĐ)
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Giá trị hao mòn năm (tự động tính)
        /// </summary>
        public decimal? AnnualDepreciation { get; set; }

        /// <summary>
        /// Id bộ phận sử dụng
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Id loại tài sản
        /// </summary>
        public Guid AssetTypeId { get; set; }
    }
}

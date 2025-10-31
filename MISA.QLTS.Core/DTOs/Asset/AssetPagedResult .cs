using MISA.QLTS.Core.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.DTOs.Asset
{
    public class AssetPagedResult : PagedResult<AssetDto>
    {
        /// <summary>
        /// Tổng số lượng
        /// </summary>
        public decimal QuantityTotal { get; set; }

        /// <summary>
        /// Tổng nguyên giá 
        /// </summary>
        public decimal PriceTotal { get; set; }

        /// <summary>
        /// Tổng khấu hao năm 
        /// </summary>
        public decimal AnnualDepreciationTotal { get; set; }

        /// <summary>
        /// Tổng giá trị còn lại
        /// </summary>
        public decimal RemainingValueTotal { get; set; }
    }
}

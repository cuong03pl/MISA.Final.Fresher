using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.DTOs.Paging
{
    /// <summary>
    ///  Chứa kết quả phân trang dùng chung.
    /// </summary>
    /// <typeparam name="T">Loại dữ liệu của bản ghi</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Dữ liệu danh sách phân trang
        /// </summary>
        public IEnumerable<T> Data { get; set; } = new List<T>();

        /// <summary>
        /// Tổng số tất cả bản ghi
        /// </summary>
        public int TotalRecords { get; set; }
    }
}

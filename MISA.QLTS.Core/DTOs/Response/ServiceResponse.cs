using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.DTOs.Response
{
    /// <summary>
    /// Kết quả trả về từ API
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu trả về</typeparam>
    /// CreatedBy: HKC (04/11/2025)
    public class ServiceResponse<T>
    {
        /// <summary>
        /// Trạng thái
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Thông báo cho người dùng
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Tạo response thành công
        /// </summary>
        /// <param name="data">Dữ liệu trả về</param>
        /// <param name="message">Thông báo</param>
        /// <returns>Response thành công</returns>
        public static ServiceResponse<T> Ok(T? data, string message = "Thành công")
        {
            return new ServiceResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Tạo response lỗi
        /// </summary>
        /// <param name="message">Thông báo</param>
        /// <param name="data">Dữ liệu trả về</param>
        /// <returns>Response lỗi</returns>
        public static ServiceResponse<T> Error(string message = "Thất bại", T data = default)
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Message = message,
                Data = data
            };
        }

        
    }
}

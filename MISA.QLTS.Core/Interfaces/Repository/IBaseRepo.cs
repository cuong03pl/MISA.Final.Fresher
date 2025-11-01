using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    /// <summary>
    /// Interface base repository cho tất cả các entity.
    /// </summary>
    /// <typeparam name="T">Loại entity</typeparam>
    public interface IBaseRepo<T>
    {
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// CreatedBy: HKC (27/10/2025)
        IEnumerable<T> GetAll();


        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id bản ghi cần lấy</param>
        /// <returns>Bản ghi</returns>
        /// CreatedBy: HKC (27/10/2025)
        T GetById(Guid entityId);


        /// <summary>
        /// Xử lý thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu cần thêm</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        /// CreatedBy: HKC (27/10/2025)
        int Insert(T entity);


        /// <summary>
        /// Xử lý cập nhật bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu thay đổi</param>
        /// <param name="entityId">Id của bản ghi cần thay đổi</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        /// CreatedBy: HKC (27/10/2025)
        int Update(T entity, Guid entityId);


        /// <summary>
        /// Xử lý xóa bản ghi
        /// </summary>
        /// <param name="entityId">Id của bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        /// CreatedBy: HKC (27/10/2025)
        int Delete(Guid entityId);

        


        /// <summary>
        /// Xử lý xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityId">Id của các bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        /// CreatedBy: HKC (30/10/2025)
        int DeleteMutiple(List<Guid> entityIds);


        /// <summary>
        /// Xử lý check mã đã tồn tại chưa
        /// </summary>
        /// <param name="entity">Dữ liệu cần kiểm tra</param>
        /// <param name="id">Id dữ liệu hiện tại cần kiểm tra</param>
        /// <returns>1 - Trùng, 0 - Không trùng</returns>
        /// CreatedBy: HKC (27/10/2025)
        void CheckCodeExist(T entity, Guid? id);

        /// <summary>
        /// Hàm sinh mã mới theo tiền tố
        /// Công thức: Lấy cái mới nhất ra sau đó + 1 
        /// </summary>
        /// <returns>Mã tài sản mới</returns>
        /// CreatedBy: HKC (01/11/2025)
        string GenerateNewCode();

    }
}

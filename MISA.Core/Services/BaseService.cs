using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    /// <summary>
    /// Base service cho tất cả các entity.
    /// </summary>
    /// <typeparam name="T">Loại entity</typeparam>

    public class BaseService<T> : IBaseService<T>
    {
       readonly IBaseRepo<T> _baseRepo;
        public BaseService(IBaseRepo<T> baseRepo) {
            _baseRepo = baseRepo;
        }


        /// <summary>
        /// Xử lý xóa bản ghi
        /// </summary>
        /// <param name="entityId">Id của bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        public int Delete(Guid entityId)
        {
            return _baseRepo.Delete(entityId);
        }

        /// <summary>
        /// Lấy tất cả bản ghi của entity T
        /// </summary>
        /// <returns>Danh sách tất cả các bản ghi</returns>
        public IEnumerable<T> GetAll()
        {
            return _baseRepo.GetAll();
        }


        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id bản ghi cần lấy</param>
        /// <returns>Bản ghi</returns>
        public T GetById(Guid entityId)
        {
            return _baseRepo.GetById(entityId);
        }


        /// <summary>
        /// Xử lý thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu cần thêm</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        public int Insert(T entity)
        {
            return _baseRepo.Insert(entity);
        }


        /// <summary>
        /// Xử lý cập nhật bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu thay đổi</param>
        /// <param name="entityId">Id của bản ghi cần thay đổi</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        public int Update(T entity, Guid entityId)
        {
            return _baseRepo.Update(entity, entityId);
        }
    }
}

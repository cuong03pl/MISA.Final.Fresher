using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Service;
using MISA.Core.MISAAttributes;
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
    /// CreatedBy: HKC (27/10/2025)

    public class BaseService<T> : IBaseService<T>
    {
        readonly IBaseRepo<T> _baseRepo;
        public BaseService(IBaseRepo<T> baseRepo)
        {
            _baseRepo = baseRepo;
        }


        /// <summary>
        /// Xử lý xóa bản ghi
        /// </summary>
        /// <param name="entityId">Id của bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        /// CreatedBy: HKC (27/10/2025)
        public int Delete(Guid entityId)
        {
            return _baseRepo.Delete(entityId);
        }

        /// <summary>
        /// Lấy tất cả bản ghi của entity T
        /// </summary>
        /// <returns>Danh sách tất cả các bản ghi</returns>
        /// CreatedBy: HKC (27/10/2025)
        public IEnumerable<T> GetAll()
        {
            return _baseRepo.GetAll();
        }


        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id bản ghi cần lấy</param>
        /// <returns>Bản ghi</returns>
        /// CreatedBy: HKC (27/10/2025)
        public T GetById(Guid entityId)
        {
            return _baseRepo.GetById(entityId);
        }


        /// <summary>
        /// Xử lý thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu cần thêm</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        /// CreatedBy: HKC (27/10/2025)
        public int Insert(T entity)
        {
            //CustomValidate(entity);
            //ValidateData(entity);
            return _baseRepo.Insert(entity);
        }


        /// <summary>
        /// Xử lý cập nhật bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu thay đổi</param>
        /// <param name="entityId">Id của bản ghi cần thay đổi</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        /// CreatedBy: HKC (27/10/2025)
        public int Update(T entity, Guid entityId)
        {
            CustomValidate(entity);
            ValidateData(entity);
            return _baseRepo.Update(entity, entityId);
        }

        /// <summary>
        /// Hàm validate dùng chung
        /// </summary>
        /// <param name="entity">Dữ liệu cần validate</param>
        /// CreatedBy: HKC (27/10/2025)
        public void ValidateData(T entity)
        {
            // Lấy tất cả các property của entity có attribute NotEmpty
            var props = entity.GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(NotEmptyAttribute)));

            // Lặp qua tất cả các props
            foreach (var prop in props)
            {
                // Lấy giá trị của prop
                var propValue = prop.GetValue(entity);
                var nameDisplay = prop.Name;
                // Lấy tất cả attribute gắn trên prop
                var attributes = prop.GetCustomAttributes(typeof(NotEmptyAttribute), true);
                if (attributes.Length > 0)
                {
                    nameDisplay = ((NotEmptyAttribute)attributes[0]).Name;
                }
                // Kiểm tra nếu giá trị là null hoặc chuỗi rỗng thì ném ra ex
                if (propValue == null || (string.IsNullOrEmpty(propValue.ToString())))
                {
                    throw new ValidateException($"{nameDisplay} không được để trống");
                }
            }
        }

        /// <summary>
        /// Hàm validate theo custom riêng cho từng entity
        /// </summary>
        /// <param name="entity">Dữ liệu cần validate</param>
        /// CreatedBy: HKC (28/10/2025)
        public virtual void CustomValidate(T entity)
        {
           
        }

        /// <summary>
        /// Xử lý xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityIds">Danh sách id bản ghi cần xóa</param>
        /// <returns></returns>
        public int DeleteMutiple(List<Guid> entityIds)
        {
          return  _baseRepo.DeleteMutiple(entityIds);
        }


        /// <summary>
        /// Hàm sinh mã mới theo tiền tố
        /// Công thức: Lấy cái mới nhất ra sau đó + 1 
        /// </summary>
        /// <returns>Mã tài sản mới</returns>
        /// CreatedBy: HKC (01/11/2025)
        public string GenerateNewCode()
        {
            return _baseRepo.GenerateNewCode();
        }
    }
}

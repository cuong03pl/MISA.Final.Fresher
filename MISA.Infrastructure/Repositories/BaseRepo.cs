using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Interfaces.Repository;
using MISA.Core.MISAAttributes;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repositories
{
    public class BaseRepo<T> : IBaseRepo<T>
    {
        protected readonly string connectionString;
        protected MySqlConnection SqlConnection;
        public BaseRepo(IConfiguration configuration) { 
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        /// <summary>
        /// Lấy tất cả bản ghi của bảng trong database
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        public IEnumerable<T> GetAll()
        {
            var tableName = GetTableName();
            string sql = @$"SELECT * FROM {tableName}";
            using (SqlConnection = new MySqlConnection(connectionString))
            {
                var data = SqlConnection.Query<T>(sql).ToList();
                return data;
            }
             
        }


        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id bản ghi cần lấy</param>
        /// <returns>Bản ghi</returns>
        public T GetById(Guid entityId)
        {
            var tableName = GetTableName();
            string sql = @$"SELECT * FROM {tableName} WHERE {tableName}_id = @{tableName}_id";
            using (SqlConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{tableName}_id", entityId);
                var data = SqlConnection.Query<T>(sql, parameters).FirstOrDefault();
                return data;
            }
        }

        
        /// <summary>
        /// Xử lý thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu cần thêm</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        public int Insert(T entity)
        {
            var tableName = GetTableName();
           
            using (SqlConnection = new MySqlConnection(connectionString))
            {
                return 1;
            }
        }


        /// <summary>
        /// Xử lý cập nhật bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu thay đổi</param>
        /// <param name="entityId">Id của bản ghi cần thay đổi</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        public int Update(T entity, Guid entityId)
        {
            var tableName = GetTableName();

            using (SqlConnection = new MySqlConnection(connectionString))
            {
                return 1;
            }
        }


        /// <summary>
        /// Xử lý xóa bản ghi
        /// </summary>
        /// <param name="entityId">Id của bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        public int Delete(Guid entityId)
        {
            var tableName = GetTableName();
            string sql = @$"DELETE * FROM {tableName} WHERE {tableName}_id = @{tableName}_id";
            using (SqlConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{tableName}_id", entityId);
                var data = SqlConnection.Execute(sql, parameters);
                return data;
            }
        }


        /// <summary>
        /// Lấy tên bảng trong database từ attribute gán trên class entity
        /// </summary>
        /// <returns>Tên bảng trong DB</returns>
        /// CreatedBy: HKC (27/10/2025)
        public static string GetTableName()
        {
            var type = typeof(T);
            var tableName = type.Name;
            var attr = (TableAttribute?)Attribute.GetCustomAttribute(type, typeof(TableAttribute));
            if (attr != null)
            {
                tableName = attr.TableName;
            }
            return tableName;
        }
    }
}

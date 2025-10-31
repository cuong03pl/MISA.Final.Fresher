using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.MISAAttributes;
using MISA.QLTS.Core.MISAAttributes;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

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
        /// CreatedBy: HKC (27/10/2025)
        public virtual IEnumerable<T> GetAll()
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
        /// CreatedBy: HKC (27/10/2025)
        public T GetById(Guid entityId)
        {
            var tableName = GetTableName();
            var columnName = GetPrimaryKeyColumn();
            string sql = @$"SELECT * FROM {tableName} WHERE {columnName} = @Id";
            using (SqlConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@Id", entityId);
                var data = SqlConnection.Query<T>(sql, parameters).FirstOrDefault();
                return data;
            }
        }


        /// <summary>
        /// Xử lý thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu cần thêm</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        /// CreatedBy: HKC (27/10/2025)
        public int Insert(T entity)
        {
            
            CheckCodeExist(entity);
            var tableName = GetTableName();
            var props = entity.GetType().GetProperties();

            // Tạo danh sách cột và giá trị, bỏ qua PK nếu cần
            var columns = new List<string>();
            var values = new List<string>();
            DynamicParameters parameters = new DynamicParameters();

            foreach (var prop in props)
            {
                var primaryKeyAttr = prop.GetCustomAttribute<PrimaryKey>();
                bool isPK = primaryKeyAttr != null;

                // Nếu là Guid PK tự sinh Guid
                if (isPK)
                {
                    prop.SetValue(entity, Guid.NewGuid());
                }

                // Lấy tên cột từ attribute nếu có
                var columnAttr = prop.GetCustomAttribute<ColumnNameAttribute>();
                string columnName = columnAttr != null ? columnAttr.Name : prop.Name;

                columns.Add(columnName);
                values.Add("@" + columnName);
                parameters.Add("@" + columnName, prop.GetValue(entity));
            }

            var sql = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)})";

            using (var conn = new MySqlConnection(connectionString))
            {
                return conn.Execute(sql, parameters);
            }
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
            CheckCodeExist(entity, entityId);
            var tableName = GetTableName();
            var props = entity.GetType().GetProperties();
            var setClauses = new List<string>();
            DynamicParameters parameters = new DynamicParameters();

            string primaryKeyColumn = null;
            object primaryKeyValue = null;

            foreach (var prop in props)
            {
                // Lấy attribute PrimaryKey 
                var primaryKeyAttr = prop.GetCustomAttribute<PrimaryKey>();
                var columnAttr = prop.GetCustomAttribute<ColumnNameAttribute>();
                string columnName = columnAttr != null ? columnAttr.Name : prop.Name;

                var propValue = prop.GetValue(entity);

               
                if (primaryKeyAttr != null)
                {
                    primaryKeyColumn = columnName;
                    primaryKeyValue = entityId;
                    continue;  // Nếu là khóa chính thì không update mà chỉ dùng để where
                }
                setClauses.Add($"{columnName} = @{columnName}");
                parameters.Add("@" + columnName, propValue);
            }
            parameters.Add("@" + primaryKeyColumn, primaryKeyValue);

            var sql = $"UPDATE {tableName} SET {string.Join(", ", setClauses)} WHERE {primaryKeyColumn} = @{primaryKeyColumn}";

            using (var conn = new MySqlConnection(connectionString))
            {
                return conn.Execute(sql, parameters);
            }
        }


        /// <summary>
        /// Xử lý xóa bản ghi
        /// </summary>
        /// <param name="entityId">Id của bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        /// CreatedBy: HKC (27/10/2025)
        public int Delete(Guid entityId)
        {
            var tableName = GetTableName();
            var columnName = GetPrimaryKeyColumn();
            string sql = @$"DELETE FROM {tableName} WHERE {columnName} = @Id";
            using (SqlConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@Id", entityId);
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
            return tableName.ToLower();
        }

        /// <summary>
        /// Kiểm tra mã đã tồn tại chưa
        /// </summary>
        /// <param name="entity">Dữ liệu mới cần thêm hoặc sửa</param>
        /// <exception cref="ValidateException"></exception>
        /// CreatedBy: 
        /// HKC (28/10/2025)
        public void CheckCodeExist(T entity, Guid? id = null)
        {
            var tableName = GetTableName();

            // Lấy các props có attribute Unique
            var props = entity.GetType().GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(UniqueAttribute))).ToList();

            if (!props.Any())
                return;

            var setClauses = new List<string>();
            var fieldNames = new List<string>();
            DynamicParameters parameters = new DynamicParameters();
            // Lặp qua các props để lấy giá trị và ColumnNameAttribute
            foreach (var prop in props)
            {
                var columnAttr = prop.GetCustomAttribute<ColumnNameAttribute>();
                var uniqueAttr = prop.GetCustomAttribute<UniqueAttribute>();
                string columnName = columnAttr != null ? columnAttr.Name : prop.Name;
                var propValue = prop.GetValue(entity);
                
                setClauses.Add($"{columnName} = @{columnName}");
                parameters.Add("@" + columnName, propValue);
                fieldNames.Add(uniqueAttr.Name);
            }

            var sql = $"SELECT COUNT(1) FROM {tableName} WHERE {string.Join(" OR ", setClauses)}";

            // Nếu là cập nhật thì loại trừ bản ghi hiện tại bằng cách thêm điều kiện AND PrimaryKey != id
            var pkProp = entity.GetType().GetProperties()
            .FirstOrDefault(p => Attribute.IsDefined(p, typeof(PrimaryKey)));
            if (id != null && pkProp != null)
            {
                var pkColumn = pkProp.GetCustomAttribute<ColumnNameAttribute>()?.Name ?? pkProp.Name;
                sql += $" AND {pkColumn} <> @id";
                parameters.Add("@id", id);
            }

            using (SqlConnection = new MySqlConnection(connectionString))
            {
                var data = SqlConnection.ExecuteScalar<int>(sql, parameters);
                if(data > 0)
                {
                    var fieldName = fieldNames.Count == 1
                ? fieldNames[0]
                : string.Join(", ", fieldNames.Select(p => p));
                    throw new ValidateException($"{fieldName} đã tồn tại trong hệ thống");
                }

            }

        }

        /// <summary>
        /// Xử lý xóa nhiều bản ghi
        /// </summary>
        /// <param name="entityIds">Danh sách id bản ghi cần xóa</param>
        /// <returns></returns>
        public int DeleteMutiple(List<Guid> entityIds)
        {
           var tableName = GetTableName();
              var columnName = GetPrimaryKeyColumn();

            DynamicParameters parameters = new DynamicParameters();
            string sql = @$"DELETE FROM {tableName} WHERE {columnName} IN @Ids";
            using (SqlConnection = new MySqlConnection(connectionString))
            {
                parameters.Add($"@Ids", entityIds);
                var data = SqlConnection.Execute(sql, parameters);
                return data;
            }
        }


        /// <summary>
        /// Lấy tên cột khóa chính của entity dựa theo attribute
        /// </summary>
        /// <returns>Tên cột trong database</returns>
        /// <exception cref="Exception">Nếu không tìm thấy khóa chính</exception>
        public static string GetPrimaryKeyColumn()
        {
            var tableName = GetTableName();
            var type = typeof(T);
            var PKProp = type.GetProperties().FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(PrimaryKey)));

            var columnAttr = PKProp.GetCustomAttribute<ColumnNameAttribute>();
            var columnName = columnAttr?.Name ?? PKProp.Name.ToLower();
            return columnName;
        }

    }
}

using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MS2_DoTrongDuc.DL
{
    public class DbConnector
    {
        // = "User Id=nvmanh;Host=103.124.92.43;Character Set=utf8;Database=MS2_08_DoTrongDuc;Port=3306;Password=12345678"
        public static string connectionString = "User Id=root;Host=127.0.0.1;Character Set = utf8; Database=ms2_08_dotrongduc;Port=3306;Password=alo123123";

        IDbConnection dbConnection;

        public DbConnector()
        {
            dbConnection = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Lấy tất cả dữ liệu trong bảng có tên TEntity
        /// </summary>
        /// <typeparam name="TEntity">Kiểu đối tượng cần truy xuất</typeparam>
        /// <returns></returns>
        public IEnumerable<TEntity> GettAllData<TEntity>()
        {
            var tableName = typeof(TEntity).Name;
            var entities = dbConnection.Query<TEntity>($"Proc_Get{tableName}s", tableName, commandType: CommandType.StoredProcedure);
            return entities;
        }

        /// <summary>
        /// Get data theo commandText
        /// </summary>
        /// <typeparam name="T">Kiểu object cần lấy</typeparam>
        /// <param name="commandText">câu lệnh command</param>
        /// <returns>mảng các object lấy được từ db</returns>
        public IEnumerable<T> GetDataByCommand<T>(string commandText)
        {
            return dbConnection.Query<T>(commandText);
        }

        /// <summary>
        /// Get 1 đối tượng bằng commandtext
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <param name="commandText">dòng cmd</param>
        /// <returns></returns>
        public object GetDataByCommand1<T>(string commandText)
        {
            return dbConnection.Query<T>(commandText).FirstOrDefault();
        }

        /// <summary>
        /// Lấy dữ liệu kiểu T bằng Procedure Store trong db
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu cần lấy</typeparam>
        /// <param name="procName">Tên của Procedure</param>
        /// <param name="parameters">Danh sách tham số</param>
        /// <returns>Trả về danh sách dữ liệu kiểu IEnumerable</returns>
        public IEnumerable<T> GetDataByProcStore<T>(string procName, object parameters)
        {
            var entities = dbConnection.Query<T>(procName, parameters, commandType: CommandType.StoredProcedure);
            return entities;
        }

        /// <summary>
        /// Lấy dữ liệu kiểu T bằng Procedure Store trong db
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu T</typeparam>
        /// <param name="procName">Tên của Procedure</param>
        /// <returns>Trả về danh sách dữ liệu kiểu IEnumerable</returns>
        public IEnumerable<T> GetDataByProcStore<T>(string procName)
        {
            var entities = dbConnection.Query<T>(procName, commandType: CommandType.StoredProcedure);
            return entities;
        }

        /// <summary>
        /// Tính toán bản ghi đầu tiên bằng procedure kiểu int
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu</typeparam>
        /// <param name="procName">Tên procedure</param>
        /// <returns>Dữ liệu kiểu int chứa thông tin cần lấy</returns>
        public int GetIntByProcStore<T>(string procName)
        {
            var number = dbConnection.Query<T>(procName, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return int.Parse(number.ToString());
        }

        /// <summary>
        /// Lấy TEntity theo Id
        /// </summary>
        /// <typeparam name="TEntity">Kiểu dữ liệu trả về</typeparam>
        /// <param name="id">GUID của TEntity</param>
        /// <returns></returns>
        public TEntity GetById<TEntity>(string id)
        {
            var tableName = typeof(TEntity).Name;

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{tableName}Id", id);

            var entity = dbConnection.Query<TEntity>($"Proc_Get{tableName}ById", dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return (TEntity)entity;
        }

        /// <summary>
        /// Thêm 1 TEntity vào db
        /// </summary>
        /// <typeparam name="TEntity">Employee || Department || Jobs</typeparam>
        /// <param name="entity">Đối tượng thêm</param>
        /// <returns>Kết quả trả về số lượng bản ghi được thêm vào trong db</returns>
        public int Insert<TEntity>(TEntity entity)
        {
            var tableName = typeof(TEntity).Name;
            var storeName = $"Proc_Insert{tableName}";

            DynamicParameters param = new DynamicParameters();

            var properties = typeof(TEntity).GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                param.Add($"@{propertyName}", propertyValue);
            }
            var affects = dbConnection.Execute(storeName, param, commandType: CommandType.StoredProcedure);
            return affects;
        }


        /// <summary>   
        /// Kiểm tra sự tồn tại của đối tượng T có trường property mang giá trị value (string) trong db
        /// </summary>
        /// <typeparam name="T1">Đối tượng cần kiểm tra</typeparam>
        /// <typeparam name="T2">Kiểu dữ liệu của value</typeparam>
        /// <param name="property">Tên thuộc tính</param>
        /// <param name="value">Giá trị thuộc tính kiểu string</param>
        /// <returns>Trả về true nếu có tồn tại ít nhất 1 bản ghi thỏa mãn, false nếu không tồn tại bản ghi nào</returns>
        public bool CheckExist<T1, T2>(string property, T2 value)
        {
            var nameType = typeof(T1).Name;
            string sql = $"Select * From {nameType} where {property} = '{value}'";
            return dbConnection.Query<T1>(sql).Count() > 0;
        }

        /// <summary>
        /// Kiểm tra sự tồn tại của đối tượng T có trường property mang giá trị value ( khác string) trong db
        /// </summary>
        /// <typeparam name="T1">Đối tượng cần kiểm tra</typeparam>
        /// <typeparam name="T2">Kiểu dữ liệu của thuộc tính cần kiểm tra</typeparam>
        /// <param name="property">Tên thuộc tính</param>
        /// <param name="value">Giá trị thuộc tính kiểu string</param>
        /// <returns>Trả về true nếu có tồn tại ít nhất 1 bản ghi thỏa mãn, false nếu không tồn tại bản ghi nào</returns>
        public bool CheckExistNotString<T1, T2>(string property, T2 value)
        {
            var nameType = typeof(T1).Name;
            var sql = $"Select * From {nameType} Where {property} = {value}";
            return dbConnection.Query<T1>(sql).Count() > 0;
        }

        /// <summary>
        /// Xóa 1 đối tượng theo Id
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng</typeparam>
        /// <param name="id">Id của đối tượng đó</param>
        /// <returns>số đối tượng xóa được</returns>
        public int DeleteById<T>(string id)
        {
            var tableName = typeof(T).Name;
            var sql = $"Delete From {tableName} Where {tableName}Id = '{id}'";
            return dbConnection.Execute(sql);
        }


        /// <summary>
        /// Cập nhật đối tượng
        /// </summary>
        /// <typeparam name="T">Kiểu đối tượng cần cập nhật</typeparam>
        /// <param name="entity">giá trị đối tượng cần cập nhật</param>
        /// <returns>số lượng đối tượng cập nhật thành công</returns>
        public int Update<T>(T entity)
        {
            var tableName = typeof(T).Name;
            var storeName = $"Proc_Update{tableName}";

            DynamicParameters param = new DynamicParameters();

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                param.Add($"@{propertyName}", propertyValue);
            }
            return dbConnection.Execute(storeName, param, commandType: CommandType.StoredProcedure);
        }
    }
}

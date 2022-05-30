using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ProductService
    {

        private static string db_source = "mysqlserver30001.database.windows.net";
        private static string db_user = "sysadmin";
        private static string db_pwd = "Acc123456789";
        private static string db_database = "mysqldb";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_pwd;
            _builder.InitialCatalog= db_database;

            return new SqlConnection(_builder.ConnectionString);

        }


        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();

            List<Product> _productList = new List<Product>();
            string statement = "select ProductId,ProductName,Quantity from Products;";

            conn.Open();

            SqlCommand cmd = new SqlCommand(statement, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while(reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    _productList.Add(product);
                }

            }
            conn.Close();

            return _productList;
        }


    
    
    
    }
}

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ASPinClass.Models;

namespace ASPInClass
{
    public class ProductRepository
    {                                                      // dont push this until we add correct connectstring

        private static string connectionString = System.IO.File.ReadAllText("ConnectionString.txt");
        public List<Product> GetAllProducts()           // so far we hav one method to get a list of all products
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Products;";

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                List<Product> allProducts = new List<Product>();

                while (reader.Read() == true)                       // whole there are records
                {                                                                 // to read
                    Product currentProduct = new Product();
                    currentProduct.ID = reader.GetInt32("ProductID");
                    currentProduct.Name = reader.GetString("Name");
                    currentProduct.Price = reader.GetDecimal("Price");

                    allProducts.Add(currentProduct);   // adds products to list
                }
                return allProducts;                 // returns list
            }
        }

        public Product GetProduct(int id)
        {

            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Products WHERE ProductID = @id;";
            cmd.Parameters.AddWithValue("id", id);   // note the quotation marks

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                var product = new Product();        // instance of the product class

                while (reader.Read() == true)
                {
                    product.ID = reader.GetInt32("ProductID");
                    product.Name = reader.GetString("Name");
                    product.Price = reader.GetDecimal("Price");
                    product.CategoryID = reader.GetInt32("CategoryID");
                    product.OnSale = reader.GetInt32("OnSale");
                    product.StockLevel = reader.GetString("StockLevel");
                }
                return product;
            }
        }
             public void UpdateProduct(Product productToUpdate)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "UPDATE products SET Name = @name, Price =@price WHERE ProductID = @id"; 
                

            cmd.Parameters.AddWithValue("name", productToUpdate.Name); 
            cmd.Parameters.AddWithValue("price", productToUpdate.Price);
            cmd.Parameters.AddWithValue("id", productToUpdate.ID);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }


        }
    }
}


using System;
using ASPInClass.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ModelViewController.Models;
namespace ASPInClass
{
    public class ProductRepository
    {

        private static string connectionString = "Server=localhost;Database=bestbuy;uid=root;Pwd=password";
        public List<Product> GetAllProducts()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateComand();
            cmd.CommandText = "SELECT * FROM Products;";

            using (conn)
            {
                conn.Open();
                MySqlDataReadwe reader = cmd.ExecuteReader();

                List<Product> allProducts = new List<Product>();

                while (reader.Read() == true)                       // whole there are records
                {                                                                 // to read
                    Product currentProduct = new Product();
                    currentProduct.ID = reader.GetInt32("Product ID");
                    currentProduct.Name = reader.GetString("Name");
                    currentProduct.Price = reader.GetDecimal("Price");

                    allProducts.Add(currentProduct);   // adds products to list
                }
                return allProducts;                 // returns list
            }


        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPinClass.Models;
using ASPInClass;
using Microsoft.AspNetCore.Mvc;


namespace ASPinClass.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()                       // method takes us to products view index
        {                                                     // relates to cshtml file 
            ProductRepository repo = new ProductRepository();
            List<Product> products = repo.GetAllProducts();           // can pass in a list of products

            return View(products);                    // view must have same name as method   see views
        }
        public IActionResult ViewProduct(int id)
        {
            ProductRepository repo = new ProductRepository();
            Product product = repo.GetProduct(id);

            return View(product);
        }

        public IActionResult UpdateProduct(int id)
        {
            ProductRepository repo = new ProductRepository();
            Product prod = repo.GetProduct(id);

            repo.UpdateProduct(prod);

            if(prod == null)
            {
                return View("ProductNotFound");
            }
            return View(prod);
        }

        public IActionResult UpdateProductToDatabase(Product product)
        {
            ProductRepository repo = new ProductRepository();
            repo.UpdateProduct(product);

            return RedirectToAction("ViewProduct", new { id = product.ID });

        }
    }
}
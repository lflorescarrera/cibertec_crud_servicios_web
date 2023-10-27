using AppCRUDMVC.Database;
using AppCRUDMVC.Database.ProductContext;
using AppCRUDMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AppCRUDMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductContext _productContext;
        public ProductController(ProductContext productContext)
        {
            this._productContext = productContext;
        }

        public IActionResult List()
        {
            var lstProd = _productContext.Products.ToList();
            ProductListViewModel model = new ProductListViewModel();
            model.List = (from c in lstProd
                          select new ProductViewModel()
                          {
                              Id = c.Id,
                              Name = c.Name,
                              Stock = c.Stock,
                              Status = c.Status,
                              Price = c.Price,
                          }).ToList();
            return View(model);
        }

        public IActionResult Add()
        {
            ProductViewModel model = new ProductViewModel();
            return View(model);
        }

        public IActionResult AddSave(ProductViewModel model)
        {
            ProductEntity entity = new ProductEntity();
            entity.Name = model.Name;
            entity.Stock = model.Stock;
            entity.Price = model.Price;
            entity.DateAdded = DateTime.Now;
            entity.Status = "A";
            _productContext.Products.Add(entity);
            _productContext.SaveChanges();
            return RedirectToAction("List", "Product");
        }

        public IActionResult Edit(int id)
        {
            var findProd = _productContext.Products.Where(c => c.Id == id).SingleOrDefault();
            var model = new ProductViewModel();
            model.Id = findProd.Id;
            model.Name = findProd.Name;
            model.Price = findProd.Price;
            model.Stock = findProd.Stock;
            return View(model);
        }

        [HttpPost]
        public IActionResult EditSave(ProductViewModel model)
        {
            var findProd = _productContext.Products.SingleOrDefault(c => c.Id == model.Id);
            if (findProd != null)
            {
                findProd.Name = model.Name;
                findProd.Price = model.Price;
                findProd.Stock = model.Stock;
                _productContext.SaveChanges();
            }
            return RedirectToAction("List", "Product");
        }

        [HttpGet]
        public JsonResult RemoveProduct(int id)
        {
            var findProd = _productContext.Products.SingleOrDefault(c => c.Id == id);
            _productContext.Products.Remove(findProd);
            _productContext.SaveChanges();
            return Json("Se eliminó correctamente");
        }

        [HttpGet]
        public JsonResult GetProductDetail(int id)
        {
            var product = _productContext.Products.Where(c => c.Id == id).SingleOrDefault();
            return Json(new
            {
                ProductName = product.Name,
                ProductPrice = product.Price,
                ProductStock = product.Stock
            });
        }
    }
}

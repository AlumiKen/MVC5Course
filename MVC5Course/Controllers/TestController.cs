using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class TestController : Controller
    {
        FabricsEntities db = new FabricsEntities();

        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EDE()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EDE(EDEViewModel data)
        {
            return View(data);
        }

        public ActionResult CreateProduct()
        {            
            Product product = new Product();
            product.ProductName = "Alumi  Ken";
            product.Price = 400;
            product.Active = true;
            product.Stock = 600;

            db.Products.Add(product);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception (ex.ToString());
            }

            //return View(product);
            return RedirectToAction("ReadProduct");
        }

        public ActionResult ReadProduct(bool? Active)
        {
            //單筆與多筆型別不同, 傳給View會有差異 , 要特別注意!!
            //var data = db.Products.Find(1);

            //var data = db.Products.AsQueryable();

            var data = db.Products
                .Where(p => p.ProductId > 1550)
                .OrderByDescending(p => p.Price).AsQueryable();

            if (Active.HasValue)
            {
                data = data.Where(p => p.Active == Active);
            }

            return View(data);
        }

        public ActionResult OneProduct(int id)
        {
            var data = db.Products.Find(id);
            //data = db.Products.FirstOrDefault(p => p.ProductId == id);
            //data = db.Products.Where(p => p.ProductId == id).FirstOrDefault();

            return View(data);
        }

        public ActionResult UpdateProduct(int id)
        {
            var data = db.Products.Find(id);

            if (data == null)
            {
                return HttpNotFound();
            }

            data.Price = data.Price * 2;

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityError in ex.EntityValidationErrors)
                {
                    foreach (var err in entityError.ValidationErrors)
                    {
                        return Content(err.PropertyName + " : " + err.ErrorMessage);
                    }
                }
            }

            return RedirectToAction("ReadProduct");
        }

        public ActionResult DeleteProduct(int id)
        {
            var data = db.Products.Find(id);

            #region
            //因為Product有設定FK給OrderLine, 若要刪除的Product有被OrderLine對應, 刪除時就會發生錯誤
            //所以需要先處理OrderLine的資料刪除, 以下為刪除Product有對應到OrderLine資料的寫法

            //基本寫法
            //foreach (var item in data.OrderLines.ToList())
            //{
            //    db.OrderLines.Remove(item);
            //}

            //進階寫法
            db.OrderLines.RemoveRange(data.OrderLines);
            #endregion

            db.Products.Remove(data);
            db.SaveChanges();

            return RedirectToAction("ReadProduct");
        }

    
        public ActionResult ProductView()
        {
            //此方式是直接執行, 不會回傳資料, 通常用在update, delete, 改善原本用迴圈逐項刪除的效能
            //var data = db.Database.ExecuteSqlCommand(
            //    "DELETE FROM Product WHERE ProductId = @p0", id);            

            var data = db.Database.SqlQuery<ProductViewModel>(
                "SELECT * FROM Product WHERE Active = @p0 AND ProductName LIKE @p1", true, "%Yellow%");

            return View(data);
        }

        public ActionResult ProductSP()
        {
            var data = db.GetProduct(true, "%Yellow%");

            return View(data);
        }
    }
}
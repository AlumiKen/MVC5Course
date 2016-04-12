using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{    
    [Authorize]
    public class ProductsController : BaseController
    {        
        //private FabricsEntities db = new FabricsEntities();

        // GET: Products
        public ActionResult Index()
        {
            //View.Model = repoProduct.All() 等同於return View(repoProduct.All())

            return View(repoProduct.All().Take(5));
        }

        [HttpPost]
        public ActionResult Index(IList<BatchUpdateProduct> data)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var product = repoProduct.Find(item.ProductId);
                    product.Price = item.Price;
                    product.Active = item.Active;
                    product.Stock = item.Stock;
                }

                repoProduct.UnitOfWork.Commit();
                TempData["EditProductMsg"] = data.Count + " 筆資料批次更新成功";
                return RedirectToAction("Index");
            }

            ViewData.Model = repoProduct.All().Take(5);
            return View();            
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult OrderLine(int ProductId)
        {
            return PartialView(repoProduct.Find(ProductId).OrderLines);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repoProduct.Add(product);
                repoProduct.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]// <= action filter/selector
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        public ActionResult Edit(int id, FormCollection form)
        {
            var products = repoProduct.Find(id);

            //模型繫結延遲驗證
            //ModelState.AddModelError
            if (TryUpdateModel(products, new string[] { "ProductId","ProductName","Active","Price","Stock" }))
            {
                //var dbProduct = (FabricsEntities)repoProduct.UnitOfWork.Context;
                //dbProduct.Entry(product).State = EntityState.Modified;
                repoProduct.UnitOfWork.Commit();

                TempData["EditProductMsg"] = products.ProductName + " 更新成功";
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repoProduct.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = repoProduct.Find(id);
            repoProduct.Delete(product);
            repoProduct.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var dbProduct = (FabricsEntities)repoProduct.UnitOfWork.Context;
                dbProduct.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Northwind.Controllers
{
    public class ProductController : ApiController
    {
        NorthwindEntities db = new NorthwindEntities();
        // GET: Product
        [HttpGet]
        //https://localhost:44325/api/product/ProductById/1
        public Product ProductById(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Products.FirstOrDefault(x => x.ProductID == id);
        }

        [HttpGet]
        //https://localhost:44325/api/product/ProductBySupplierCategory/?supplierId=1&categoryId=1
        public HttpResponseMessage ProductBySupplierCategory(int? supplierId = 0, int? categoryId = 0 )
        {
            db.Configuration.ProxyCreationEnabled = false;
            IQueryable<Product> query = db.Products;
            
            if(supplierId != 0 )
            {
                query = query.Where(e => e.SupplierID == supplierId);
            }
           
            if (categoryId != 0 )
            {
                query = query.Where(e => e.CategoryID == categoryId);
            }
           
            return Request.CreateResponse(HttpStatusCode.OK, query.ToList());
        }
    }
}
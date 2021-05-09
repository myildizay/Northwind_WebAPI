using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Northwind.Controllers
{
    public class OrderController : ApiController
    {
        NorthwindEntities db = new NorthwindEntities();
        
        [System.Web.Http.HttpGet]
        //https://localhost:44325/api/order/OrderParamByEmployeeId/?employeeId=1
        public HttpResponseMessage OrderParamByEmployeeId(int? employeeId= 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IQueryable<Order> query = db.Orders;

            if (employeeId != 0)
            {
                query = query.Where(e => e.EmployeeID == employeeId);
            }
            return Request.CreateResponse(HttpStatusCode.OK, query.ToList());
        }

        [System.Web.Http.HttpGet]
        //https://localhost:44325/api/order/OrderParamByShipVia/?shipVia=1
        public HttpResponseMessage OrderParamByShipVia(int? shipVia = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            IQueryable<Order> query = db.Orders;

            if (shipVia != 0)
            {
                query = query.Where(e => e.ShipVia == shipVia);
            }
            return Request.CreateResponse(HttpStatusCode.OK, query.ToList());
        }


        [System.Web.Http.HttpGet]
        //https://localhost:44325/api/order/OrderParamByShipCountryShipCity/?shipCountry=France&shipCity=Reims
        public HttpResponseMessage OrderParamByShipCountryShipCity(string shipCountry = "", string shipCity ="")
        {
            db.Configuration.ProxyCreationEnabled = false;
            IQueryable<Order> query = db.Orders;

            if (shipCountry != "")
            {
                query = query.Where(e => e.ShipCountry == shipCountry);
            }
            if (shipCity != "")
            {
                query = query.Where(e => e.ShipCity == shipCity);
            }

            return Request.CreateResponse(HttpStatusCode.OK, query.ToList());
        }
    }
}
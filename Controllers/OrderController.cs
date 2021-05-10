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


        public IEnumerable<Order> Get()
        {
            return db.Orders.ToList();
        }

        public Order Get(int id)
        {
            return db.Orders.FirstOrDefault(x => x.OrderID == id);
        }


        [System.Web.Http.HttpGet]
        //https://localhost:44325/api/order/OrderParamByEmployeeId/employeeId=1
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
        //https://localhost:44325/api/order/OrderParamByCustomerShipCountryShipCity?customerId=VINET&shipCountry=France&shipCity=Reims
        public HttpResponseMessage OrderParamByCustomerShipCountryShipCity(string  customerId ="" , string shipCountry = "", string shipCity ="")
        {
            db.Configuration.ProxyCreationEnabled = false;
            IQueryable<Order> query = db.Orders;
            if (customerId != "")
            {
                query = query.Where(e => e.CustomerID == customerId);
            }
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

        [System.Web.Http.HttpGet]
        //https://localhost:44325/api/order/OrderParamByShipName?shipName=TOMS
        public HttpResponseMessage OrderParamByShipName(string shipName="")
        {
            db.Configuration.ProxyCreationEnabled = false;
            IQueryable<Order> query = db.Orders;
            if (shipName != "")
            {
                query = query.Where(e => e.ShipName.Contains(shipName));
            }
            

            return Request.CreateResponse(HttpStatusCode.OK, query.ToList());
        }



    }
}
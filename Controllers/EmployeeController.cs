using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace Northwind.Controllers
{
    public class EmployeeController : ApiController
    {
        // GET: Employee
        NorthwindEntities db = new NorthwindEntities();

        public IEnumerable<Employee> Get()
        {
            return db.Employees.ToList();
        }

        public Employee Get(int id)
        {
            return db.Employees.FirstOrDefault(x => x.EmployeeID == id);
        }

        [HttpGet]
        //https://localhost:44325/api/employee/EmployeeReportsTo/?reportsTo=1
        public HttpResponseMessage EmployeeReportsTo(int? reportsTo)
        {
            IQueryable<Employee> query = db.Employees;
            db.Configuration.ProxyCreationEnabled = false;
            query = query.Where(e => e.ReportsTo == reportsTo);
            return Request.CreateResponse(HttpStatusCode.OK, query.ToList());
        }

        [HttpGet]
        // https://localhost:44325/api/employee/EmployeeByReportsTo/?reportsTo=1 
        public HttpResponseMessage EmployeeByReportsTo(int reportsTo)
        {
            IQueryable<Employee> query = db.Employees;
            db.Configuration.ProxyCreationEnabled = false;
            query = query.Where(e => e.ReportsTo == reportsTo);

            if (query == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Id: {reportsTo} not found any records.");
            }
            return Request.CreateResponse(HttpStatusCode.OK, query.ToList());
        }

    }
}
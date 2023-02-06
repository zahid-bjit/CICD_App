using Microsoft.IdentityModel.Tokens;
using RESTAPITest.DbContext;
using RESTAPITest.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;

namespace RESTAPITest.Controllers
{
    public class EmployeeApiController : ApiController
    {
        [Authorize]
        [HttpGet]
        public Object List()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var data = ctx.Employee.ToList();
                return Ok(data);
            }
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Create(EmployeeModel employeeModel)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var data = ctx.Employee.Add(employeeModel);
                ctx.SaveChanges();
                if (data != null)
                {
                    var hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                    hubContext.Clients.All.SendMessage(data.EmployeeName, "Employee added!");
                    return Ok("Employee added!");
                }

                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Update(EmployeeModel employeeModel)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var emp = ctx.Employee.Find(employeeModel.EmployeeId);
                if (emp != null)
                {
                    emp.EmployeeName = employeeModel.EmployeeName;
                    ctx.SaveChanges();
                    return Ok("Employee Updated!");
                }
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Deete(EmployeeModel employeeModel)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var emp = ctx.Employee.Find(employeeModel.EmployeeId);
                if (emp != null)
                {
                    ctx.Employee.Remove(emp);
                    ctx.SaveChanges();
                    return Ok("Employee Deleted!");
                }
                return NotFound();
            }
        }
    }
}
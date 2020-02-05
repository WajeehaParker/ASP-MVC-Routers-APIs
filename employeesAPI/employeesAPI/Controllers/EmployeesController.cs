using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using employeesAPI.Models;

namespace employeesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        EmployeeDB employeeDB = new EmployeeDB();

        // GET api/values
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return employeeDB.getAllEmployee().ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return employeeDB.getEmployeeByID(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Employee emp)
        {
            employeeDB.addEmployee(emp);
        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromBody] Employee emp)
        {
            employeeDB.updateEmployee(emp);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            employeeDB.deleteEmployee(id);
        }

        /*
         * EmployeeDB employeeDB = new EmployeeDB();
        [HttpGet("all")]
        public ActionResult<IEnumerable<Employee>> Index()
        {
            return employeeDB.getAllEmployee().ToList();
        }

        [HttpGet("emp")]
        public Employee SingleEmployee([FromQuery] int id)
        {
            return employeeDB.getEmployeeByID(id);
        }

        [HttpGet("create")]
        public IActionResult Create([FromQuery] string name, [FromQuery] string gender, [FromQuery] string department, [FromQuery] string username, [FromQuery] string password)
        {
            Employee emp = new Employee();
            emp.Name = name;
            emp.Gender = gender;
            emp.Department = department;
            emp.Username = username;
            emp.Password = password;
            employeeDB.addEmployee(emp);
            return Accepted();
        }

        [HttpGet("edit")]
        public IActionResult Edit([FromQuery] int id, [FromQuery] string name, [FromQuery] string gender, [FromQuery] string department, [FromQuery] string username, [FromQuery] string password)
        {
            if(employeeDB.getEmployeeByID(id)==null)
                return NotFound();
            Employee emp = new Employee();
            emp.ID = id;
            emp.Name = name;
            emp.Gender = gender;
            emp.Department = department;
            emp.Username = username;
            emp.Password = password;
            employeeDB.updateEmployee(emp);
            return Accepted();
        }

        [HttpGet("delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            if (employeeDB.getEmployeeByID(id) == null)
                return NotFound();
            employeeDB.deleteEmployee(id);
            return Accepted();
        }
        */
    }
}
 
using EmployeeDetailsWebAPI.Context;
using EmployeeDetailsWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeDetailsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly CurdContext _curdContext;
        public EmployeeController(CurdContext curdContext)
        {
            _curdContext = curdContext;
        }


        // GET: api/Employee
        [HttpGet]
        public IEnumerable<Employee> Get()
        {            
            return _curdContext.Employees;
        }

        // GET api/Employee/5
        [HttpGet("{id}", Name ="Get")]
        public Employee Get(int id)
        {
            return _curdContext.Employees.SingleOrDefault(x => x.EmployeeID == id);
        }

        // POST api/Employee
        [HttpPost]
        public void Post([FromBody] Employee employee)
        {
            _curdContext.Employees.Add(employee);
            _curdContext.SaveChanges();
        }

        // PUT api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee employee)
        {
            employee.EmployeeID = id;
            _curdContext.Employees.Update(employee);
            _curdContext.SaveChanges();
        }

        // DELETE api/Employee/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = _curdContext.Employees.FirstOrDefault(x => x.EmployeeID == id);
            if(item != null)
            {
                _curdContext.Employees.Remove(item); 
                _curdContext.SaveChanges();
            }
        }
    }
}

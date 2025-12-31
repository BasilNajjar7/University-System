using Microsoft.AspNetCore.Mvc;
using University_system.DTO;
using University_system.Model;
using University_system.Services;

namespace University_system.Controllers
{
    [ApiController]
    public class EmployeeContoller : ControllerBase
    {
        private readonly IRepositoryService<Employee> _repository;
        public EmployeeContoller(IRepositoryService<Employee> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Route("api/employee/all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAll();

            return Ok(result);
        }
        [HttpGet]
        [Route("api/employee/title")]
        public async Task<IActionResult> GetAllByJobTitle(string JobTitle)
        {
            var result = await _repository.GetAllEmployeeByJobTitle(JobTitle);

            return Ok(result);
        }
        [HttpGet]
        [Route("api/employee/id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _repository.GetById(id);

            if (result == null) return NotFound();

            return Ok(result);
        }
        [HttpPost]
        [Route("api/employee/add")]
        public async Task<IActionResult> RigisterAsync_emp([FromBody] AddEmployeeDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _repository.RegisterAsync_emp(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Massage);

            return Ok(result);
        }
        [HttpPut]
        [Route("api/employee/update")]
        public async Task<IActionResult> Update(AddEmployeeDTO employee)
        {
            if (await _repository.GetById(employee.Id) == null)
                return NotFound();

            var emp = new Employee();

            emp.Id = employee.Id;
            emp.First_Name = employee.First_Name;
            emp.Last_Name= employee.Last_Name;
            emp.UserName = employee.UserName;
            emp.Gender= employee.Gender;
            emp.PhoneNumber = employee.Phone_Number;
            emp.Salary=employee.Salary;

            var result = await _repository.Update(employee.Id,emp);

            return Ok(result);
        }
        [HttpDelete]
        [Route("api/employee/delete")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var result = await _repository.Delete(id);

            if (result == null) return NotFound();

            return Ok();
        }
        [HttpPost]
        [Route("api/employee/token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _repository.GetTokenAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Massage);

            return Ok(result);
        }
    }
}

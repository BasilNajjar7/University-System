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
        [Route("Get all employee")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAll();

            return Ok(result);
        }
        [HttpGet]
        [Route("Get employee by id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _repository.GetById(id);

            if (result == null) return NotFound();

            return Ok(result);
        }
        [HttpPost("Employee register")]
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
        [Route("Update emplyee data")]
        public async Task<IActionResult> Update(Employee employee)
        {
            if (_repository.GetById(employee.Id) == null)
                return NotFound();

            var result = await _repository.Update(employee.Id, employee);

            return Ok(result);
        }
        [HttpDelete]
        [Route("Remove employee")]
        public IActionResult DeleteById(Guid id)
        {
            var result = _repository.Delete(id);

            if (result == null) return NotFound();

            return Ok();
        }
        [HttpPost("Token emp")]
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

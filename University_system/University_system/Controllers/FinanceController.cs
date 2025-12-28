using Microsoft.AspNetCore.Mvc;
using University_system.Model;
using University_system.Services;

namespace University_system.Controllers
{
    [ApiController]
    public class FinanceController : ControllerBase
    {
        private readonly IRepositoryService<Student> _repository;
        public FinanceController(IRepositoryService<Student> repository)
        {
            _repository = repository;
        }
        [HttpGet("Get Student Balance by id")]
        public IActionResult GetById(string id)
        {
            var result=_repository.GetById(id);
            
            if(result == null)return NotFound("Sorry");

            return Ok(result.Student_Balance);
        }

        [HttpPut("id")]
        public IActionResult CashDeposit(string id,int money)
        {
            var result = _repository.GetById(id);

            if (result == null) return NotFound();

            result.Student_Balance += money;
            _repository.Update(id, result);

            return Ok(result);
        }
    }
}
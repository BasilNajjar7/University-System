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
        [HttpGet]
        [Route("api/finance/id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result=await _repository.GetById(id);
            
            if(result == null)return NotFound();

            return Ok(result.Student_Balance);
        }

        [HttpPut]
        [Route("api/finance/add")]
        public async Task<IActionResult> CashDeposit(Guid id,int money)
        {
            var result = await _repository.GetById(id);

            if (result == null) 
                return NotFound();

            result.Student_Balance += money;
            _repository.Update(id, result);

            return Ok(result);
        }
    }
}
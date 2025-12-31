using Microsoft.AspNetCore.Mvc;
using University_system.DTO;
using University_system.Model;
using University_system.Services;

namespace University_system.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IRepositoryService<Student> _repository;
        public StudentController(IRepositoryService<Student> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Route("api/student/allbyyear")]
        public async Task<IActionResult> GetByYear(int year)
        {
            var result = await _repository.GetByYear(year);

            return Ok(result);
        }
        [HttpGet]
        [Route("api/student/id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _repository.GetById(id);

            if(result == null)return NotFound();

            return Ok(result);
        }
        [HttpPost]
        [Route("api/student/register")]
        public async Task<IActionResult> RegisterAsync([FromBody] AddStudentDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _repository.RegisterAsync_stu(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Massage);

            return Ok(result);
        }
        [HttpPut]
        [Route("api/student/update")]
        public async Task<IActionResult> Update(Student student)
        {
            if (await _repository.GetById(student.Id) == null)
                return NotFound();

            var result = await _repository.Update(student.Id,student);

            return Ok(result);
        }
        [HttpDelete]
        [Route("api/student/delete")]
        public IActionResult DeleteById(Guid id)
        {
            var result = _repository.Delete(id);
            
            if(result==null)return NotFound();

            return Ok();
        }
        [HttpPost]
        [Route("api/student/token")]
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